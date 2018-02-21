using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Actions
{
    public override void InitAction()
    {
        _currentType = CardType.SWORD;
    }



    //TODO : Second resolution for Sword (damage and action)

    public override void ExecuteAction(int fromPlayer, Board currentBoard, Actions enemyAction)
    {
        Debug.Log("play SWORD :" + _resolutionAmount);
        int basePos = currentBoard.GetPlayerPos(fromPlayer);
        int newPos = currentBoard.CalcPlayerPos(fromPlayer, 1);
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

            switch (enemyAction.GetCardType())
            {
                case CardType.ARROW:
                    int otherPlayer = fromPlayer == 0 ? 1 : 0;
                    newPos = currentBoard.CalcPlayerPos(otherPlayer, -_resolutionAmount);
                    currentBoard.SetPlayerPos(otherPlayer, newPos);
                    GameManager.GetInstance().DamagePlayer(otherPlayer, _resolutionAmount);
                    break;
                case CardType.SWORD:
                    if(_resolutionAmount > enemyAction.GetResolutionAmount())
                    {
                        otherPlayer = fromPlayer == 0 ? 1 : 0;
                        newPos = currentBoard.CalcPlayerPos(otherPlayer, -_resolutionAmount);
                        currentBoard.SetPlayerPos(otherPlayer, newPos);
                        GameManager.GetInstance().DamagePlayer(otherPlayer, _resolutionAmount);
                    }
                    else if (_resolutionAmount < enemyAction.GetResolutionAmount())
                    {
                        newPos = currentBoard.CalcPlayerPos(fromPlayer, -enemyAction.GetResolutionAmount());
                        currentBoard.SetPlayerPos(fromPlayer, newPos);
                        GameManager.GetInstance().DamagePlayer(fromPlayer, enemyAction.GetResolutionAmount());
                    }
                    else
                    {
                        currentBoard.SetPlayerPos(fromPlayer, -_resolutionAmount);
                        otherPlayer = fromPlayer == 0 ? 1 : 0;
                        newPos = currentBoard.CalcPlayerPos(otherPlayer, -_resolutionAmount);
                        currentBoard.SetPlayerPos(otherPlayer, newPos);
                        GameManager.GetInstance().DamagePlayer(otherPlayer, _resolutionAmount);
                    }
                    break;
                case CardType.MOVE:
                    otherPlayer = fromPlayer == 0 ? 1 : 0;
                    newPos = currentBoard.CalcPlayerPos(otherPlayer, -_resolutionAmount);
                    currentBoard.SetPlayerPos(otherPlayer, newPos);
                    GameManager.GetInstance().DamagePlayer(otherPlayer, _resolutionAmount);
                    break;
                case CardType.SHIELD:
                    if(_resolutionAmount > enemyAction.GetCardAmount())
                    {
                        otherPlayer = fromPlayer == 0 ? 1 : 0;
                        newPos = currentBoard.CalcPlayerPos(otherPlayer, -_resolutionAmount);
                        currentBoard.SetPlayerPos(otherPlayer, newPos);
                        GameManager.GetInstance().DamagePlayer(otherPlayer, _resolutionAmount);
                    }
                    else
                    {
                        otherPlayer = fromPlayer == 0 ? 1 : 0;
                        newPos = currentBoard.CalcPlayerPos(otherPlayer, -_resolutionAmount);
                        currentBoard.SetPlayerPos(otherPlayer, newPos);
                        GameManager.GetInstance().DamagePlayer(otherPlayer, _resolutionAmount);
                    }
                    break;
            }


            BoardManager.GetInstance().SetMustEndTurn();

            StopAction();
            _resolutionAmount = 0;
        }
    }
}