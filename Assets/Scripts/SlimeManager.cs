using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    [SerializeField] private float slimeDeathMaxTimer = 5f;
    private float slimeDeathTimeLeft;
    private bool slimeStopMoving = false;
    private Vector3 normalScale;

    void Start()
    {
        normalScale = transform.localScale;
        slimeDeathTimeLeft = slimeDeathMaxTimer;
    }

    void Update()
    {
        if (slimeDeathTimeLeft >= 0)
        {
            slimeDeathTimeLeft -= Time.deltaTime;
            normalScale.y += Time.deltaTime;
            if (normalScale.y <= slimeDeathMaxTimer)
            {
                transform.localScale = normalScale;
            }
            else
            {
                normalScale.y = slimeDeathMaxTimer;
            }
        }
        else
        {
            slimeStopMoving = true;
            //Debug.Log("Ability will go here");
            enabled = false;
        }

    }
}
