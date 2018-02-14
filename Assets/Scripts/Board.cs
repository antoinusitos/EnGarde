using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform[] positions = null;

    private int player0Pos = -1;
    private int player1Pos = -1;

    private void Start()
    {
        player0Pos = 0;
        player1Pos = positions.Length - 1;
    }

    public int SetPlayerPos(int player, int pos)
    {
        if (player == 0)
        {
            int toReturn = pos;

            for (int i = 0; i < Mathf.Abs(pos); i++)
            {
                int sign = pos > 0 ? 1 : -1;
                int calc = player0Pos + (1 * sign);

                if (calc >= 0 && calc <= positions.Length - 1 && calc != player1Pos)
                {
                    player0Pos = calc;
                    toReturn--;
                }
                else break;
            }

            return toReturn;
        }
        else if (player == 1)
        {
            int toReturn = pos;

            for (int i = 0; i < Mathf.Abs(pos); i++)
            {
                int sign = pos > 0 ? -1 : 1;
                int calc = player1Pos + (1 * sign);

                if (calc >= 0 && calc <= positions.Length - 1 && calc != player0Pos)
                {
                    player1Pos = calc;
                    toReturn--;
                }
                else break;
            }

            return toReturn;
        }

        return -1;
    }

    public Vector3 GetPlayerPos(int player)
    {
        return player == 0 ? positions[player0Pos].transform.position : positions[player1Pos].transform.position;
    }
}