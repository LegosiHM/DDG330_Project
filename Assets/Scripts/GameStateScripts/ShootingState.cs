using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : GameState
{
    private bool isFreeLook = false;   

    public ShootingState(SlimeGameManager gameManager) : base(gameManager)
    {
        Debug.Log("Shooting State");
    }

    public override void OnEnter()
    {
        if (_gameManager.cannon.SlimeProjectile.Count > 0)
        {
            _gameManager.cannon.LoadNextSlime();
            _gameManager.EnableCannonCamera();
            _gameManager.cannonPredictor.SetTrajectoryVisible(true);
        }
        else
        {
            _gameManager.SetState(new HumanMovingState(_gameManager));
        }
    }

    public override void OnExit()
    {
        _gameManager.cannonPredictor.SetTrajectoryVisible(false);
    }

    public override void OnUpdate()
    {
        bool rmbHeld = Input.GetMouseButton(1);      
        bool lmbReleased = Input.GetMouseButtonUp(0); 

        if (rmbHeld)
        {
            isFreeLook = true;
            _gameManager.cannonFreeLook.FreeLook();
            _gameManager.cannonPredictor.SetTrajectoryVisible(false);

            return; 
        }

        if (isFreeLook)
        {
            isFreeLook = false;
            _gameManager.cannonFreeLook.ResetToDefault();
            _gameManager.cannonPredictor.SetTrajectoryVisible(true);
        }

        _gameManager.cannon.SwapSlime();
        _gameManager.cannon.DisplaySlime();
        _gameManager.cannon.Predict();               
        _gameManager.cannonLook.CannonRotate();      

        if (lmbReleased)                             
        {
            _gameManager.cannon.ThrowObject();
            _gameManager.SetState(new SlimeMovingState(_gameManager));
        }
    }
}
