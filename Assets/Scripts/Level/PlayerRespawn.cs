using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespaw : MonoBehaviour
{
    //for debug only
    [SerializeField] private float yThreshold = -2f;
    [SerializeField] private Transform respawnPosition;
    private Animator animator;

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
    }

    private void Respawn()
    {
        transform.position = respawnPosition.transform.position;
    }
}
