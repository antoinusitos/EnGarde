using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : Actions
{
    public override void InitAction()
    {
        _currentType = CardType.MAGIC;
    }

    public override void ExecuteAction(int fromPlayer, Board currentBoard, Actions enemyAction)
    {
        //Attack
        Debug.Log("ATTACK MAGIC :" + _resolutionAmount);
        int otherPlayer = fromPlayer == 0 ? 1 : 0;
        GameManager.GetInstance().DamagePlayer(otherPlayer, _resolutionAmount);
        BoardManager.GetInstance().SetMustEndTurn();
        StopAction();
        _resolutionAmount = 0;
    }
}