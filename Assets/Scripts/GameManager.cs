using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    INIT,
    DISTRIBUTING,
    WAITINGFORPLAYERS,
    EXECUTING,
    ENDGAME,

}

public class GameManager : MonoBehaviour
{
    public Player[] _players = new Player[2];

    private GameState _currentGameState = GameState.INIT;

    private bool _resolving = false;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _players[0].StartPlayer();
        _players[1].StartPlayer();

        Debug.Log("Starting Game !");
        _currentGameState = GameState.DISTRIBUTING;
    }

    private void Update()
    {
        switch(_currentGameState)
        {
            case GameState.DISTRIBUTING:
                Debug.Log("New turn !");
                //Debug.Log("Player 1 :");
                _players[0].PickCard();
                //Debug.Log("Player 2 :");
                _players[1].PickCard();

                // Show card for both players here

                Debug.Log("");
                _currentGameState = GameState.WAITINGFORPLAYERS;
                break;

            case GameState.WAITINGFORPLAYERS:
                if (_players[0].GetHavePlayed() && _players[1].GetHavePlayed() && !_resolving)
                {
                    _resolving = true;

                    //Debug.Log("player 1 played :");
                    //_players[0].GetCurrentCard().SideToString(_players[0].GetCurrentAction());

                    //Debug.Log("player 2 played :");
                   // _players[1].GetCurrentCard().SideToString(_players[1].GetCurrentAction());

                    StartCoroutine("ShowResolution");
                }
                break;

            case GameState.EXECUTING:
                _players[0].ResetHavePlayed();
                _players[1].ResetHavePlayed();
                _currentGameState = GameState.DISTRIBUTING;

                if(_players[0].GetLife() <= 0 || _players[1].GetLife() <= 0)
                {
                    Debug.Log("End Game");
                    _currentGameState = GameState.ENDGAME;
                }
                break;

            case GameState.ENDGAME:

                break;
        }
    }

    private IEnumerator ShowResolution()
    {
        Actions player0Action = _players[0].GetCurrentCard().GetSelectedAction(_players[0].GetCurrentAction());
        Actions player1Action = _players[1].GetCurrentCard().GetSelectedAction(_players[1].GetCurrentAction());

        int resolution = Resolution(player0Action, player1Action);

        if(resolution == -1)
        {
            Debug.Log("player 0 win turn");
            player0Action.ExecuteAction(0, _players[0].GetCurrentBoard());
            player0Action.ExecuteAction(0, _players[1].GetCurrentBoard());
        }
        else if (resolution == 0)
        {
            Debug.Log("both players win turn");
            player0Action.ExecuteAction(0, _players[0].GetCurrentBoard());
            player0Action.ExecuteAction(0, _players[1].GetCurrentBoard());
            player1Action.ExecuteAction(1, _players[1].GetCurrentBoard());
            player1Action.ExecuteAction(1, _players[0].GetCurrentBoard());
        }
        else if (resolution == 1)
        {
            Debug.Log("player 1 win turn");
            player1Action.ExecuteAction(1, _players[1].GetCurrentBoard());
            player1Action.ExecuteAction(1, _players[0].GetCurrentBoard());
        }
        else if (resolution == 3)
        {
            Debug.Log("both players were pushed back !");
            // Special case for two sword equals => push back each players
            _players[0].GetCurrentBoard().SetPlayerPos(0, -player0Action.GetCardAmount());
            _players[0].GetCurrentBoard().SetPlayerPos(1, -player0Action.GetCardAmount());
            _players[1].GetCurrentBoard().SetPlayerPos(0, -player0Action.GetCardAmount());
            _players[1].GetCurrentBoard().SetPlayerPos(1, -player0Action.GetCardAmount());
        }
        else
        {
            Debug.Log("Nothing happended !");
        }


        yield return new WaitForSeconds(2.0f);
        //play animation here
        _currentGameState = GameState.EXECUTING;
        _resolving = false;
    }

    // return -1 if player 0 must execute his action
    // return 0 if both players must execute their action
    // return 1 if player 1 must execute his action
    // return 2 if nothing append
    // return 3 for special action
    private int Resolution(Actions player0Action, Actions player1Action)
    {
        switch(player0Action.GetCardType())
        {
            case CardType.MAGIC:
                #region Magic
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        if (player0Action.GetCardAmount() > player1Action.GetCardAmount())
                            return -1;
                        else if (player0Action.GetCardAmount() < player1Action.GetCardAmount())
                            return 1;
                        else
                            return 2;

                    case CardType.SWORD:
                        return -1;

                    case CardType.ARROW:
                        return 1;

                    case CardType.SHIELD:
                        return -1;

                    case CardType.MOVE:
                        return 2;
                }
                #endregion
                break;

            case CardType.ARROW:
                #region Arrow
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return -1;

                    case CardType.SWORD:
                        return 1;

                    case CardType.ARROW:
                        return 2;

                    case CardType.SHIELD:
                        return 2;

                    case CardType.MOVE:
                        return -1;
                }
                #endregion
                break;

            case CardType.MOVE:
                #region Move
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return 2;

                    case CardType.SWORD:
                        return 1;

                    case CardType.ARROW:
                        return 1;

                    case CardType.SHIELD:
                        return 2;

                    case CardType.MOVE:
                        return 0;
                }
                #endregion
                break;

            case CardType.SHIELD:
                #region Shield
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return 1;

                    case CardType.SWORD:
                        if (player0Action.GetCardAmount() > player1Action.GetCardAmount())
                            return -1;
                        else if (player0Action.GetCardAmount() < player1Action.GetCardAmount())
                            return 1;
                        else
                            return 2;

                    case CardType.ARROW:
                        return 2;

                    case CardType.SHIELD:
                        return 2;

                    case CardType.MOVE:
                        return 2;
                }
                #endregion
                break;

            case CardType.SWORD:
                #region Sword
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return 1;

                    case CardType.SWORD:
                        if (player0Action.GetCardAmount() > player1Action.GetCardAmount())
                            return -1;
                        else if (player0Action.GetCardAmount() < player1Action.GetCardAmount())
                            return 1;
                        else
                            return 3;

                    case CardType.ARROW:
                        return -1;

                    case CardType.SHIELD:
                        if (player0Action.GetCardAmount() > player1Action.GetCardAmount())
                            return -1;
                        else if (player0Action.GetCardAmount() < player1Action.GetCardAmount())
                            return 1;
                        else
                            return 2;

                    case CardType.MOVE:
                        return -1;
                }
                #endregion
                break;
        }

        return 2;
    }
}
