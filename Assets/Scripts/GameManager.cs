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
        // Warning : be sure that there is only 2 players in the scene
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
        player0Action.ExecuteAction(0, _players[0].GetCurrentBoard());
        player0Action.ExecuteAction(0, _players[1].GetCurrentBoard());
        Actions player1Action = _players[1].GetCurrentCard().GetSelectedAction(_players[1].GetCurrentAction());
        player1Action.ExecuteAction(1, _players[1].GetCurrentBoard());
        player1Action.ExecuteAction(1, _players[0].GetCurrentBoard());
        yield return new WaitForSeconds(2.0f);
        _currentGameState = GameState.EXECUTING;
        _resolving = false;
    }
}
