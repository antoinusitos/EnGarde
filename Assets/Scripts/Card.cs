using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private Actions _leftAction = null;
    private Actions _RightAction = null;

    private int _currentLeftAmount = 0;
    private int _currentRightAmount = 0;

    public void SetCard(Actions type1, int amount1, Actions type2, int amount2)
    {
        _leftAction = type1;
        _currentLeftAmount = amount1;
        _RightAction = type1;
        _currentRightAmount = amount2;
    }

    public void CardToString()
    {
        Debug.Log(
            "Print Card :" +
            "Effect1 = " + _leftAction.GetCardType() + " for " + _currentLeftAmount + " units. " +
            "Effect2 = " + _RightAction.GetCardType() + " for " + _currentRightAmount + " units."
            );
    }

    public void GetAction(bool isLeft, int actionIndex, int actionAmount)
    {
        switch(actionIndex)
        {
            case 0:
                if(isLeft)
                    _leftAction = new Arrow();
                else
                    _RightAction = new Arrow();
                break;
            case 1:
                if (isLeft)
                    _leftAction = new Magic();
                else
                    _RightAction = new Magic();
                break;
            case 2:
                if (isLeft)
                    _leftAction = new Move();
                else
                    _RightAction = new Move();
                break;
            case 3:
                if (isLeft)
                    _leftAction = new Shield();
                else
                    _RightAction = new Shield();
                break;
            case 4:
                if (isLeft)
                    _leftAction = new Sword();
                else
                    _RightAction = new Sword();
                break;
        }

        if (isLeft)
        {
            _leftAction.InitAction();
            _currentLeftAmount = actionAmount;
        }
        else
        {
            _RightAction.InitAction();
            _currentRightAmount = actionAmount;
        }
    }
}