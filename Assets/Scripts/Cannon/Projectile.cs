using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Slime _slimeOfThisProjectile;
    [SerializeField] private string _cannonSlimeTag = "CannonSlime";
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

        if (other.gameObject.CompareTag(_cannonSlimeTag))
        {
            other.GetComponent<CannonSlime>().TakenProjectileRegister(gameObject.GetComponent<Projectile>());
            Destroy(gameObject);
            other.GetComponent<CannonSlime>().ShootProjectileInDirection();
            _isDestroy = true;
        }
        else
        {
            _newSlime = Instantiate(_slimeOfThisProjectile, transform.position, transform.rotation);
            Debug.Log("Spawn Slime");


            Destroy(gameObject);
            Debug.Log("Destroy projectile");
            _isDestroy = true;
        }
    }
}
