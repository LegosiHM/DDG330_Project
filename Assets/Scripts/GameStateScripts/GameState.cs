using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected SlimeGameManager _gameManager;

    public GameState(SlimeGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
