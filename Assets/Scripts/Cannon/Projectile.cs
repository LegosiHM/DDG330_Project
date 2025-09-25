using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Slime _slimeOfThisProjectile;
    private Slime _newSlime;
    public Slime newSlime => _newSlime;

    private bool _isDestroy = false;
    public bool isDestroy => _isDestroy;


    private void OnTriggerEnter(Collider other)
    {
        if (_isDestroy)
        {
            return; //prevent spoling more than 1 slime at once
        }

        _newSlime = Instantiate(_slimeOfThisProjectile, transform.position, transform.rotation);
        Debug.Log("Spawn Slime");


        Destroy(gameObject);
        Debug.Log("Destroy projectile");
        _isDestroy = true;
    }
}
