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

    public UIDeckSelection uiDeckSelection = null;
    public GameObject uiGame = null;

    public GameObject uiEndGame = null;

    private void Start()
    {
        _boardManager = BoardManager.GetInstance();
        _boardManager.Init();

        uiDeckSelection.gameObject.SetActive(true);
        uiGame.SetActive(false);

        StartDeckSelection();
    }

    public void DamagePlayer(int player, int damage)
    {
        _players[player].TakeDamage(damage);
        if(_players[player].GetLife() <= 0)
        {
            Debug.Log("Player " + player + " lose the game");
        }
    }

    public void SetPlayerDeck(int player, string deckName)
    {
        _players[player].deckName = deckName;
        _players[player].useDeckName = true;
    }

    private void StartDeckSelection()
    {
        DeckInfos[] deckInfos = FileReader.GetInstance().GetDeckInfos();

        /*for(int i = 0; i < deckInfos.Length; i++)
        {
            Debug.Log(deckInfos[i].deckName + " with size " + deckInfos[i].deckSize);
        }*/

        uiDeckSelection.ShowDeckAvailable(deckInfos);
    }

    public void StartGame()
    {
        uiDeckSelection.gameObject.SetActive(false);
        uiGame.SetActive(true);

        _players[0].StartPlayer();
        _players[1].StartPlayer();

        if (!_players[0].GetCurrentDeck().GetDeckLimitOK())
        {
            Debug.Log("players 0 deck is off limits !");
            return;
        }
        if (!_players[1].GetCurrentDeck().GetDeckLimitOK())
        {
            Debug.Log("players 1 deck is off limits !");
            return;
        }

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

                if(Input.GetKeyDown(KeyCode.S))
                {
                    FileReader.GetInstance().SaveDeck("DeckLol", _players[0].GetCurrentDeck());
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
                uiEndGame.SetActive(true);
                if (_players[0].GetLife() <= 0)
                    uiEndGame.GetComponent<EndGame>().SetWinner(1);
                else
                    uiEndGame.GetComponent<EndGame>().SetWinner(0);
                break;
        }
    }

    public void StartAgain()
    {
        uiEndGame.SetActive(false);
        _players[0].ResetPlayer();
        _players[1].ResetPlayer();
        _boardManager.ResetBoard();
        Debug.Log("Starting Game !");
        _currentGameState = GameState.DISTRIBUTING;
    }

    public void SetGameState(GameState newState)
    {
        _currentGameState = newState;
    }

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
