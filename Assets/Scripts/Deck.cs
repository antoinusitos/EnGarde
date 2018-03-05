using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{ 
    private Card1[] _allCards = null;

    private int limitValue = 100;

    private bool _deckLimitOk = false;

    private int lastCost = 0;

    public void StartDeck(string deckName, bool useDeckName)
    {
        _allCards = new Card1[10];
        if (useDeckName)
        {
            LoadDeck(deckName);
            ShuffleDeck();
        }
        else
            GenerateDeck();
    }

    public Card1[] GetCards()
    {
        return _allCards;
    }

    public void InitDeck()
    {
        _allCards = new Card1[10];
    }

    public void AddCard(Card1 c, int index)
    {
        _allCards[index] = c;
    }

    private void LoadDeck(string deckName)
    {
        //File Reader to Get back a deck
        _allCards = FileReader.GetInstance().ReadFile(deckName);

        //Debug.Log("check limit for deck : " + deckName);
        CheckDeckLimit();
    }

    public bool GetDeckLimitOK()
    {
        return _deckLimitOk;
    }

    public int GetLastCost()
    {
        return lastCost;
    }

    public void CheckDeckLimit()
    {
        int finalValue = 0;

        for (int i = 0; i < _allCards.Length; i++)
        {
            finalValue += Data.GetTypeValue(_allCards[i].GetCardType(), _allCards[i].GetCardAmount());
        }

        if (finalValue > limitValue)
        {
            Debug.Log("deck value > "+ limitValue +" with " + finalValue);
        }
        else if(_allCards.Length > 10 || _allCards.Length < 1)
        {
            Debug.Log("number of cards is not between 1 and 10");
        }
        else
        {
            Debug.Log("deck value <= " + limitValue + "  with " + finalValue);
            _deckLimitOk = true;
        }

        lastCost = finalValue;
    }

    private void GenerateDeck()
    {
        for(int i = 0; i < _allCards.Length; i++)
        {
            int random1 = Random.Range(2, 5);

            _allCards[i] = new Card1();
            _allCards[i].RecoverActions(random1, Random.Range(1, 5));
        }

        CheckDeckLimit();
    }

    public Card1 PickCard()
    {
        Card1 temp = _allCards[0];

        for(int i = 1; i < _allCards.Length; i++)
        {
            _allCards[i - 1] = _allCards[i];
        }

        _allCards[_allCards.Length - 1] = temp;

        return temp;
    }

    public void ShuffleDeck()
    {
        for(int i = 0; i < _allCards.Length; i++)
        {
            int rand = Random.Range(0, _allCards.Length);
            Card1 temp = _allCards[rand];
            _allCards[rand] = _allCards[i];
            _allCards[i] = temp;
        }
    }

    public void DebugDeck()
    {
        for(int i = 0; i < _allCards.Length; i++)
        {
            _allCards[i].CardToString();
        }
    }
}