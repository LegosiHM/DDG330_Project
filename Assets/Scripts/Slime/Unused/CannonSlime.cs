using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CannonSlime : MonoBehaviour
{
    /*
    [SerializeField] Transform StartPosition;
    [SerializeField] float force = 10f;
    private Projectile takenProjectile;
    private Projectile shootingProjectile;

    void Start()
    {
        
    }

    public void TakenProjectileRegister(Projectile projectile)
    {
        takenProjectile = projectile;
    }

    public void ShootProjectileInDirection()
    {
        shootingProjectile = Instantiate(takenProjectile, StartPosition.position, Quaternion.identity);
        shootingProjectile.GetComponent<Rigidbody>().AddForce(StartPosition.forward * force, ForceMode.Impulse);
    }
    */
}
