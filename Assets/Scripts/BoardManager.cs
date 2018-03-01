using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int positions = 10;

    private bool _resolving = false;

    private Board _masterBoard = null;

    private bool _mustEndTurn = false;

    public void Init()
    {
        _masterBoard = GetComponent<Board>();
        _masterBoard.StartBoard();
    }

    public bool GetResolving()
    {
        return _resolving;
    }

    public void SetMustEndTurn()
    {
        _mustEndTurn = true;
    }

    public void Resolve(RecomposedCard player0Card, RecomposedCard player1Card, int player0Choice, int player1Choice)
    {
        if (_resolving) return;

        _resolving = true;

        StartCoroutine(ShowResolution(player0Card, player1Card, player0Choice, player1Choice));
    }

    public void ResetBoard()
    {
        _resolving = false;
        _mustEndTurn = false;
        _masterBoard.ResetBoard();
    }

    private IEnumerator ShowResolution(RecomposedCard player0Card, RecomposedCard player1Card, int player0Choice, int player1Choice)
    {
        Actions player0Action = player0Card.GetSelectedAction(player0Choice);
        Actions player1Action = player1Card.GetSelectedAction(player1Choice);

        player0Action.InitResolution();
        player1Action.InitResolution();

        //Go at least one time in the loop
        while ((player0Action.GetResolutionAmount() > 0 || player1Action.GetResolutionAmount() > 0) && !_mustEndTurn)
        {
            //The first time we go there
            if (player0Action.GetCanAct() && player1Action.GetCanAct())
            {
                int resolution = Resolution(player0Action, player1Action);

                Debug.Log("resolution " + resolution);

                if (resolution == 0)
                {
                    player0Action.ExecuteAction(0, _masterBoard, player1Action);
                    player1Action.ExecuteAction(1, _masterBoard, player0Action);
                }
                else if (resolution == -1)
                {
                    player0Action.ExecuteAction(0, _masterBoard, player1Action);
                }
                else if (resolution == 1)
                {
                    player1Action.ExecuteAction(1, _masterBoard, player0Action);
                }
                else if (resolution == 3) // special case
                {
                    player0Action.ExecuteAction(0, _masterBoard, player1Action);
                    player1Action.ExecuteAction(1, _masterBoard, player0Action);
                    break;
                }
                else
                {
                    _mustEndTurn = true;
                    break; // end the turn
                }
            }
            else if (player0Action.GetCanAct())
            {
                player0Action.ExecuteAction(0, _masterBoard, player1Action);
            }
            else if(player1Action.GetCanAct())
            {
                player1Action.ExecuteAction(1, _masterBoard, player0Action);
            }
            player0Action.UpdateAction();
            player1Action.UpdateAction();
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);
        player0Action.ResetAction();
        player1Action.ResetAction();
        _mustEndTurn = false;

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
    // return 2 if nothing append (no animation)
    // return 3 for special action
    private int Resolution(Actions player0Action, Actions player1Action)
    {
        switch (player0Action.GetCardType())
        {
            case CardType.MAGIC:
                #region Magic
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        if (player0Action.GetCardAmount() > player1Action.GetCardAmount())
                            return -1; // magic player 0 win
                        else if (player0Action.GetCardAmount() < player1Action.GetCardAmount())
                            return 1; // magic player 1 win
                        else
                            return 2; // players cancel each other

                    case CardType.SWORD:
                        return -1; // magic win

                    case CardType.ARROW:
                        return 1; // arrow win

                    case CardType.SHIELD:
                        return -1; // magic win

                    case CardType.MOVE:
                        return 1; // move win
                }
                #endregion
                break;

            case CardType.ARROW:
                #region Arrow
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return -1; // arrow win

                    case CardType.SWORD:
                        return 1; // sword win (Move or attack if possible)

                    case CardType.ARROW:
                        return 2; // players cancel each other

                    case CardType.SHIELD:
                        return 2; // nothing happend

                    case CardType.MOVE:
                        return -1; // arrow win
                }
                #endregion
                break;

            case CardType.MOVE:
                #region Move
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return -1; // move win (move player 0)

                    case CardType.SWORD:
                        int distance = _masterBoard.GetDistancePlayers();
                        if (distance == 1)
                            return 1; // sword win because it's close enough
                        return 0; // move both players

                    case CardType.ARROW:
                        return 1; // arrow win

                    case CardType.SHIELD:
                        return -1; // move win (move player 0)

                    case CardType.MOVE:
                        return 0; // both players move
                }
                #endregion
                break;

            case CardType.SHIELD:
                #region Shield
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return 1; // magic win

                    case CardType.SWORD:
                        int distance = _masterBoard.GetDistancePlayers();
                        if (distance == 1)
                        {
                            if (player1Action.GetResolutionAmount() > player0Action.GetCardAmount())
                                return 1; // Sword win 
                            return -1; // Shield win
                        }
                        return 1; // Move sword

                    case CardType.ARROW:
                        return 2; // nothing happend

                    case CardType.SHIELD:
                        return 2; // nothing happend

                    case CardType.MOVE:
                        return 1; // move win (move player 1)
                }
                #endregion
                break;

            case CardType.SWORD:
                #region Sword
                switch (player1Action.GetCardType())
                {
                    case CardType.MAGIC:
                        return 1; // magic win

                    case CardType.SWORD:
                        int distance = _masterBoard.GetDistancePlayers();
                        if (distance <= 2)
                        {
                            if (player0Action.GetResolutionAmount() > player1Action.GetResolutionAmount())
                                return -1;
                            else if (player0Action.GetResolutionAmount() < player1Action.GetResolutionAmount())
                                return 1;
                            return 3; // push back both players
                        }
                        else
                            return 0; // play both sword (move)

                    case CardType.ARROW:
                        return -1; // sword win (Move or Attack if possible)

                    case CardType.SHIELD:
                        distance = _masterBoard.GetDistancePlayers();
                        if (distance == 1)
                        {
                            if (player0Action.GetResolutionAmount() > player1Action.GetCardAmount())
                                return -1; // Sword win 
                            return 1; // Shield win
                        }
                        return -1; // Move sword

                    case CardType.MOVE:
                        distance = _masterBoard.GetDistancePlayers();
                        if (distance == 1)
                            return -1; // sword win because it's close enough
                        return 0; // move both players
                }
                #endregion
                break;
        }

        return 2;
    }


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
