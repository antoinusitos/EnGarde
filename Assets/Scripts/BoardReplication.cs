using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardReplication : MonoBehaviour
{
    //USE THIS CLASS TO REPLICATE THE MASTER BOARD
    //LAUNCH ANIMATIONS
    //SET THE POSITIONS WITH THE OFFSET

    public Transform[] positions = null;

    private int player0Pos = -1;
    private int player1Pos = -1;

    public void SetPlayerPos(int playerIndex, int newPos)
    {
        if (playerIndex == 0)
            player0Pos = newPos;
        else
            player1Pos = newPos;
    }

    public Vector3 GetPlayerPos(int playerIndex)
    {
        if (playerIndex == 0)
            return positions[player0Pos].position;
        else
            return positions[player1Pos].position;
    }
}
