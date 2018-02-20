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
                    //if distance <= player1Action.GetCardAmount then player 1 can hit
                    return 1; // sword win (Move and attack if possible)

                case CardType.ARROW:
                    return 2; // players cancel each other

                case CardType.SHIELD:
                    return 1; // shield win

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
                    return 2; // sword win if can reach but move both players

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
                    //if distance <= player1Action.GetCardAmount then player 1 can hit
                    return 1; // sword win (Move and attack if possible)

                case CardType.ARROW:
                    return -1; // shield win

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
                        return 3; // push back both players
                    else
                        return 0; // play both sword (move and attack if possible)

                case CardType.ARROW:
                    //if distance <= player0Action.GetCardAmount then player 0 can hit
                    return -1; // sword win (Move and attack if possible)

                case CardType.SHIELD:
                    //if distance <= player0Action.GetCardAmount then player 0 can hit
                    return -1; // sword win (Move and attack if possible)

                case CardType.MOVE:
                    distance = _masterBoard.GetDistancePlayers();
                    if (distance == 1)
                        return -1; // sword win because it's close enough
                    return 2; // sword win if can reach but move both players
            }
            #endregion
            break;
    }

    return 2;
}