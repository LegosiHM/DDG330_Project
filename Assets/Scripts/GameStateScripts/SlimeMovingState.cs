using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovingState : GameState
{
    public SlimeMovingState(SlimeGameManager gameManager) : base(gameManager)
    {
        Debug.Log("Slime Moving State");
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        //Debug only
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("change to Human Moving State State");
            _gameManager.SetState(new HumanMovingState(_gameManager));
        }
    }
}
