using UnityEngine;
using System;

/// <summary>
/// 出牌回合类
/// </summary>
public class RoundModel
{
    /// <summary>
    /// 玩家出牌事件
    /// </summary>
    public static event Action<bool> PlayerHandler;
    /// <summary>
    /// 电脑出牌事件
    /// </summary>
    public static event Action<ComputerSmartArgs> ComputerHandler;

    private CharacterType biggestCharacter;
    private CharacterType currentCharacter;
    private CardType currentType;
    private int currentWeight;
    private int currentLength;

    /// <summary>
    /// 出牌的长度
    /// </summary>
    public int Length
    {
        get { return currentLength; }
        set { currentLength = value; }
    }

    /// <summary>
    /// 出牌的权值
    /// </summary>
    public int Weight
    {
        get { return currentWeight; }
        set { currentWeight = value; }
    }

    /// <summary>
    /// 出牌类型
    /// </summary>
    public CardType CardType
    {
        get { return currentType; }
        set { currentType = value; }
    }

    /// <summary>
    /// 最NB的出牌者
    /// </summary>
    public CharacterType Biggest
    {
        get { return biggestCharacter; }
        set { biggestCharacter = value; }
    }

    /// <summary>
    /// 当前出牌者
    /// </summary>
    public CharacterType Current
    {
        get { return currentCharacter; }
        set
        {
            currentCharacter = value;
        }
    }

    /// <summary>
    /// 初始化回合信息
    /// </summary>
    public void InitRound()
    {
        this.currentCharacter = CharacterType.Desk;
        this.biggestCharacter = CharacterType.Desk;
        this.currentType = CardType.None;
        this.currentWeight = -1;
        this.currentLength = -1;
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    /// <param name="cType"></param>
    public void Start(CharacterType cType)
    {
        this.currentCharacter = cType;
        this.biggestCharacter = cType;

        BeginWith(cType);
    }


    /// <summary>
    /// 转换出牌
    /// </summary>
    public void Turn()
    {
        currentCharacter++;
        //过滤
        if (currentCharacter == CharacterType.Desk || currentCharacter == CharacterType.Library)
            currentCharacter = CharacterType.Player;

        BeginWith(currentCharacter);
    }

    /// <summary>
    /// 开始回合
    /// </summary>
    /// <param name="cType"></param>
    private void BeginWith(CharacterType cType)
    {
        if (cType == CharacterType.Player)
        {
            //玩家出牌
            if (PlayerHandler != null)
                PlayerHandler(biggestCharacter != CharacterType.Player);
        }
        else
        {
            //电脑自动出牌
            if (ComputerHandler != null)
            {
                ComputerSmartArgs e = new ComputerSmartArgs()
                {
                    Biggest = this.Biggest,
                    CardType = this.CardType,
                    CharacterType = this.Current,
                    Length = this.Length,
                    Weight = this.Weight
                };
                ComputerHandler(e);
            }
        }
    }


}

