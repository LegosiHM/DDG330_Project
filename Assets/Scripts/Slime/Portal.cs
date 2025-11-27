using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
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

            else if (other.GetComponent<Slime>() != null) //is slime only
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

            else if (other.GetComponent<Slime>() != null) //is slime only
            {
                StopAllCoroutines();

            }
        }
    }
    IEnumerator TeleportCountdown(Collider other)
    {
        float timer = teleportDelay;
        float max = teleportDelay;

        // Start ticking with low volume
        SoundManager.Instance.PlayContinuous("portal_tick", 0.2f);

        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            float percent = 1f - (timer / max);

            float newVolume = Mathf.Lerp(0.2f, 1.0f, percent);
            SoundManager.Instance.PlayContinuous("portal_tick", newVolume);

            yield return null;
        }

        SoundManager.Instance.StopContinuous("portal_tick");
        SoundManager.Instance.PlaySFX("slime_teleport");

        other.GetComponent<CharacterController>().enabled = false;
        other.transform.position = portalDestination + portalDestinationOffset;
        other.GetComponent<CharacterController>().enabled = true;
    }
}
