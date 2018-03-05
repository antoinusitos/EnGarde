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

    public static Sprite GetSprite(CardType type)
    {
        string path = "Sprites/";

        switch (type)
        {
            case CardType.ARROW:
                return Resources.Load<Sprite>(path + "Arrow");
            case CardType.MAGIC:
                return Resources.Load<Sprite>(path + "Magic");
            case CardType.MOVE:
                return Resources.Load<Sprite>(path + "Move");
            case CardType.SHIELD:
                return Resources.Load<Sprite>(path + "Shield");
            case CardType.SWORD:
                return Resources.Load<Sprite>(path + "Sword");
        }

        Debug.Log("return null");

        return null;
    }
}