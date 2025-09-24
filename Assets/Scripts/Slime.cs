using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Slime : MonoBehaviour
{
    [SerializeField] private float slimeDeathMaxTimer = 5f;
    [SerializeField] private float slimeDeathTimeLeft;

    [SerializeField] private float slimeManualStopTimer = 1f;
    [SerializeField] private float slimeManualStopTimeLeft;
    //private bool slimeStopMoving = false;
    //private Vector3 normalScale;
    private PlayerMovement _SlimeNormalMovement;
    private SlimeClimbMovement _SlimeClimbMovement;
    private Rigidbody rb;

    
    void Start()
    {
        //normalScale = transform.localScale;
        slimeDeathTimeLeft = slimeDeathMaxTimer;
        slimeManualStopTimeLeft = slimeManualStopTimer;
        _SlimeNormalMovement = GetComponent<PlayerMovement>();
        _SlimeClimbMovement = GetComponent<SlimeClimbMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        SlimeDeathCountDown();
    }

    private void SlimeDeathCountDown()
    {
        if (slimeDeathTimeLeft > 0)
        {
            slimeDeathTimeLeft -= Time.deltaTime;

            if (rb.velocity.magnitude < 0.1)
            {
                if (slimeManualStopTimeLeft > 0)
                {
                    slimeManualStopTimeLeft -= Time.deltaTime;
                }

                if (slimeManualStopTimeLeft <= 0)
                {
                    slimeDeathTimeLeft = 0;
                    Debug.Log("Already Dead");
                }
            }
            else
            {
                slimeManualStopTimeLeft = slimeManualStopTimer;
            }

            /*
            normalScale.y += Time.deltaTime;
            if (normalScale.y <= slimeDeathMaxTimer)
            {
                transform.localScale = normalScale;
            }
            else
            {
                normalScale.y = slimeDeathMaxTimer;
            }
            */
        }
        else
        {
            //slimeStopMoving = true;
            _SlimeNormalMovement.enabled = false;
            _SlimeClimbMovement.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            //SlimeMovement.enabled = false;
            //enabled = false;
        }

    }
}
