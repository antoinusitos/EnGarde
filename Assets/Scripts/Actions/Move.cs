using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Actions
{
    public override void InitAction()
    {
        _currentType = CardType.MOVE;
    }

    public override void ExecuteAction(int fromPlayer)
    {
        UIManager.GetInstance().GetPlayerUIState(0).SetPlayerPos(fromPlayer, _currentAmount);
    }
}