using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeMovingState : GameState
{
    public SlimeMovingState(SlimeGameManager gameManager) : base(gameManager)
    {
        Debug.Log("Slime Moving State");
    }

    public override void OnEnter()
    {
        SoundManager.Instance.FadeMusic("bgm_slime", 1f);
        _gameManager.ShowLifespanBar();
    }

    public override void OnExit()
    {
        SoundManager.Instance.StopContinuous("slime_move");
        _gameManager.cannon.thrownObject.newSlime.GetComponent<Slime>().MakeSlimeDead();
        SoundManager.Instance.PlaySFX("slime_dead");
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

            float currentLifespan = _gameManager.cannon.thrownObject.newSlime.GetComponent<Slime>().slimeDeathTimeLeft;
            float maxLifespan = _gameManager.cannon.thrownObject.newSlime.GetComponent<Slime>().slimeDeathMaxTimer;

            _gameManager.UpdateHPBar(currentLifespan, maxLifespan);

            float currentManualStop = _gameManager.cannon.thrownObject.newSlime.GetComponent<Slime>().slimeManualStopTimeLeft;
            float maxManualStop = _gameManager.cannon.thrownObject.newSlime.GetComponent<Slime>().slimeManualStopTimer;

            _gameManager.UpdateManualStopbar(currentManualStop, maxManualStop);


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
