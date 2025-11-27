using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stateText;
    [SerializeField] private TextMeshProUGUI _deathTimerText;
    [SerializeField] private TextMeshProUGUI _manualStopTimerText;

    //[SerializeField] private TextMeshProUGUI _debugText;
    [SerializeField] private SlimeGameManager _gameManager;

    void Start()
    {
        
    }
    void Update()
    {
        var slime = _gameManager?.cannon?.thrownObject?.newSlime;

        _stateText.text = $"{_gameManager?.currentState}";

        if (slime != null)
        {
            _deathTimerText.text = $"{slime.slimeDeathTimeLeft}";
            //_manualStopTimerText.text = $"Slime Manual Stop Timer: {slime.slimeManualStopTimeLeft}";
            /*_debugText.text = $"Current State: {_gameManager.currentState}" +
                              $"\nSlime Death Timer: {slime.slimeDeathTimeLeft}" +
                              $"\nSlime Manual Stop Timer: {slime.slimeManualStopTimeLeft}";*/
        }
        else
        {
            _stateText.text = $"{_gameManager?.currentState}";
            //_debugText.text = $"Current State: {_gameManager?.currentState}";
            _deathTimerText.text = "0.00";
            //_manualStopTimerText.text = "Slime Manual Stop Timer: N/A";
        }

    }
}