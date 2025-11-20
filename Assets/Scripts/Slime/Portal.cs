using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string playerLayer = "Player";
    [SerializeField] private Vector3 portalDestinationOffset;
    [SerializeField] private float teleportDelay = 3f;

    private Vector3 portalDestination;



    public void SetPortalDestination(Vector3 destination)
    {
        portalDestination = destination;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(portalDestination == Vector3.zero)
        {
            return;
        }
        else
        {
            if (other.gameObject == gameObject) //make sure to not detect itself
            {
                return;
            }

            else if (other.CompareTag(playerLayer))
            {
                StartCoroutine(TeleportCountdown(other));
                
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (portalDestination == Vector3.zero)
        {
            return;
        }
        else
        {
            if (other.gameObject == gameObject) //make sure to not detect itself
            {
                return;
            }

            else if (other.CompareTag(playerLayer))
            {
                StopAllCoroutines();

            }
        }
    }
    IEnumerator TeleportCountdown(Collider other)
    {
        yield return new WaitForSeconds(teleportDelay);

        other.GetComponent<CharacterController>().enabled = false; //disable character controller to teleport
        other.transform.position = portalDestination + portalDestinationOffset;
        other.GetComponent<CharacterController>().enabled = true; //reenable character controller
    }

}
