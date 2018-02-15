using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Actions
{
    public override void InitAction()
    {
        _currentType = CardType.SWORD;
    }

    public override void ExecuteAction(int fromPlayer, Board currentBoard)
    {
        base.ExecuteAction(fromPlayer, currentBoard);
        int l = currentBoard.SetPlayerPos(fromPlayer, 1);
        if (l != 0)
        {
            //Attack
            Debug.Log("ATTACK SWORD :" + _resolutionAmount);
            StopAction();
            _resolutionAmount = 0;
        }
    }
}