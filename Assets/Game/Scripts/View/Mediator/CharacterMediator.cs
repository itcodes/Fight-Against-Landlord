using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CharacterMediator : EventMediator
{
    [Inject]
    public CharacterView CharacterView { get; set; }

    public override void OnRegister()
    {
        CharacterView.Init();

        dispatcher.AddListener(CommandEvent.DealCard, onDealCard);
        dispatcher.AddListener(ViewEvent.COMPLETE_DEAL, onCompleteDeal);
        dispatcher.AddListener(ViewEvent.DEAL_THREECARD, onDealThreeCard);
        dispatcher.AddListener(ViewEvent.REQUEST_PLAY, onPlayerPlayCard);
        dispatcher.AddListener(ViewEvent.SUCCESSED_PLAY, onPlayerSuccessPlay);
        dispatcher.AddListener(ViewEvent.RESTART_GAME, onRestartGame);
        dispatcher.AddListener(ViewEvent.UPDATE_INTEGRATION, onUpdateIntegration);

        RoundModel.ComputerHandler += RoundModel_ComputerHandler;

        dispatcher.Dispatch(CommandEvent.RequestUpdate);
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(CommandEvent.DealCard, onDealCard);
        dispatcher.RemoveListener(ViewEvent.COMPLETE_DEAL, onCompleteDeal);
        dispatcher.RemoveListener(ViewEvent.DEAL_THREECARD, onDealThreeCard);
        dispatcher.RemoveListener(ViewEvent.REQUEST_PLAY, onPlayerPlayCard);
        dispatcher.RemoveListener(ViewEvent.SUCCESSED_PLAY, onPlayerSuccessPlay);
        dispatcher.RemoveListener(ViewEvent.RESTART_GAME, onRestartGame);
        dispatcher.RemoveListener(ViewEvent.UPDATE_INTEGRATION, onUpdateIntegration);

        RoundModel.ComputerHandler -= RoundModel_ComputerHandler;
    }



    #region 回调事件

    /// <summary>
    /// 更新积分的处理
    /// </summary>
    private void onUpdateIntegration(IEvent evt)
    {
        GameData data = evt.data as GameData;
        CharacterView.Player.characterUI.SetIntergration(data.PlayerIntergration);
        CharacterView.ComputerLeft.characterUI.SetIntergration(data.ComputerLeftIntergration);
        CharacterView.ComputerRight.characterUI.SetIntergration(data.ComputerRightIntergration);
    }

    /// <summary>
    /// 重新开始游戏
    /// </summary>
    private void onRestartGame()
    {
        CharacterView.Player.CardList.Clear();
        CharacterView.ComputerLeft.CardList.Clear();
        CharacterView.ComputerRight.CardList.Clear();
    }

    /// <summary>
    /// 电脑自动出牌
    /// </summary>
    /// <param name="obj"></param>
    private void RoundModel_ComputerHandler(ComputerSmartArgs e)
    {
        StartCoroutine("DelayOneSecond", e);
    }

    /// <summary>
    /// 延迟出牌
    /// </summary>
    IEnumerator DelayOneSecond(ComputerSmartArgs e)
    {
        yield return new WaitForSeconds(1f);

        bool can = false;
        switch (e.CharacterType)
        {
            case CharacterType.ComputerRight:
                can = CharacterView.ComputerRight.ComputerSmartPlayCard(e.CardType, e.Weight, e.Length, e.Biggest == CharacterType.ComputerRight);
                //出牌的检测
                if (can)
                {
                    List<Card> cardList =
                       CharacterView.ComputerRight.SelectCards;
                    CardType cardType = CharacterView.ComputerRight.CurrType;

                    //添加牌到桌面
                    CharacterView.Desk.Clear();
                    foreach (Card card in cardList)
                        CharacterView.AddCard(CharacterType.Desk, card, false);

                    //可以出牌
                    PlayCardArgs ee = new PlayCardArgs()
                    {
                        cardType = cardType,
                        characterType = CharacterType.ComputerRight,
                        Length = cardList.Count,
                        Weight = Tools.GetWeight(cardList, cardType)
                    };

                    if (!CharacterView.ComputerRight.HasCard)
                    {
                        Identity r = CharacterView.ComputerRight.Identity;
                        Identity l = CharacterView.ComputerLeft.Identity;
                        Identity p = CharacterView.Player.Identity;
                        GameOverArgs eee = new GameOverArgs()
                        {
                            ComputerRightWin = true,
                            ComputerLeftWin = l == r ? true : false,
                            PlayerWin = p == r ? true : false
                        };
                        dispatcher.Dispatch(CommandEvent.GameOver, eee);
                    }
                    else
                        dispatcher.Dispatch(CommandEvent.PlayCard, ee);
                }
                else
                {
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }
                break;
            case CharacterType.ComputerLeft:
                can = CharacterView.ComputerLeft.ComputerSmartPlayCard(e.CardType, e.Weight, e.Length, e.Biggest == CharacterType.ComputerLeft);
                //出牌的检测
                if (can)
                {
                    List<Card> cardList =
                       CharacterView.ComputerLeft.SelectCards;
                    CardType cardType = CharacterView.ComputerLeft.CurrType;

                    //添加牌到桌面
                    CharacterView.Desk.Clear();
                    foreach (Card card in cardList)
                        CharacterView.AddCard(CharacterType.Desk, card, false);

                    //可以出牌
                    PlayCardArgs ee = new PlayCardArgs()
                    {
                        cardType = cardType,
                        characterType = CharacterType.ComputerLeft,
                        Length = cardList.Count,
                        Weight = Tools.GetWeight(cardList, cardType)
                    };

                    if (!CharacterView.ComputerLeft.HasCard)
                    {
                        Identity r = CharacterView.ComputerRight.Identity;
                        Identity l = CharacterView.ComputerLeft.Identity;
                        Identity p = CharacterView.Player.Identity;
                        GameOverArgs eee = new GameOverArgs()
                        {
                            ComputerLeftWin = true,
                            ComputerRightWin = r == l ? true : false,
                            PlayerWin = p == l ? true : false
                        };
                        dispatcher.Dispatch(CommandEvent.GameOver, eee);
                    }
                    else
                        dispatcher.Dispatch(CommandEvent.PlayCard, ee);
                }
                else
                {
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 发牌的回调
    /// </summary>
    private void onDealCard(IEvent evt)
    {
        DealCardArgs e = evt.data as DealCardArgs;
        CharacterView.AddCard(e.cType, e.card, e.selected);
    }

    /// <summary>
    /// 发牌结束的回调
    /// </summary>
    private void onCompleteDeal()
    {
        CharacterView.Player.Sort(false);
        CharacterView.Desk.Sort(true);
        //34567
        CharacterView.ComputerLeft.Sort(true);
        CharacterView.ComputerRight.Sort(true);
    }


    /// <summary>
    /// 发底牌的回调
    /// </summary>
    /// <param name="payload"></param>
    private void onDealThreeCard(IEvent evt)
    {
        GrabLandlordArgs e = evt.data as GrabLandlordArgs;
        CharacterView.AddThreeCard(e.cType);
    }


    /// <summary>
    /// 玩家请求出牌
    /// </summary>
    private void onPlayerPlayCard()
    {
        List<Card> cardList = CharacterView.Player.FindSelectCard();
        CardType cardType;
        Rulers.CanPop(cardList, out cardType);

        //!#@!#!@$!@#!@$!@%!@#
        //还需要根据上一次的出牌类型 

        if (cardType != CardType.None)
        {
            //可以出牌
            PlayCardArgs e = new PlayCardArgs()
            {
                cardType = cardType,
                characterType = CharacterType.Player,
                Length = cardList.Count,
                Weight = Tools.GetWeight(cardList, cardType)
            };
            dispatcher.Dispatch(CommandEvent.PlayCard, e);
        }
        else
        {
            UnityEngine.Debug.Log("请选择正确的牌");
        }
    }

    /// <summary>
    /// 玩家成功出牌
    /// </summary>
    private void onPlayerSuccessPlay()
    {
        List<Card> cardList = CharacterView.Player.FindSelectCard();

        //添加牌到桌面
        CharacterView.Desk.Clear();
        foreach (Card card in cardList)
            CharacterView.AddCard(CharacterType.Desk, card, false);

        CharacterView.Player.DestroySelectCard();

        //游戏胜利的判断
        if (!CharacterView.Player.HasCard)
        {
            Identity r = CharacterView.ComputerRight.Identity;
            Identity l = CharacterView.ComputerLeft.Identity;
            Identity p = CharacterView.Player.Identity;
            GameOverArgs eee = new GameOverArgs()
            {
                PlayerWin = true,
                ComputerRightWin = r == p ? true : false,
                ComputerLeftWin = l == p ? true : false
            };
            dispatcher.Dispatch(CommandEvent.GameOver, eee);
        }
        else
            dispatcher.Dispatch(ViewEvent.COMPLETE_PLAY);
    }

    #endregion


}
