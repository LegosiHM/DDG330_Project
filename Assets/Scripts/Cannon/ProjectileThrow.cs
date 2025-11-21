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

    private int index = 0;

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
        _SlimeProjectile.RemoveAt(index);
    }


    public void LoadNextSlime()
    {
        if (_SlimeProjectile.Count > 0)
        {
            if(index > _SlimeProjectile.Count)
            {
                index = _SlimeProjectile.Count;
            }

            _objectToThrow = _SlimeProjectile[index];
            //_SlimeProjectile.RemoveAt(index);
        }
        else
        {
            Debug.Log("No Slime Left");
        }
    }

    public void SwapSlime()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            index++;
            index = Mathf.Clamp(index, 0, _SlimeProjectile.Count -1);

            _objectToThrow = _SlimeProjectile[index];
            //Debug.Log(_SlimeProjectile[index].name);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            index--;
            index = Mathf.Clamp(index, 0, _SlimeProjectile.Count-1);

            _objectToThrow = _SlimeProjectile[index];
            //Debug.Log(_SlimeProjectile[index].name);
        }
    }
}