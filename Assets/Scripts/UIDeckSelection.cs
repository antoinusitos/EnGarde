using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeckSelection : MonoBehaviour
{
    public GameObject aDeckInfoPrefab = null;

    public float sizeY = 500;

    private UIADeckInfo[] _allDeckInfos = null;

    public Transform allDeckInfoParent = null;

    public void ShowDeckAvailable(DeckInfos[] decks)
    {
        _allDeckInfos = new UIADeckInfo[decks.Length];
        float posY = -50;
        Rect r = allDeckInfoParent.GetComponent<RectTransform>().rect;
        allDeckInfoParent.GetComponent<RectTransform>().sizeDelta = new Vector2(r.width, decks.Length * 100);

        for (int i = 0; i < decks.Length; i++)
        {
            GameObject go = Instantiate(aDeckInfoPrefab);
            go.transform.SetParent(allDeckInfoParent);
            go.transform.localPosition = Vector2.up * posY + Vector2.right * 250;
            go.GetComponent<UIADeckInfo>().SetDeckName(decks[i].deckName);
            go.GetComponent<UIADeckInfo>().SetDeckCost(decks[i].deckSize);
            _allDeckInfos[i] = go.GetComponent<UIADeckInfo>();
            posY -= 100;
        }
    }

    public void ExecuteButton()
    {
        string player0Deck = "";
        string player1Deck = "";
        bool player0Found = false;
        bool player1Found = false;

        for (int i = 0; i < _allDeckInfos.Length; i++)
        {
            if(!player0Found && _allDeckInfos[i].GetLeftSideSelected())
            {
                player0Found = true;
                player0Deck = _allDeckInfos[i].deckName.text;
            }
            if (!player1Found && _allDeckInfos[i].GetRightSideSelected())
            {
                player1Found = true;
                player1Deck = _allDeckInfos[i].deckName.text;
            }

            if (player1Found && player0Found)
            {
                GameManager.GetInstance().SetPlayerDeck(0, player0Deck);
                GameManager.GetInstance().SetPlayerDeck(1, player1Deck);
                Debug.Log("starting game...");
                GameManager.GetInstance().StartGame();
                return;
            }
        }
    }

    public string GetDeckSelected()
    {
        for (int i = 0; i < _allDeckInfos.Length; i++)
        {
            if (_allDeckInfos[i].GetLeftSideSelected())
            {
                return _allDeckInfos[i].deckName.text;
            }
        }

        return "";
    }
}
