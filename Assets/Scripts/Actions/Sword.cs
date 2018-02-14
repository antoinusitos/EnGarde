﻿using System.Collections;
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
        currentBoard.SetPlayerPos(fromPlayer, _currentAmount);
    }
}