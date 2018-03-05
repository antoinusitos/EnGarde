using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilding : MonoBehaviour
{
    public UICard[] allCards = null;

    public Text totalText = null;
    public Slider totalSlider = null;

    public Text deckText = null;

    private int lastCalcul = 0;

    public void CalculTotal()
    {
        int total = 0;
        for(int i = 0; i < allCards.Length; i++)
        {
            total += allCards[i].GetCost();
        }

        totalText.text = total + " / 100";
        totalSlider.value = total;

        lastCalcul = total;
    }

    public void AffectDeck(Card1[] deck, string deckName)
    {
        for(int i = 0; i < allCards.Length; i++)
        {
            allCards[i].UpdateCard(
                deck[i].GetCardType(), 
                Data.GetSprite(deck[i].GetCardType()), 
                Data.GetTypeValue(deck[i].GetCardType(), 
                deck[i].GetCardAmount()), 
                deck[i].GetCardAmount()
            );
        }

        deckText.transform.parent.GetComponent<InputField>().text = deckName;
    }

    public void SaveDeck()
    {
        CalculTotal();

        Deck d = new Deck();
        d.InitDeck();

        for(int i = 0; i < allCards.Length; i++)
        {
            Actions action = null;

            switch (allCards[i].GetCardType() - 1)
            {
                case 0:
                    action = new Arrow();
                    break;
                case 1:
                    action = new Magic();
                    break;
                case 2:
                    action = new Sword();
                    break;
                case 3:
                    action = new Move();
                    break;
                case 4:
                    action = new Shield();
                    break;
            }

            action.InitAction();
            action.SetCardAmount(allCards[i].GetStrength());

            Card1 c = new Card1();
            c.SetCard(action);
            d.AddCard(c, i);
        }

        if(lastCalcul < 100 && deckText.text != "")
        {
            FileReader.GetInstance().SaveDeck(deckText.text, d);
        }
    }
}
