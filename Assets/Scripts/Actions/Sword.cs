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
        int basePos = currentBoard.GetPlayerPos(fromPlayer);
        int newPos = currentBoard.CalcPlayerPos(fromPlayer, 1);
        Debug.Log("fromPlayer :" + fromPlayer);
        Debug.Log("basePos :" + basePos);
        Debug.Log("newPosnewPos :" + newPos);
        // if we moved
        if (newPos != basePos)
        {
            currentBoard.SetPlayerPos(fromPlayer, newPos);
            AllowActionUpdate();
        }
        else
        {
            //Cannot move more
            Debug.Log("ATTACK SWORD :" + _resolutionAmount);
            StopAction();
            _resolutionAmount = 0;
        }
    }
}