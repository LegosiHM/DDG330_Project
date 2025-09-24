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
        //insert the next slime to the cannon (if any)
        //if no slime left, change state => human moving state
    }

    public override void OnExit()
    {
        //reset value
        //spawn slime at the hit position (if shoot slime)
        //spawn player at the position (if player walk)
    }

    public override void OnUpdate()
    {
        //aiming
        //change state when shooting => slime moving state

        //debug only
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("change to Slime Moving State");
            _gameManager.SetState(new SlimeMovingState(_gameManager));
        }
    }
}
