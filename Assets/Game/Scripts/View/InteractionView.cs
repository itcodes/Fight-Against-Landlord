using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using strange.extensions.mediation.impl;

public class InteractionView : View
{
    public Button btn_Deal;
    public Button btn_Play;
    public Button btn_Pass;
    public Button btn_Grab;
    public Button btn_Disgrab;

    /// <summary>
    /// 全部隐藏按钮
    /// </summary>
    public void DeactiveAll()
    {
        btn_Deal.gameObject.SetActive(false);
        btn_Play.gameObject.SetActive(false);
        btn_Pass.gameObject.SetActive(false);
        btn_Grab.gameObject.SetActive(false);
        btn_Disgrab.gameObject.SetActive(false);
    }


    /// <summary>
    /// 显示发牌按钮
    /// </summary>
    public void ActiveDeal()
    {
        btn_Deal.gameObject.SetActive(true);
        btn_Play.gameObject.SetActive(false);
        btn_Pass.gameObject.SetActive(false);
        btn_Grab.gameObject.SetActive(false);
        btn_Disgrab.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示出牌按钮
    /// </summary>
    public void ActivePlayAndPass(bool canPass = true)
    {
        btn_Deal.gameObject.SetActive(false);
        btn_Play.gameObject.SetActive(true);
        btn_Pass.gameObject.SetActive(true);
        btn_Pass.interactable = canPass;
        btn_Grab.gameObject.SetActive(false);
        btn_Disgrab.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示抢地主按钮
    /// </summary>
    public void ActiveGrabAndDisgrab()
    {
        btn_Deal.gameObject.SetActive(false);
        btn_Play.gameObject.SetActive(false);
        btn_Pass.gameObject.SetActive(false);
        btn_Grab.gameObject.SetActive(true);
        btn_Disgrab.gameObject.SetActive(true);
    }


}
