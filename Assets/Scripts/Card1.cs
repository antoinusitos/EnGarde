using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card1
{
    private Actions _action = null;

    public void SetCard(Actions type)
    {
        _action = type;
    }

    public void CardToString()
    {
        Debug.Log(
            "Print Card :" +
            "Effect = " + _action.GetCardType() + " for " + _action.GetCardAmount() + " units. "
            );
    }

    public Actions GetSelectedAction()
    {
        return _action;
    }

    public void RecoverActions(int actionIndex, int actionAmount)
    {
        switch(actionIndex)
        {
            case 0:
                _action = new Arrow();
                break;
            case 1:
                _action = new Magic();
                break;
            case 2:
                _action = new Move();
                break;
            case 3:
                _action = new Shield();
                break;
            case 4:
                _action = new Sword();
                break;
        }

        _action.InitAction();
        _action.SetCardAmount(actionAmount);
    }

    public CardType GetCardType()
    {
        return _action.GetCardType();
    }

    public int GetCardAmount()
    {
        return _action.GetCardAmount();
    }
}