using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : Actions
{
    public override void InitAction()
    {
        _currentType = CardType.MAGIC;
    }

    public override void ExecuteAction(int fromPlayer, Board currentBoard)
    {

    }
}