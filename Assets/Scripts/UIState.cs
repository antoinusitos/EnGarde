using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Make this class a visual for the board
public class UIState : MonoBehaviour
{
    public Transform[] positions = null;

    public Image player0State = null;
    public Image player1State = null;

    private float offsetPlayer = 20.0f;

    private int player0Pos = 0;
    private int player1Pos = 0;

    private void Start()
    {
        player0Pos = 0;
        player1Pos = positions.Length - 1;

        Vector3 pos = positions[player0Pos].transform.position;
        pos.y += offsetPlayer;
        player0State.transform.position = pos;
        pos = positions[player1Pos].transform.position;
        pos.y += offsetPlayer;
        player1State.transform.position = pos;
    }

    public void SetPlayerPos(int player, int pos)
    {
        if(player == 0)
        {
            player0Pos += pos;

            player0Pos = Mathf.Clamp(player0Pos, 0, positions.Length - 1);

            Vector3 newPosition = positions[player0Pos].transform.position;
            newPosition.y += offsetPlayer;
            player0State.transform.position = newPosition;
        }
        else if (player == 1)
        {
            player1Pos -= pos;

            player1Pos = Mathf.Clamp(player1Pos, 0, positions.Length - 1);

            Vector3 newPosition = positions[player1Pos].transform.position;
            newPosition.y += offsetPlayer;
            player1State.transform.position = newPosition;
        }
    }
}
