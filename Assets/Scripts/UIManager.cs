using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image player0LeftAction = null;
    public Image player0RightAction = null;

    public Text player0LeftAmount = null;
    public Text player0RightAmount = null;

    public Image player1LeftAction = null;
    public Image player1RightAction = null;

    public Text player1LeftAmount = null;
    public Text player1RightAmount = null;

    public GameObject player0LeftSelection = null;
    public GameObject player0RightSelection = null;

    public GameObject player1LeftSelection = null;
    public GameObject player1RightSelection = null;

    public Sprite arrowSprite = null;
    public Sprite shieldSprite = null;
    public Sprite moveSprite = null;
    public Sprite magicSprite = null;
    public Sprite swordSprite = null;

    public UIState player0UIState = null;
    public UIState player1UIState = null;

    public void SetImageSprite(int playerNumber, bool left, CardType type, int amount = 1)
    {
        Image imageToChange = null;
        Text amountToChange = null;

        if (playerNumber == 0)
        {
            if (left)
            {
                imageToChange = player0LeftAction;
                amountToChange = player0LeftAmount;
            }
            else
            {
                imageToChange = player0RightAction;
                amountToChange = player0RightAmount;
            }
        }
        else
        {
            if (left)
            {
                imageToChange = player1LeftAction;
                amountToChange = player1LeftAmount;
            }
            else
            {
                imageToChange = player1RightAction;
                amountToChange = player1RightAmount;
            }
        }

        switch (type)
        {
            case CardType.ARROW:
                imageToChange.sprite = arrowSprite;
                break;
            case CardType.MAGIC:
                imageToChange.sprite = magicSprite;
                break;
            case CardType.MOVE:
                imageToChange.sprite = moveSprite;
                break;
            case CardType.SHIELD:
                imageToChange.sprite = shieldSprite;
                break;
            case CardType.SWORD:
                imageToChange.sprite = swordSprite;
                break;
        }
        amountToChange.text = amount.ToString();
    }

    public void ShowSelection(int playerNumber, bool left, bool show)
    {
        if(playerNumber == 0)
        {
            if (left)
                player0LeftSelection.SetActive(show);
            else
                player0RightSelection.SetActive(show);
        }
        else
        {
            if (left)
                player1LeftSelection.SetActive(show);
            else
                player1RightSelection.SetActive(show);
        }
    }

    public UIState GetPlayerUIState(int player)
    {
        if (player == 0)
            return player0UIState;
        else if (player == 1)
            return player1UIState;

        return null;
    }

    // SINGLETON

    private static UIManager _instance = null;

    public static UIManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
