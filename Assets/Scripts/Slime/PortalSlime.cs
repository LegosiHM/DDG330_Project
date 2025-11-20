using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSlime : MonoBehaviour
{
    [Header("SlimeMovementSO")]
    [SerializeField] private SlimeMovementSO _slimeAfterPlacePortalSO;

    [Header("SlimePortalComponent")]
    [SerializeField] private Transform portalObject;
    [SerializeField] private Transform playerObject;

    private Slime slimeComponent => GetComponent<Slime>();
    private SlimeMovement slimeMovement => GetComponent<SlimeMovement>();


    void Update()
    {
        if (slimeComponent.isDead)
        {
            portalObject.GetComponent<Portal>().SetPortalDestination(playerObject.transform.position);
            return;
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                PlacePortal();
            }
        }

    }

    private void PlacePortal()
    {
        if (slimeMovement.isGrounded)
        {
            slimeMovement.ChangeSlimeMovementSO(_slimeAfterPlacePortalSO);
            portalObject.SetParent(null);
        }
    }


}
