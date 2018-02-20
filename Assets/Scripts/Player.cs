using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Deck _currentDeck = null;

    private int _currentLife = 5;

    private bool _havePlayed = false;

    public int playerNumber = 0;

    private Card _currentCard = null;
    private int _currentAction = -1; // 0 = Left, 1 = Right

    private Board _currentBoard = null;

    public string deckName = "DeckName";
    public bool useDeckName = false;

    public void StartPlayer()
    {
        _currentBoard = GetComponent<Board>();

        _currentLife = 5;
        _currentDeck = new Deck();
        _currentDeck.StartDeck(deckName, useDeckName);
    }

    public Board GetCurrentBoard()
    {
        return _currentBoard;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && playerNumber == 0)
        {
            _havePlayed = true;
            _currentAction = 0;
            UIManager.GetInstance().ShowSelection(playerNumber, true, true);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && playerNumber == 0)
        {
            _havePlayed = true;
            _currentAction = 1;
            UIManager.GetInstance().ShowSelection(playerNumber, false, true);
        }

        else if (Input.GetKeyDown(KeyCode.O) && playerNumber == 1)
        {
            _havePlayed = true;
            _currentAction = 0;
            UIManager.GetInstance().ShowSelection(playerNumber, true, true);
        }
        else if (Input.GetKeyDown(KeyCode.P) && playerNumber == 1)
        {
            _havePlayed = true;
            _currentAction = 1;
            UIManager.GetInstance().ShowSelection(playerNumber, false, true);
        }
    }

    public void PickCard()
    {
        _currentCard = _currentDeck.PickCard();
        UIManager.GetInstance().SetImageSprite(playerNumber, true, _currentCard.GetCardType(true), _currentCard.GetCardAmount(true));
        UIManager.GetInstance().SetImageSprite(playerNumber, false, _currentCard.GetCardType(false), _currentCard.GetCardAmount(false));
        //_currentCard.CardToString();
    }

    public int GetLife()
    {
        return _currentLife;
    }

    public bool GetHavePlayed()
    {
        return _havePlayed;
    }

    public Card GetCurrentCard()
    {
        return _currentCard;
    }

    public int GetCurrentAction()
    {
        return _currentAction;
    }

    public void ResetHavePlayed()
    {
        _havePlayed = false;
        _currentCard = null;
        _currentAction = -1;
        UIManager.GetInstance().ShowSelection(playerNumber, true, false);
        UIManager.GetInstance().ShowSelection(playerNumber, false, false);
    }
}