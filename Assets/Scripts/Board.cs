using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int positions = 9;

    private int player0Pos = -1;
    private int player1Pos = -1;

    private BoardReplication[] _boardReplications;

    public void StartBoard()
    {
        player0Pos = 0;
        player1Pos = positions - 1;

        _boardReplications = FindObjectsOfType<BoardReplication>();

        for (int i = 0; i < _boardReplications.Length; i++)
        {
            _boardReplications[i].SetPlayerPos(0, player0Pos);
            _boardReplications[i].SetPlayerPos(1, player1Pos);
        }
    }

    // return 0 = movement done
    // return >0 = movement not done
    // return -1 = player index fail
    public int CalcPlayerPos(int player, int pos)
    {
        if (player == 0)
        {
            int tempPos = player0Pos;

            for (int i = 0; i < Mathf.Abs(pos); i++)
            {
                int sign = pos > 0 ? 1 : -1;
                int calc = tempPos + (1 * sign);

                if (calc >= 0 && calc <= positions - 1 && calc != player1Pos)
                {
                    tempPos = calc;
                }
                else break;
            }

            return tempPos;
        }
        else if (player == 1)
        {
            int tempPos = player1Pos;

            for (int i = 0; i < Mathf.Abs(pos); i++)
            {
                int sign = pos > 0 ? -1 : 1;
                int calc = tempPos + (1 * sign);

                if (calc >= 0 && calc <= positions - 1 && calc != player0Pos)
                {
                    tempPos = calc;
                }
                else break;
            }

            return tempPos;
        }

        return -1;
    }

    public int GetPlayerPos(int player) { return player == 0 ? player0Pos : player1Pos; }

    public void SetPlayerPos(int player, int movement)
    {
        if (player == 0)
        {
            player0Pos = movement;
            player0Pos = Mathf.Clamp(player0Pos, 0, positions - 1);
            //Ask Boards replication to move the visual of the players
            for (int i = 0; i < _boardReplications.Length; i++)
            {
                _boardReplications[i].SetPlayerPos(0, movement);
            }
        }
        else if (player == 1)
        {
            player1Pos = movement;
            player1Pos = Mathf.Clamp(player1Pos, 0, positions - 1);
            //Ask Boards replication to move the visual of the players
            for (int i = 0; i < _boardReplications.Length; i++)
            {
                _boardReplications[i].SetPlayerPos(1, movement);
            }
        }
    }

    public void UpdateBoard()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            DebugBoard();
        }
    }

    public void DebugBoard()
    {
        Debug.Log("player0Pos :" + player0Pos + ", player1Pos :" + player1Pos);
    }

    //Get number of step to reach the other player
    public int GetDistancePlayers()
    {
        return player1Pos - player0Pos;
    }
}