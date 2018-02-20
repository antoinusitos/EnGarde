﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Actions
{
    public override void InitAction()
    {
        _currentType = CardType.SHIELD;
    }

    public override void ExecuteAction(int fromPlayer, Board currentBoard, Actions enemyAction)
    {
        //Shield
        Debug.Log("SHIELD :" + _resolutionAmount);
        StopAction();
        _resolutionAmount = 0;
    }
}