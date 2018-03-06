using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public GameObject[] allHelp = null;

    private int _currentIndex = -1;

    public void Execute()
    {
        if(_currentIndex >= 0)
        {
            allHelp[_currentIndex].SetActive(false);
        }
        _currentIndex ++;
        if (_currentIndex < allHelp.Length)
            allHelp[_currentIndex].SetActive(true);
        else
        {
            _currentIndex = -1;
            gameObject.SetActive(false);
        }
    }
}
