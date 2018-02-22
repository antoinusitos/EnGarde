using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReplicateLife : MonoBehaviour
{
    public Player observingPlayer = null;

    private Text _lifeText = null;

    private void Start()
    {
        _lifeText = GetComponent<Text>();
    }

    private void Update()
    {
        if(observingPlayer != null)
        {
            _lifeText.text = observingPlayer.GetLife().ToString();
        }
    }
}
