using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    [SerializeField] private float slimeDeathTimer = 10f;
    private bool slimeStopMoving = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (slimeDeathTimer >= 0)
        {
            slimeDeathTimer -= Time.deltaTime;
        }
        else
        {
            slimeStopMoving = true;
            Debug.Log("Ability will go here");
            enabled = false;
        }

    }
}
