using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private Actions _leftAction = null;
    private Actions _rightAction = null;

    /*private int _currentLeftAmount = 0;
    private int _currentRightAmount = 0;*/

    public void SetCard(Actions type1, int amount1, Actions type2, int amount2)
    {
        _leftAction = type1;
        //_currentLeftAmount = amount1;
        _rightAction = type1;
        //_currentRightAmount = amount2;
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
        if(side == 0)
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
            return _leftAction;
        else
            return _rightAction;
    }

    public void RecoverActions(bool isLeft, int actionIndex, int actionAmount)
    {
        switch(actionIndex)
        {
            case 0:
                if(isLeft)
                    _leftAction = new Arrow();
                else
                    _rightAction = new Arrow();
                break;
            case 1:
                if (isLeft)
                    _leftAction = new Magic();
                else
                    _rightAction = new Magic();
                break;
            case 2:
                if (isLeft)
                    _leftAction = new Move();
                else
                    _rightAction = new Move();
                break;
            case 3:
                if (isLeft)
                    _leftAction = new Shield();
                else
                    _rightAction = new Shield();
                break;
            case 4:
                if (isLeft)
                    _leftAction = new Sword();
                else
                    _rightAction = new Sword();
                break;
        }

        if (isLeft)
        {
            _leftAction.InitAction();
            _leftAction.SetCardAmount(actionAmount);
        }
        else
        {
            _rightAction.InitAction();
            _rightAction.SetCardAmount(actionAmount);
        }
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
}