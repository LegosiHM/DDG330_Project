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
        _gameManager.cannon.thrownObject.newSlime.GetComponent<Slime>().MakeSlimeDead();
    }

    public override void OnUpdate()
    {
        var slime = _gameManager?.cannon?.thrownObject?.newSlime;


        //slime movement
        if (slime != null)
        {
            _gameManager.cannon.thrownObject.newSlime.SlimeDeathCountDown();//start count down
            _gameManager.cannon.thrownObject.newSlime.GetComponent<SlimeClimbMovement>().SlimeClimbing();
            _gameManager.cannon.thrownObject.newSlime.GetComponent<SlimeMovement>().SlimeMoving();

            if (_gameManager.cannon.thrownObject.newSlime.slimeDeathTimeLeft <= 0)
            {
                Debug.Log("End Slime State");
                _gameManager.cannon.thrownObject.newSlime.GetComponent<SlimeMovement>().slimeCamera.enabled = false;
                _gameManager.cannon.thrownObject.newSlime.GetComponent<SlimeMovement>().slimeCinemachine.enabled = false;
                _gameManager.SetState(new ShootingState(_gameManager));
            }

        }




        //exit when slime is dead => shooting state

        //Debug only
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("change to Human Moving State State");
            _gameManager.SetState(new HumanMovingState(_gameManager));
        }
        */
    }
}
