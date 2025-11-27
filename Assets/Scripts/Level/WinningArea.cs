using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningArea : MonoBehaviour
{
    [SerializeField] private string playerLayer = "Player";
    [SerializeField] private SlimeGameManager gameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log("Win");
            StarManager.Instance.SaveBestStars();
            ResultScreenManager.Instance.ShowResult();
            gameManager.SetState(new WinningState(gameManager));
        }
    }
}
