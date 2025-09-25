using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _debugText;
    [SerializeField] private SlimeGameManager _gameManager;

    void Start()
    {
        
    }
    void Update()
    {
        var slime = _gameManager?.cannon?.thrownObject?.newSlime;

        if (slime != null)
        {
            _debugText.text = $"Current State: {_gameManager.currentState}" +
                              $"\nSlime Death Timer: {slime.slimeDeathTimeLeft}" +
                              $"\nSlime Manual Stop Timer: {slime.slimeManualStopTimeLeft}";
        }
        else
        {
            _debugText.text = $"Current State: {_gameManager?.currentState}";
        }

    }
}