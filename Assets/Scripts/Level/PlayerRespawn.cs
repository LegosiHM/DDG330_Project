using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespaw : MonoBehaviour
{
    //for debug only
    [SerializeField] private float yThreshold = -2f;
    [SerializeField] private Transform respawnPosition;
    private Animator animator;
    private float waitTime = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (transform.position.y < yThreshold || Input.GetKeyDown(KeyCode.P))
        {
            animator.enabled = false;
            Respawn();
        }

        //this is not supposed to be the way I do it, but it worked for now
        if (waitTime > 0f)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            animator.enabled = true;
            waitTime = 1f;
        }
    }

    private void Respawn()
    {
        transform.position = respawnPosition.transform.position;


    }

}
