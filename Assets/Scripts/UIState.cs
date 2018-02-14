using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Make this class a visual for the board
public class UIState : MonoBehaviour
{
    public Image player0State = null;
    public Image player1State = null;

    private float offsetPlayer = 20.0f;

    public Board board = null;

    private void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            Vector3 pos = board.GetPlayerPos(i);
            pos.y += offsetPlayer;
            if(i == 0)
                player0State.transform.position = pos;
            else
                player1State.transform.position = pos;
        }
    }
}
