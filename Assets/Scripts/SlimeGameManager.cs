using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGameManager : MonoBehaviour
{
    private GameState _currentState;
    [HideInInspector] public GameState currentState => _currentState;


    private void Start()
    {
        SetState(new ShootingState(this));
    }

    private void Update()
    {
        _currentState?.OnUpdate();
    }

    public void SetState(GameState newState)
    {
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }
}
