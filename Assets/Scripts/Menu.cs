using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu = null;
    public GameObject DeckBuildMenu = null;
    public GameObject DeckSelectionMenu = null;

    private Card1[] deckSelected = null;

    private DeckInfos[] _lastDeckInfos = null;
    private string _lastDeckname = null;

    public void LaunchGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResetDeckSelected()
    {
        deckSelected = null;
        _lastDeckname = "";
    }

    public void SaveDeckSelected()
    {
        string name = DeckSelectionMenu.GetComponent<UIDeckSelection>().GetDeckSelected();

        if(name != "")
        {
            _lastDeckname = name;
            deckSelected = FileReader.GetInstance().ReadFile(name);
        }
        else
        {
            deckSelected = null;
            Debug.LogError("No Deck with this name found !");
            BackToMenu();
        }
    }

    public void LaunchDeckSelection()
    {
        DeckBuildMenu.SetActive(false);
        DeckSelectionMenu.SetActive(true);
        mainMenu.SetActive(false);

        _lastDeckInfos = FileReader.GetInstance().GetDeckInfos();
        
        DeckSelectionMenu.GetComponent<UIDeckSelection>().ShowDeckAvailable(_lastDeckInfos);
    }

    public void LaunchDeckBuilding()
    {
        DeckBuildMenu.SetActive(true);
        DeckSelectionMenu.SetActive(false);
        mainMenu.SetActive(false);

        if(deckSelected != null)
        {
            DeckBuildMenu.GetComponent<DeckBuilding>().AffectDeck(deckSelected, _lastDeckname);
            DeckBuildMenu.GetComponent<DeckBuilding>().CalculTotal();
        }
    }

    public void BackToMenu()
    {
        DeckBuildMenu.SetActive(false);
        DeckSelectionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }


}
