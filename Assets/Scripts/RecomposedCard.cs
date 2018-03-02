using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecomposedCard
{
    private Card1 _leftAction = null;
    private Card1 _rightAction = null;

    public void SetCard(Card1 type1, Card1 type2)
    {
        _leftAction = type1;
        _rightAction = type2;
    }


    public void CardToString()
    {
        Debug.Log(
            "Print Card :" +
            "Effect1 = " + _leftAction.GetCardType() + " for " + _leftAction.GetCardAmount() + " units. " +
            "Effect2 = " + _rightAction.GetCardType() + " for " + _rightAction.GetCardAmount() + " units."
            );
    }

    // 0 = Left, 1 = Right
    public void SideToString(int side)
    {
        if (side == 0)
            Debug.Log(
                _leftAction.GetCardType() + " for " + _leftAction.GetCardAmount() + " units. "
                );
        else if (side == 1)
            Debug.Log(
            _rightAction.GetCardType() + " for " + _rightAction.GetCardAmount() + " units. "
           );
    }

    //left = 0, right = 1
    public Actions GetSelectedAction(int side)
    {
        if (side == 0)
            return _leftAction.GetSelectedAction();
        else
            return _rightAction.GetSelectedAction();
    }

    public CardType GetCardType(bool left)
    {
        if (left)
            return _leftAction.GetCardType();
        else
            return _rightAction.GetCardType();
    }

    public int GetCardAmount(bool left)
    {
        if (left)
            return _leftAction.GetCardAmount();
        else
            return _rightAction.GetCardAmount();
    }

    public void RecoverActions(bool isLeft, int actionIndex, int actionAmount)
    {
        Actions lAction = null;
        Actions rAction = null;
        switch (actionIndex)
        {
            case 0:
                if (isLeft)
                    lAction = new Arrow();
                else
                    rAction = new Arrow();
                break;
            case 1:
                if (isLeft)
                    lAction = new Magic();
                else
                    rAction = new Magic();
                break;
            case 2:
                if (isLeft)
                    lAction = new Move();
                else
                    rAction = new Move();
                break;
            case 3:
                if (isLeft)
                    lAction = new Shield();
                else
                    rAction = new Shield();
                break;
            case 4:
                if (isLeft)
                    lAction = new Sword();
                else
                    rAction = new Sword();
                break;
        }

        if (isLeft)
        {
            _leftAction.SetCard(lAction);
            _leftAction.GetSelectedAction().InitAction();
            _leftAction.GetSelectedAction().SetCardAmount(actionAmount);
        }
        else
        {
            _rightAction.SetCard(rAction);
            _rightAction.GetSelectedAction().InitAction();
            _rightAction.GetSelectedAction().SetCardAmount(actionAmount);
        }
    }
}
