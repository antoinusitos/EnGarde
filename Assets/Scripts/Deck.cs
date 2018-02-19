using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{ 
    private Card[] _allCards = null;

    public void StartDeck()
    {
        LoadDeck();
        GenerateDeck();
    }

    private void LoadDeck()
    {
        //File Reader to Get back a deck
    }

    private void GenerateDeck()
    {
        _allCards = new Card[8];

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
}