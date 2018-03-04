using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCustomization : MonoBehaviour
{
    public int number = 1;
    public int max = 5;
    public int min = 1;

    private int cost = 1;

    private CardType type = CardType.MOVE;

    public Text textCost = null;
    public Text textStrength = null;

    public Image icon = null;

    public Sprite arrowSprite = null;
    public Sprite shieldSprite = null;
    public Sprite moveSprite = null;
    public Sprite magicSprite = null;
    public Sprite swordSprite = null;

    private UICard _currentCard = null;

    public void Init(CardType newType, int newNumber, UICard cardEditing)
    {
        Recalcul();
        _currentCard = cardEditing;
    }

    public void Add()
    {
        if(number + 1 <= max)
        {
            number++;
            Recalcul();
        }
    }

    public void Substract()
    {
        if (number - 1 >= min)
        {
            number--;
            Recalcul();
        }
    }

    public void Exit()
    {
        _currentCard = null;
        gameObject.SetActive(false);
    }

    private void Recalcul()
    {
        cost = Data.GetTypeValue(type, number);
        textCost.text = cost.ToString();
        textStrength.text = number.ToString();

        if(_currentCard != null)
        {
            _currentCard.UpdateCard(type, icon.sprite, cost, number);
        }
    }

    public void ChangeType(int newType)
    {
        type = (CardType)newType;

        switch (type)
        {
            case CardType.ARROW:
                icon.sprite = arrowSprite;
                break;
            case CardType.MAGIC:
                icon.sprite = magicSprite;
                break;
            case CardType.MOVE:
                icon.sprite = moveSprite;
                break;
            case CardType.SHIELD:
                icon.sprite = shieldSprite;
                break;
            case CardType.SWORD:
                icon.sprite = swordSprite;
                break;
        }

        Recalcul();
    }
}
