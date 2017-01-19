using strange.extensions.command.impl;
using System.Collections.Generic;
using UnityEngine;


public class PlayCardCommand : EventCommand
{
    [Inject]
    public RoundModel RoundModel { get; set; }

    [Inject]
    public IntegrationModel IntegrationModel { get; set; }

    public override void Execute()
    {
        PlayCardArgs e = evt.data as PlayCardArgs;

        //判断玩家出牌合不合法
        if (e.characterType == CharacterType.Player)
        {
            if (e.cardType == RoundModel.CardType && e.Weight > RoundModel.Weight)
                dispatcher.Dispatch(ViewEvent.SUCCESSED_PLAY);
            else if (e.cardType == CardType.Boom && RoundModel.CardType != CardType.Boom)
                dispatcher.Dispatch(ViewEvent.SUCCESSED_PLAY);
            else if (e.cardType == CardType.JokerBoom)
                dispatcher.Dispatch(ViewEvent.SUCCESSED_PLAY);
            else if (RoundModel.Biggest == CharacterType.Player)
                dispatcher.Dispatch(ViewEvent.SUCCESSED_PLAY);
            else
            {
                Debug.Log("不合法的出牌");
                return;
            }
        }

        //炸弹翻倍
        if (e.cardType == CardType.Boom || e.cardType == CardType.JokerBoom)
            IntegrationModel.Multiples *= 2;

        //保存回合信息
        RoundModel.Length = e.Length;
        RoundModel.Weight = e.Weight;
        RoundModel.CardType = e.cardType;
        RoundModel.Biggest = e.characterType;

        //转换出牌
        RoundModel.Turn();
    }
}

