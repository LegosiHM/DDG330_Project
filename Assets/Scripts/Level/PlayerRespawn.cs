using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespaw : MonoBehaviour
{
    //for debug only
    [SerializeField] private float yThreshold = -10f;
    [SerializeField] private Transform respawnPosition;

    void FixedUpdate()
    {
        if (transform.position.y < yThreshold || Input.GetKeyDown(KeyCode.P))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = respawnPosition.transform.position;
    }
}
