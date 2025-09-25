using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : GameState
{
    public ShootingState(SlimeGameManager gameManager) : base(gameManager)
    {
        Debug.Log("Shooting State");
    }

    public override void OnEnter()
    {
        //Debug.Log("ShootingState");
        //insert the next slime to the cannon (if any)
        if(_gameManager.cannon.SlimeProjectile.Count > 0)
        {
            _gameManager.cannon.LoadNextSlime();
            _gameManager.cannonPredictor.SetTrajectoryVisible(true);
            _gameManager.EnableCannonCamera();
        }
        else //if no slime left, change state => human moving state
        {
            Debug.Log("No slime left. Change to human state");
            _gameManager.SetState(new HumanMovingState(_gameManager));
        }
    }

    public override void OnExit()
    {
        //reset value
        _gameManager.cannonPredictor.SetTrajectoryVisible(false);
        //spawn player at the position (if player walk)
    }

    public override void OnUpdate()
    {
        //aiming
        _gameManager.cannon.Predict();
        _gameManager.cannonLook.CannonRotate();
        //change state when shooting => slime moving state
        if (Input.GetMouseButtonUp(0))
        {
            _gameManager.cannon.ThrowObject();
            Debug.Log("change to Slime Moving State");
            _gameManager.SetState(new SlimeMovingState(_gameManager));
        }


        //debug only
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("change to Slime Moving State");
            _gameManager.SetState(new SlimeMovingState(_gameManager));
        }
        */
    }
}
