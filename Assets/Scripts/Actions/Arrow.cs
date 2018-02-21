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
        int otherPlayer = fromPlayer == 0 ? 1 : 0;
        GameManager.GetInstance().DamagePlayer(otherPlayer, _resolutionAmount);
        _resolutionAmount = 0;
    }
}
