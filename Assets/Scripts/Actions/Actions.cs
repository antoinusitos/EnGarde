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

    protected int _currentAmount = 1;

    public virtual void InitAction()
    {

    }

    public virtual void ExecuteAction(int fromPlayer, Board currentBoard)
    {

    }

    public CardType GetCardType()
    {
        return _currentType;
    }

    public int GetCardAmount()
    {
        return _currentAmount;
    }

    public void SetCardAmount(int amount)
    {
        _currentAmount = amount;
    }
}
