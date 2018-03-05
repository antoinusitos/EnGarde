using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public Text winner = null;

    public void SetWinner(int player)
    {
        winner.text = "Player " + player + " Win !";
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartAgain()
    {
        GameManager.GetInstance().StartAgain();
    }
}
