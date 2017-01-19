﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 卡牌
/// </summary>
public class Card
{
    private string cardName;
    private Colors cardColor;
    private Weight cardWeight;
    private CharacterType belongTo;

    /// <summary>
    /// 卡牌名字
    /// </summary>
    public string CardName
    {
        get { return cardName; }
    }

    /// <summary>
    /// 卡牌花色
    /// </summary>
    public Colors CardColor
    {
        get { return cardColor; }
    }

    /// <summary>
    /// 卡牌权值
    /// </summary>
    public Weight CardWeight
    {
        get { return cardWeight; }
    }

    /// <summary>
    /// 卡牌所属的角色
    /// </summary>
    public CharacterType BelongTo
    {
        get { return belongTo; }
        set { belongTo = value; }
    }


    public Card(string name, Colors color, Weight weight, CharacterType belongTo)
    {
        this.cardName = name;
        this.cardColor = color;
        this.cardWeight = weight;
        this.belongTo = belongTo;
    }

}
