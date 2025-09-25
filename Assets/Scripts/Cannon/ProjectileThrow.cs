using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    TrajectoryPredictor trajectoryPredictor;


    [SerializeField, Range(0.0f, 50.0f)]
    private float force;

    [SerializeField]
    private Transform StartPosition;

    [SerializeField]
    private List<Projectile> _SlimeProjectile = new List<Projectile>();
    public List<Projectile> SlimeProjectile => _SlimeProjectile;

    private Projectile _objectToThrow;
    public Projectile objectToThrow => _objectToThrow;

    private Projectile _thrownObject;
    public Projectile thrownObject => _thrownObject;

    public InputAction fire;

    void OnEnable()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();

        if (StartPosition == null)
            StartPosition = transform;

        //fire.Enable();
        //fire.performed += ThrowObject;
    }


    public void Predict()
    {
        trajectoryPredictor.PredictTrajectory(ProjectileData());
    }

    ProjectileProperties ProjectileData()
    {
        ProjectileProperties properties = new ProjectileProperties();
        Rigidbody r = objectToThrow.GetComponent<Rigidbody>();

        properties.direction = StartPosition.forward;
        properties.initialPosition = StartPosition.position;
        properties.initialSpeed = force;
        properties.mass = r.mass;
        properties.drag = r.drag;

        return properties;
    }

    /*
    public void ThrowObject(InputAction.CallbackContext ctx)
    {
        Rigidbody thrownObject = Instantiate(objectToThrow.GetComponent<Rigidbody>(), StartPosition.position, Quaternion.identity);
        thrownObject.AddForce(StartPosition.forward * force, ForceMode.Impulse);
    }
    */
    public void ThrowObject()
    {
        _thrownObject = Instantiate(objectToThrow, StartPosition.position, Quaternion.identity);
        _thrownObject.GetComponent<Rigidbody>().AddForce(StartPosition.forward * force, ForceMode.Impulse);
    }


    public void LoadNextSlime()
    {
        if (_SlimeProjectile.Count > 0)
        {
            _objectToThrow = _SlimeProjectile[0];
            _SlimeProjectile.RemoveAt(0);
        }
        else
        {
            Debug.Log("No Slime Left");
        }
    }
}