using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Deck _currentDeck = null;

    private int _currentLife = 3;

    private bool _havePlayed = false;

    public int playerNumber = 0;

    public void StartPlayer()
    {
        _currentLife = 3;
        _currentDeck = new Deck();
        _currentDeck.StartDeck();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && playerNumber == 0)
        {
            _havePlayed = true;

        }
        else if (Input.GetKeyDown(KeyCode.Z) && playerNumber == 0)
        {
            _havePlayed = true;

        }

        else if (Input.GetKeyDown(KeyCode.O) && playerNumber == 1)
        {
            _havePlayed = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && playerNumber == 1)
        {
            _havePlayed = true;

        }
    }

    public void PickCard()
    {
        _currentDeck.PickCard().CardToString();
    }

    public int GetLife()
    {
        return _currentLife;
    }

    public bool GetHavePlayed()
    {
        return _havePlayed;
    }

    public void ResetHavePlayed()
    {
        _havePlayed = false;
    }
}