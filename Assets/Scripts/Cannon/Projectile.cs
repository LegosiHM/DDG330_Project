using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Slime _slimeOfThisProjectile;

    private void OnTriggerEnter(Collider other)
    {
        Slime newSlime;
        newSlime = Instantiate(_slimeOfThisProjectile, transform.position, transform.rotation);
        Debug.Log("Spawn Slime");
        Destroy(this);
    }
}
