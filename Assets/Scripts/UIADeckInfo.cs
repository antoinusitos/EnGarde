using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIADeckInfo : MonoBehaviour
{
    public Text deckName = null;
    public Text deckCost = null;

    public Slider sliderCost = null;

    private bool leftSelected = false;
    private bool rightSelected = false;

    public void SetLeftSelected(bool newState)
    {
        leftSelected = newState;
    }

    public void SetRightSelected(bool newState)
    {
        rightSelected = newState;
    }

    public bool GetLeftSideSelected()
    {
        return leftSelected;
    }

    public bool GetRightSideSelected()
    {
        return rightSelected;
    }

    public void SetDeckName(string name)
    {
        deckName.text = name;
    }

    public void SetDeckCost(int cost)
    {
        deckCost.text = cost.ToString() + "/100";
        sliderCost.value = cost;
    }

}
