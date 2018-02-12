using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private Actions[] _allActions = null;

    public void StartDeck()
    {
        GenerateActions();
        LoadDeck();
    }

    private void GenerateActions()
    {
        _allActions = new Actions[5];
        // Create all actions here..
    }

    private void LoadDeck()
    {
        //File Reader to Get back a deck
    }
}