using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static int GetTypeValue(CardType type, int strenght)
    {
        switch (type)
        {
            case CardType.ARROW:
                return 15;

            case CardType.MAGIC:
                return 4 * strenght;

            case CardType.MOVE:
                return strenght;

            case CardType.SHIELD:
                return strenght;

            case CardType.SWORD:
                return 4 * strenght;
        }
        return 0;
    }
}
