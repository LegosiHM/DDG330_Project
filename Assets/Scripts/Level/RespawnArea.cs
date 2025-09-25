using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnArea : MonoBehaviour
{
    private SlimeGameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<SlimeGameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something enter");
        other.transform.root.position = gameManager.respawnPosition.transform.position; //not working
    }
}
