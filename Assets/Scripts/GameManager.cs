using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    INIT,
    DISTRIBUTING,
    WAITINGFORPLAYERS,
    ENDTURN,
    ENDGAME,

}

public class GameManager : MonoBehaviour
{
    public Player[] _players = new Player[2];

    private GameState _currentGameState = GameState.INIT;

    private BoardManager _boardManager = null;

    private void Start()
    {
        _boardManager = BoardManager.GetInstance();
        _boardManager.Init();
        StartGame();
    }

    public void DamagePlayer(int player, int damage)
    {
        _players[player].TakeDamage(damage);
        if(_players[player].GetLife() <= 0)
        {
            Debug.Log("Player " + player + " lose the game");
        }
    }

    private void StartGame()
    {
        _players[0].StartPlayer();
        _players[1].StartPlayer();

        if (!_players[0].GetCurrentDeck().GetDeckLimitOK()) return;
        if (!_players[1].GetCurrentDeck().GetDeckLimitOK()) return;

        Debug.Log("Starting Game !");
        _currentGameState = GameState.DISTRIBUTING;
    }

    private void Update()
    {
        switch(_currentGameState)
        {
            case GameState.DISTRIBUTING:
                Debug.Log("New turn !");
                _players[0].PickCard();
                _players[1].PickCard();

                // Show card for both players here

                Debug.Log("");
                _currentGameState = GameState.WAITINGFORPLAYERS;
                break;

            case GameState.WAITINGFORPLAYERS:
                if (_players[0].GetHavePlayed() && _players[1].GetHavePlayed())
                {
                    _boardManager.Resolve(
                        _players[0].GetCurrentCard(), _players[1].GetCurrentCard(),
                        _players[0].GetCurrentAction(), _players[1].GetCurrentAction());
                }
                break;

            case GameState.ENDTURN:
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

    public void SetGameState(GameState newState)
    {
        _currentGameState = newState;
    }

    // return -1 if player 0 must execute his action
    // return 0 if both players must execute their action
    // return 1 if player 1 must execute his action
    // return 2 if nothing append
    // return 3 for special action
    /*private int Resolution(Actions player0Action, Actions player1Action)
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
    }*/

    private static GameManager _instance = null;

    public static GameManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
