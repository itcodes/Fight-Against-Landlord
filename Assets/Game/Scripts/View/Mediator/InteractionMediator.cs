using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using System;

public class InteractionMediator : EventMediator
{
    [Inject]
    public InteractionView InteractionView { get; set; }


    public override void OnRegister()
    {
        InteractionView.btn_Deal.onClick.AddListener(onDealClick);

        dispatcher.AddListener(ViewEvent.COMPLETE_DEAL, onCompleteDeal);
        dispatcher.AddListener(ViewEvent.COMPLETE_PLAY, onCompletePlay);
        dispatcher.AddListener(ViewEvent.RESTART_GAME, onRestartGame);

        InteractionView.btn_Disgrab.onClick.AddListener(onDisgrabClick);
        InteractionView.btn_Grab.onClick.AddListener(onGrabClick);
        InteractionView.btn_Pass.onClick.AddListener(onPassClick);
        InteractionView.btn_Play.onClick.AddListener(onPlayClick);

        //注册静态事件
        RoundModel.PlayerHandler += ActiveButton;
    }

    public override void OnRemove()
    {
        InteractionView.btn_Deal.onClick.RemoveListener(onDealClick);

        dispatcher.RemoveListener(ViewEvent.COMPLETE_DEAL, onCompleteDeal);
        dispatcher.RemoveListener(ViewEvent.COMPLETE_PLAY, onCompletePlay);
        dispatcher.RemoveListener(ViewEvent.RESTART_GAME, onRestartGame);

        InteractionView.btn_Disgrab.onClick.RemoveListener(onGrabClick);
        InteractionView.btn_Grab.onClick.RemoveListener(onGrabClick);
        InteractionView.btn_Pass.onClick.RemoveListener(onPassClick);
        InteractionView.btn_Play.onClick.RemoveListener(onPlayClick);

        RoundModel.PlayerHandler -= ActiveButton;
    }


    #region 回调事件

    /// <summary>
    /// 重新开始游戏的回调
    /// </summary>
    private void onRestartGame()
    {
        InteractionView.DeactiveAll();
        InteractionView.ActiveDeal();
    }

    /// <summary>
    /// 发牌的回调函数
    /// </summary>
    private void onDealClick()
    {
        dispatcher.Dispatch(CommandEvent.RequestDeal);
        InteractionView.DeactiveAll();
    }

    /// <summary>
    /// 发牌结束的回调
    /// </summary>
    /// <param name="payload"></param>
    private void onCompleteDeal()
    {
        InteractionView.ActiveGrabAndDisgrab();
    }

    /// <summary>
    /// 不抢的点击事件
    /// </summary>
    private void onDisgrabClick()
    {
        InteractionView.DeactiveAll();

        int r = UnityEngine.Random.Range(2, 4);
        GrabLandlordArgs e = new GrabLandlordArgs()
        { cType = (CharacterType)r };
        dispatcher.Dispatch(CommandEvent.GrabLandLord, e);
    }


    /// <summary>
    /// 抢地主的点击事件
    /// </summary>
    private void onGrabClick()
    {
        InteractionView.DeactiveAll();

        GrabLandlordArgs e = new GrabLandlordArgs()
        { cType = CharacterType.Player };
        dispatcher.Dispatch(CommandEvent.GrabLandLord, e);
    }

    /// <summary>
    /// 激活按钮
    /// </summary>
    private void ActiveButton(bool canPass)
    {
        InteractionView.ActivePlayAndPass(canPass);
    }

    /// <summary>
    /// 出牌点击事件
    /// </summary>
    private void onPlayClick()
    {
        dispatcher.Dispatch(ViewEvent.REQUEST_PLAY);
    }

    /// <summary>
    /// 不出点击事件
    /// </summary>
    private void onPassClick()
    {
        dispatcher.Dispatch(CommandEvent.PassCard);
        InteractionView.DeactiveAll();
    }

    /// <summary>
    /// 完成出牌的回调
    /// </summary>
    private void onCompletePlay()
    {
        InteractionView.DeactiveAll();
    }


    #endregion





}
