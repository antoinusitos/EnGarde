using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Actions
{
    public override void InitAction()
    {
        _currentType = CardType.MOVE;
    }

    public override void ExecuteAction(int fromPlayer, Board currentBoard)
    {
        base.ExecuteAction(fromPlayer, currentBoard);
        int l = currentBoard.SetPlayerPos(fromPlayer, -1);
        if (l != 0)
        {
            //Cannot move more
            StopAction();
            _resolutionAmount = 0;
        }
    }
}