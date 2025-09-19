using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : GameState
{
    public ShootingState(SlimeGameManager gameManager) : base(gameManager)
    {
        Debug.Log("SHooting State");
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        //debug only
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("change to Slime Moving State");
            _gameManager.SetState(new SlimeMovingState(_gameManager));
        }
    }
}
