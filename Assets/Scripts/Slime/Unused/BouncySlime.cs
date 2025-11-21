using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySlime : MonoBehaviour
{
    [SerializeField] private Transform bouncePivot;
    [SerializeField] private Camera slimeCamera;

    [Header("Bounce Info")]
    /*
    [SerializeField] private float bounceCooldown = 3f;
    [SerializeField] private float bounceDelay = 2f;
    */
    [SerializeField] private float bounceForce = 20f;
    private Slime slimeComponent => GetComponent<Slime>();
    private bool isDead => slimeComponent.isDead;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            RotatePivotAsCamera();
        }
    }

    private void RotatePivotAsCamera()
    {
        bouncePivot.transform.rotation = slimeCamera.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == gameObject)
        {
            return; //ignore self
        }

        CharacterController controller = other.GetComponent<CharacterController>();

        if(other.GetComponent<Slime>() != null) //is slime
        {
            SlimeMovement slimeMovement = other.GetComponent<SlimeMovement>();
            if(slimeMovement != null)
            {
                Vector3 bounceVelocity = bouncePivot.up * bounceForce;
                slimeMovement.ApplyExternalVelocity(bounceVelocity);
            }
        }
    }
}
