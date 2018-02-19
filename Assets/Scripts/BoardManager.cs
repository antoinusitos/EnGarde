using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int positions = 10;

    private bool _resolving = false;

    private Board _masterBoard = null;

    public void Init()
    {
        _masterBoard = new Board();
        _masterBoard.StartBoard();
    }

    public bool GetResolving()
    {
        return _resolving;
    }

    public void Resolve(Card player0Card, Card player1Card, int player0Choice, int player1Choice)
    {
        if (_resolving) return;

        _resolving = true;

        StartCoroutine(ShowResolution(player0Card, player1Card, player0Choice, player1Choice));
    }

    private IEnumerator ShowResolution(Card player0Card, Card player1Card, int player0Choice, int player1Choice)
    {
        Actions player0Action = player0Card.GetSelectedAction(player0Choice);
        Actions player1Action = player1Card.GetSelectedAction(player1Choice);

        player0Action.InitResolution();
        player1Action.InitResolution();

        //Go at least one time in the loop
        while (player0Action.GetResolutionAmount() > 0 || player1Action.GetResolutionAmount() > 0)
        {
            if (player0Action.GetCanAct())
            {
                player0Action.ExecuteAction(0, _masterBoard);
            }
            player0Action.UpdateAction();
            if (player1Action.GetCanAct())
            {
                player1Action.ExecuteAction(1, _masterBoard);
            }
            player1Action.UpdateAction();
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);
        player0Action.ResetAction();
        player1Action.ResetAction();

        _masterBoard.DebugBoard();
        GameManager.GetInstance().SetGameState(GameState.ENDTURN);
        _resolving = false;
    }

    public Board GetMasterBoard()
    {
        return _masterBoard;
    }

    private void Update()
    {
        _masterBoard.UpdateBoard();
    }

    // return -1 if player 0 must execute his action
    // return 0 if both players must execute their action
    // return 1 if player 1 must execute his action
    // return 2 if nothing append
    // return 3 for special action
    /*private int Resolution(Actions player0Action, Actions player1Action)
    {
        switch (player0Action.GetCardType())
        {
            case CardType.MAGIC:
                #region Magic
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        if (player0Action.GetCardAmount() > player1Action.GetCardAmount())
                            return -1;
                        else if (player0Action.GetCardAmount() < player1Action.GetCardAmount())
                            return 1;
                        else
                            return 2;

                    case CardType.SWORD:
                        return -1;

                    case CardType.ARROW:
                        return 1;

                    case CardType.SHIELD:
                        return -1;

                    case CardType.MOVE:
                        return 2;
                }
                #endregion
                break;

            case CardType.ARROW:
                #region Arrow
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return -1;

                    case CardType.SWORD:
                        return 1;

                    case CardType.ARROW:
                        return 2;

                    case CardType.SHIELD:
                        return 2;

                    case CardType.MOVE:
                        return -1;
                }
                #endregion
                break;

            case CardType.MOVE:
                #region Move
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return 2;

                    case CardType.SWORD:
                        return 1;

                    case CardType.ARROW:
                        return 1;

                    case CardType.SHIELD:
                        return 2;

                    case CardType.MOVE:
                        return 0;
                }
                #endregion
                break;

            case CardType.SHIELD:
                #region Shield
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return 1;

                    case CardType.SWORD:
                        if (player0Action.GetCardAmount() > player1Action.GetCardAmount())
                            return -1;
                        else if (player0Action.GetCardAmount() < player1Action.GetCardAmount())
                            return 1;
                        else
                            return 2;

                    case CardType.ARROW:
                        return 2;

                    case CardType.SHIELD:
                        return 2;

                    case CardType.MOVE:
                        return 2;
                }
                #endregion
                break;

            case CardType.SWORD:
                #region Sword
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return 1;

                    case CardType.SWORD:
                        if (player0Action.GetCardAmount() > player1Action.GetCardAmount())
                            return -1;
                        else if (player0Action.GetCardAmount() < player1Action.GetCardAmount())
                            return 1;
                        else
                            return 3;

                    case CardType.ARROW:
                        return -1;

                    case CardType.SHIELD:
                        if (player0Action.GetCardAmount() > player1Action.GetCardAmount())
                            return -1;
                        else if (player0Action.GetCardAmount() < player1Action.GetCardAmount())
                            return 1;
                        else
                            return 2;

                    case CardType.MOVE:
                        return -1;
                }
                #endregion
                break;
        }

        return 2;
    }*/


    #region Singleton
    // SINGLETON

    private static BoardManager _instance = null;

    public static BoardManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion
}
