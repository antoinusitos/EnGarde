using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Deck _currentDeck = null;

    public void StartPlayer()
    {
        _currentDeck = new Deck();
        _currentDeck.StartDeck();
    }
}