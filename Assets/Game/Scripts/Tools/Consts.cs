using System;
using System.Collections.Generic;
using UnityEngine;

public class Consts
{
    /// <summary>
    /// 游戏数据路径信息
    /// </summary>
    public static readonly string DataPath = Application.persistentDataPath + @"\data.xml";


}


/// <summary>
/// View的事件
/// </summary>
public enum ViewEvent
{
    CHANGE_MULTIPLE,//改变倍数
    COMPLETE_DEAL,//完成发牌
    DEAL_THREECARD,//发底牌
    REQUEST_PLAY,//请求出牌
    SUCCESSED_PLAY,//成功出牌
    COMPLETE_PLAY,//完成出牌
    RESTART_GAME,//重新开始
    UPDATE_INTEGRATION,//更新积分
}

/// <summary>
/// Command的事件
/// </summary>
public enum CommandEvent
{
    ChangeMultiple,//改变倍数
    RequestDeal,//请求发牌
    DealCard,//发牌
    GrabLandLord,//抢地主
    PlayCard,//出牌
    PassCard,//不出
    GameOver,//游戏结束
    RequestUpdate,//更新积分
}


/// <summary>
/// 面板类型
/// </summary>
public enum PanelType
{
    StartPanel,
    BackgroundPanel,
    CharacterPanel,
    InteractionPanel,
    GameOverPanel
}

/// <summary>
/// 角色类型
/// </summary>
public enum CharacterType
{
    Library = 0,//牌库
    Player = 1,//玩家
    ComputerRight = 2,//电脑
    ComputerLeft = 3,
    Desk//桌子
}


/// <summary>
/// 卡牌花色
/// </summary>
public enum Colors
{
    None,
    Club,//梅花
    Heart,//红桃
    Spade,//黑桃
    Square//方片
}


/// <summary>
/// 卡牌的权值
/// </summary>
public enum Weight
{
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    One,
    Two,
    SJoker,
    LJoker
}


/// <summary>
/// 出牌类型
/// </summary>
public enum CardType
{
    None,
    Single,//单
    Double,//对儿
    Straight,//顺子
    DoubleStraight,//双顺
    TripleStraight,//飞机
    Three,//三不带
    ThreeAndOne,//三带一
    ThreeAndTwo,//三代二
    Boom,//炸弹
    JokerBoom//王炸
}


/// <summary>
/// 身份
/// </summary>
public enum Identity
{
    Farmer,//农民
    Landlord//地主
}
