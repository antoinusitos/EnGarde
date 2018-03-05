using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    private CardType type = CardType.MOVE;

    public Image cardImage = null;
    private int strength = 1;
    public Text cardStrength = null;
    private int cost = 1;
    public Text cardCost = null;

    public CardCustomization customization = null;

    public void UpdateCard(CardType newType, Sprite newSprite, int newCost, int newStrength)
    {
        type = newType;
        cardImage.sprite = newSprite;
        cost = newCost;
        cardCost.text = newCost.ToString();
        strength = newStrength;
        cardStrength.text = newStrength.ToString();
    }

    public void ActivateModification()
    {
        customization.gameObject.SetActive(true);
        customization.Init(type, strength, this);
    }

    public int GetCost()
    {
        return cost;
    }

    public int GetStrength()
    {
        return strength;
    }

    public int GetCardType()
    {
        return (int)type;
    }
}
