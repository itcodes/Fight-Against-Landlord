using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections.Generic;


public class StartMediator : EventMediator
{
    [Inject]
    public StartView StartView { get; set; }

    /// <summary>
    /// 注册函数
    /// </summary>
    public override void OnRegister()
    {
        StartView.Init();

        StartView.dispatcher.AddListener(ViewEvent.CHANGE_MULTIPLE, onViewClick);
    }

    /// <summary>
    /// 移除函数
    /// </summary>
    public override void OnRemove()
    {
        StartView.ViewDestroy();

        StartView.dispatcher.RemoveListener(ViewEvent.CHANGE_MULTIPLE, onViewClick);
    }
    
    /// <summary>
    /// View被点击时候的调用
    /// </summary>
    /// <param name="evt"></param>
    private void onViewClick(IEvent evt)
    {
        int multiple = (int)evt.data;
        //发送出去
        dispatcher.Dispatch(CommandEvent.ChangeMultiple, multiple);
    }
}
