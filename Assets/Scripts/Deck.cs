using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{ 
    private Card[] _allCards = null;

    private int limitValue = 100;

    private bool _deckLimitOk = false;

    public void StartDeck(string deckName, bool useDeckName)
    {
        _allCards = new Card[10];
        if (useDeckName)
        {
            LoadDeck(deckName);
            ShuffleDeck();
        }
        else
            GenerateDeck();
    }

    public Card[] GetCards()
    {
        return _allCards;
    }

    private void LoadDeck(string deckName)
    {
        //File Reader to Get back a deck
        _allCards = FileReader.GetInstance().ReadFile(deckName);

        Debug.Log("check limit for deck : " + deckName);
        CheckDeckLimit();
    }

    public bool GetDeckLimitOK()
    {
        return _deckLimitOk;
    }

    private int GetTypeValue(CardType type, int strenght)
    {
        switch (type)
        {
            case CardType.ARROW:
                return 15;

            case CardType.MAGIC:
                return 4 * strenght;

            case CardType.MOVE:
                return strenght;

            case CardType.SHIELD:
                return strenght;

            case CardType.SWORD:
                return 4 * strenght;
        }
        return 0;
    }

    private void CheckDeckLimit()
    {
        int finalValue = 0;
        for (int i = 0; i < _allCards.Length; i++)
        {
            finalValue += GetTypeValue(_allCards[i].GetCardType(true), _allCards[i].GetCardAmount(true));
            finalValue += GetTypeValue(_allCards[i].GetCardType(false), _allCards[i].GetCardAmount(false));
        }

        if (finalValue > limitValue)
        {
            Debug.Log("deck value > "+ limitValue +" with " + finalValue);
        }
        else
        {
            Debug.Log("deck value <= " + limitValue + "  with " + finalValue);
            _deckLimitOk = true;
        }
    }

    private void GenerateDeck()
    {
        for(int i = 0; i < _allCards.Length; i++)
        {
            int random1 = Random.Range(2, 5);
            int random2 = Random.Range(2, 5);
            while(random2 == random1)
                random2 = Random.Range(2, 5);

            _allCards[i] = new Card();
            _allCards[i].RecoverActions(true, random1, Random.Range(1, 5));
            _allCards[i].RecoverActions(false, random2, Random.Range(1, 5));
        }

        CheckDeckLimit();
    }

    public Card PickCard()
    {
        Card temp = _allCards[0];

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
            Card temp = _allCards[rand];
            _allCards[rand] = _allCards[i];
            _allCards[i] = temp;
        }
    }
}