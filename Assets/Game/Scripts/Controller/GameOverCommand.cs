using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;

public class GameOverCommand : EventCommand
{
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }

    [Inject]
    public RoundModel RoundModel { get; set; }

    [Inject]
    public CardModel CardModel { get; set; }

    public override void Execute()
    {
        int result = IntegrationModel.Result;

        GameOverArgs e = evt.data as GameOverArgs;

        #region 更新积分
        if (e.PlayerWin)
            IntegrationModel.PlayerIntergration += result;
        else
            IntegrationModel.PlayerIntergration -= result;
        if (e.ComputerLeftWin)
            IntegrationModel.ComputerLeftIntergration += result;
        else
            IntegrationModel.ComputerLeftIntergration -= result;
        if (e.ComputerRightWin)
            IntegrationModel.ComputerRightIntergration += result;
        else
            IntegrationModel.ComputerRightIntergration -= result;
        #endregion

        #region 保存数据
        GameData data = new GameData();
        data.PlayerIntergration = IntegrationModel.PlayerIntergration;
        data.ComputerLeftIntergration = IntegrationModel.ComputerLeftIntergration;
        data.ComputerRightIntergration = IntegrationModel.ComputerRightIntergration;
        Tools.SaveData(data);
        #endregion

        //更新积分UI
        dispatcher.Dispatch(ViewEvent.UPDATE_INTEGRATION, data);

        CardModel.InitCardLibrary();
        RoundModel.InitRound();
        PoolManager.Instance.HideAllObject("Card");

        //显示一个游戏结束的面板
        Tools.CreateUIPanel(PanelType.GameOverPanel);
    }
}
