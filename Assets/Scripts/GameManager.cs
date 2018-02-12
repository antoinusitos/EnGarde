using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player[] _players = new Player[2];

    private void StartGame()
    {
        // Warning : be sure that there is only 2 players in the scene
        _players = FindObjectsOfType<Player>(); 
    }
}
