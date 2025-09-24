using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovingState : GameState
{
    public HumanMovingState(SlimeGameManager gameManager) : base(gameManager)
    {
        Debug.Log("Human Moving State");
    }

    public override void OnEnter()
    {
        //attach player component to player
    }

    public override void OnExit()
    {
        //end stage
    }

    public override void OnUpdate()
    {
        //recieve movement input
        //movement

        //Debug only
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("change to Shooting State");
            _gameManager.SetState(new ShootingState(_gameManager));
        }
    }
}
