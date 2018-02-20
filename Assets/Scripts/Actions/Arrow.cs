using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Actions
{
    public override void InitAction()
    {
        _currentType = CardType.ARROW;
    }

    public override void ExecuteAction(int fromPlayer, Board currentBoard, Actions enemyAction)
    {
        //Attack
        Debug.Log("ATTACK ARROW :" + _resolutionAmount);
        StopAction();
        _resolutionAmount = 0;
    }
}
