using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Actions
{
    public override void InitAction()
    {
        _currentType = CardType.SHIELD;
    }

    public override void ExecuteAction(int fromPlayer, Board currentBoard)
    {
        base.ExecuteAction(fromPlayer, currentBoard);
        //Shield
        Debug.Log("SHIELD :" + _resolutionAmount);
        StopAction();
        _resolutionAmount = 0;
    }
}