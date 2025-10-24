using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [Header("Collectable")]
    public string collectableName;

    [Header("Rotation")]
    public Vector3 rotation;
    public float rotationSpeed;

    void Start()
    {
        if (FBPP.HasKey(collectableName))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            FBPP.SetString(collectableName, "Obtained");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }
}
