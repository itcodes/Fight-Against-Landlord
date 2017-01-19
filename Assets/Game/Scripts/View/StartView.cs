using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using strange.extensions.mediation.impl;

public class StartView : EventView
{
    private Button btn_One;
    private Button btn_Two;

    /// <summary>
    /// 初始化获取组件等
    /// </summary>
    public void Init()
    {
        btn_One = transform.Find("btn_One").GetComponent<Button>();
        btn_Two = transform.Find("btn_Two").GetComponent<Button>();
        //注册点击事件
        btn_One.onClick.AddListener(onOneClick);
        btn_Two.onClick.AddListener(onTwoClick);
    }

    /// <summary>
    /// 移除点击事件
    /// </summary>
    public void ViewDestroy()
    {
        btn_One.onClick.RemoveListener(onOneClick);
        btn_Two.onClick.RemoveListener(onTwoClick);
    }

    /// <summary>
    /// 单倍按钮点击
    /// </summary>
    private void onOneClick()
    {
        //更改Intergration的倍数为1
        dispatcher.Dispatch(ViewEvent.CHANGE_MULTIPLE, 1);
        //删除面板
        Destroy(gameObject);
    }

    /// <summary>
    /// 双倍按钮点击
    /// </summary>
    private void onTwoClick()
    {
        //更改Intergration的倍数为2
        dispatcher.Dispatch(ViewEvent.CHANGE_MULTIPLE, 2);
        //删除面板
        Destroy(gameObject);
    }
}

