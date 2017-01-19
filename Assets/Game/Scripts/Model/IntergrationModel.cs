using UnityEngine;
using System.Collections;

/// <summary>
/// 积分
/// </summary>
public class IntegrationModel
{
    /// <summary>
    /// 底分
    /// </summary>
    public int BasePoint;

    /// <summary>
    /// 倍数
    /// </summary>
    public int Multiples;

    /// <summary>
    /// 总分
    /// </summary>
    public int Result
    {
        get { return BasePoint * Multiples; }
    }

    /// <summary>
    /// 玩家积分
    /// </summary>
    private int playerIntergration;
    public int PlayerIntergration
    {
        set
        {
            if (value < 0)
                playerIntergration = 0;
            else
                playerIntergration = value;
        }
        get { return playerIntergration; }
    }

    private int computerLeftIntergration;
    public int ComputerLeftIntergration
    {
        set
        {
            if (value < 0)
                computerLeftIntergration = 0;
            else
                computerLeftIntergration = value;
        }
        get { return computerLeftIntergration; }
    }
    private int computerRightIntergration;
    public int ComputerRightIntergration
    {
        set
        {
            if (value< 0)
                computerRightIntergration = 0;
            else
                computerRightIntergration = value;
        }
        get { return computerRightIntergration; }
    }


    /// <summary>
    /// 初始化积分信息
    /// </summary>
    public void InitIntergration()
    {
        PlayerIntergration = 1000;
        ComputerLeftIntergration = 1000;
        ComputerRightIntergration = 1000;

        Multiples = 1;
        BasePoint = 100;
    }

}

