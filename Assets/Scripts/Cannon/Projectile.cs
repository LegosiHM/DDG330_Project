using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Slime _slimeOfThisProjectile;
    public Slime slimeOfThisProjectile => _slimeOfThisProjectile;

    //[SerializeField] private string _cannonSlimeTag = "CannonSlime";
    private Slime _newSlime;
    public Slime newSlime => _newSlime;

    private bool _isDestroy = false;
    public bool isDestroy => _isDestroy;

    private void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDestroy)
        {
            return; //prevent spoling more than 1 slime at once
        }

        else
        {
            _newSlime = Instantiate(_slimeOfThisProjectile, transform.position, transform.rotation);
            Debug.Log("Spawn Slime");

            SoundManager.Instance.PlaySFX("slime_land");

            Destroy(gameObject);
            Debug.Log("Destroy projectile");
            _isDestroy = true;
        }
    }
}
