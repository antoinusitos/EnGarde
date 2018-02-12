using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    NONE,
    ARROW,
    MAGIC,
    SWORD,
    MOVE,
    SHIELD,
}

public class Actions
{
    protected CardType _currentType = CardType.NONE;

    public virtual void InitAction()
    {

    }

    public virtual void ExecuteAction()
    {

    }
}
