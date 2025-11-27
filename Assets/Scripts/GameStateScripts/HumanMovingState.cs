using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class HumanMovingState : GameState
{
    public HumanMovingState(SlimeGameManager gameManager) : base(gameManager)
    {
        Debug.Log("Human Moving State");
    }

    public override void OnEnter()
    {
        SoundManager.Instance.StopContinuous("slime_move");
        SoundManager.Instance.FadeMusic("bgm_human", 1f);
        _gameManager.EnablePlayerCamera();
    }

    public override void OnExit()
    {
        //end stage
    }

    public override void OnUpdate()
    {
        _gameManager.playerMovement.PlayerMoving();

        //Debug only
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("change to Shooting State");
            _gameManager.SetState(new ShootingState(_gameManager));
        }
        */
    }
}
