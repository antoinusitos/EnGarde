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
        currentBoard.SetPlayerPos(fromPlayer, -_currentAmount);
    }
}