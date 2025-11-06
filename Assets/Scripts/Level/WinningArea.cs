using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningArea : MonoBehaviour
{
    [SerializeField] private string playerLayer = "Player";
    [SerializeField] private SlimeGameManager gameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerLayer))
        {
            Debug.Log("Win");
            StarManager.Instance?.SaveBestStars();
            gameManager.SetState(new WinningState(gameManager));
        }
    }
}
