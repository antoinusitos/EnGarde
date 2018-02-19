using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Actions
{
    //THIS ACTION MAKE THE PLAYER MOVING BACK

    public override void InitAction()
    {
        _currentType = CardType.MOVE;
    }

    public override void ExecuteAction(int fromPlayer, Board currentBoard)
    {
        int basePos = currentBoard.GetPlayerPos(fromPlayer);
        int newPos = currentBoard.CalcPlayerPos(fromPlayer, -1);
        // if we moved
        if (newPos != basePos)
        {
            currentBoard.SetPlayerPos(fromPlayer, newPos);
            AllowActionUpdate();
        }
        else 
        {
            //Cannot move more
            StopAction();
            _resolutionAmount = 0;
        }
    }
}