using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    [SerializeField] private float slimeDeathMaxTimer = 5f;
    [SerializeField] private float slimeDeathTimeLeft;

    [SerializeField] private float slimeManualStopTimer = 1f;
    [SerializeField] private float slimeManualStopTimeLeft;
    private bool slimeStopMoving = false;
    private Vector3 normalScale;
    private SlimeMovement SlimeMovement;
    private Rigidbody rb;

    void Start()
    {
        normalScale = transform.localScale;
        slimeDeathTimeLeft = slimeDeathMaxTimer;
        slimeManualStopTimeLeft = slimeManualStopTimer;
        SlimeMovement = GetComponent<SlimeMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (slimeDeathTimeLeft > 0)
        {
            slimeDeathTimeLeft -= Time.deltaTime;

            if(rb.velocity.magnitude < 0.1)
            {
                if(slimeManualStopTimeLeft > 0)
                {
                    slimeManualStopTimeLeft -= Time.deltaTime;
                }

                if(slimeManualStopTimeLeft <= 0)
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
            slimeStopMoving = true;
            SlimeMovement.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            
            //SlimeMovement.enabled = false;
            //enabled = false;
        }

    }
}
