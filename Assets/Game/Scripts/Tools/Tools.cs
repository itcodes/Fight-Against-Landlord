using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Collections.Generic;

public static class Tools
{
    private static Transform uiParent;
    /// <summary>
    /// UI的父物体
    /// </summary>
    public static Transform UIParent
    {
        get
        {
            if (uiParent == null)
                uiParent = GameObject.Find("GameRoot").transform;
            return uiParent;
        }
    }


    /// <summary>
    /// 创建UI面板
    /// </summary>
    /// <param name="panelType">面板类型</param>
    /// <returns>创建面板的实例</returns>
    public static GameObject CreateUIPanel(PanelType panelType)
    {
        GameObject prefab = Resources.Load<GameObject>(panelType.ToString());
        if (prefab == null)
        {
            Debug.LogWarning("这个 " + panelType.ToString() + " 面板不存在");
            return null;
        }
        else
        {
            GameObject panel = Object.Instantiate<GameObject>(prefab);
            panel.name = panelType.ToString();
            panel.transform.SetParent(UIParent, false);
            return panel;
        }
    }



    /// <summary>
    /// 用UTF8保存数据
    /// </summary>
    public static void SaveData(GameData data)
    {
        string fileName = Consts.DataPath;

        Stream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
        xmlSerializer.Serialize(sw, data);

        sw.Close();
        stream.Close();
    }


    /// <summary>
    /// 获取数据
    /// </summary>
    /// <returns></returns>
    public static GameData GeyDataWithOutBom()
    {
        GameData data = new GameData();

        Stream stream = new FileStream(Consts.DataPath, FileMode.Open, FileAccess.Read, FileShare.None);
        //忽略标记--true
        StreamReader sr = new StreamReader(stream, true);
        XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
        data = xmlSerializer.Deserialize(sr) as GameData;
        stream.Close();
        sr.Close();

        return data;
    }

    /// <summary>
    /// 卡牌排序
    /// </summary>
    /// <param name="cards">选择的牌</param>
    /// <param name="asc">是否升序</param>
    public static void Sort(List<Card> cards, bool asc)
    {
        cards.Sort(
            (Card a, Card b) =>
            {
                if (asc)
                    return a.CardWeight.CompareTo(b.CardWeight);
                else
                    return -a.CardWeight.CompareTo(b.CardWeight);
            }
            );
    }

    /// <summary>
    /// 获取卡牌的权值
    /// </summary>
    /// <param name="cards">选中的卡牌</param>
    /// <param name="cardType">卡牌类型</param>
    /// <returns>权值</returns>
    public static int GetWeight(List<Card> cards, CardType cardType)
    {
        int totalWeight = 0;
        //过滤三带一、二//3332 2333
        if (cardType == CardType.ThreeAndOne || cardType == CardType.ThreeAndTwo)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].CardWeight == cards[i + 1].CardWeight && cards[i].CardWeight == cards[i + 2].CardWeight)
                {
                    totalWeight += (int)cards[i].CardWeight;
                    totalWeight *= 3;
                    break;
                }
            }
        }
        else if (cardType == CardType.Straight || cardType == CardType.DoubleStraight)
        {
            totalWeight = (int)cards[0].CardWeight;
        }
        //其余类型 全算权值
        else
        {
            for (int i = 0; i < cards.Count; i++)
            {
                totalWeight += (int)cards[i].CardWeight;
            }
        }
        return totalWeight;
    }

}


