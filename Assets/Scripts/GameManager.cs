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
    private Player[] _players = new Player[2];

    private GameState _currentGameState = GameState.INIT;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        // Warning : be sure that there is only 2 players in the scene
        _players = FindObjectsOfType<Player>();
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
                Debug.Log("Player 1 :");
                _players[0].PickCard();
                Debug.Log("Player 2 :");
                _players[1].PickCard();

                // Show card for both players here

                Debug.Log("");
                _currentGameState = GameState.WAITINGFORPLAYERS;
                break;

            case GameState.WAITINGFORPLAYERS:
                if (_players[0].GetHavePlayed() && _players[1].GetHavePlayed())
                {
                    _currentGameState = GameState.EXECUTING;
                }
                break;

            case GameState.EXECUTING:
                Debug.Log("Players have played !");
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
}
