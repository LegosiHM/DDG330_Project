using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespaw : MonoBehaviour
{
    public float threshold;
    public Vector3 playerPosition;

    void FixedUpdate()
    {
        if (transform.position.y < threshold) { transform.position = playerPosition; }
    }
}
