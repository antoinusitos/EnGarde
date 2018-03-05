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
    public Text textNumber = null;

    public Image icon = null;

    private UICard _currentCard = null;

    public DeckBuilding deckBuilding = null;

    public void Init(CardType newType, int newNumber, UICard cardEditing)
    {
        type = newType;
        number = newNumber;
        textNumber.text = number.ToString();
        ShowType();
        Recalcul();
        _currentCard = cardEditing;
    }

    public void Add()
    {
        if(number + 1 <= max)
        {
            number++;
            textNumber.text = number.ToString();
            Recalcul();
        }
    }

    public void Substract()
    {
        if (number - 1 >= min)
        {
            number--;
            textNumber.text = number.ToString();
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

        if (_currentCard != null)
        {
            _currentCard.UpdateCard(type, icon.sprite, cost, number);
        }

        deckBuilding.CalculTotal();
    }

    public void ChangeType(int newType)
    {
        type = (CardType)newType;

        ShowType();

        Recalcul();
    }

    public void ShowType()
    {
        icon.sprite = Data.GetSprite(type);
    }
}
