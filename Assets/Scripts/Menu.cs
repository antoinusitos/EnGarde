using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu = null;
    public GameObject DeckBuildMenu = null;

    public void LaunchGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LaunchDeckBuilding()
    {
        DeckBuildMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackToMenu()
    {
        DeckBuildMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
