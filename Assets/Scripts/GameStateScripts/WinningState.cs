using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningState : GameState
{
    public WinningState(SlimeGameManager gameManager) : base(gameManager)
    {
        Debug.Log("Winning Moving State");
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
