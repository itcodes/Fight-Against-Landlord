using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;

public class GameOverView : View
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }


    /// <summary>
    /// 点击重新开始
    /// </summary>
    public void OnRestartClick()
    {
        dispatcher.Dispatch(ViewEvent.RESTART_GAME);
        Destroy(gameObject);
    }


    /// <summary>
    /// 退出的点击
    /// </summary>
    public void OnExitClick()
    {
        Application.Quit();
    }

}
