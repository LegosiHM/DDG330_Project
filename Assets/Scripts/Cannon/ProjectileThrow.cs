using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    [Header("Slime Displayer")]
    [SerializeField] private GameObject _previousSlimeIcon;
    private Image previousSlimeIconComponent;
    [SerializeField] private GameObject _currentSlimeIcon;
    private Image currentSlimeIconComponent;
    [SerializeField] private GameObject _nextSlimeIcon;
    private Image nextSlimeIconComponent;

    [Header("Cannon Info")]
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

    private void Awake()
    {
        previousSlimeIconComponent = _previousSlimeIcon.GetComponent<Image>();
        currentSlimeIconComponent = _currentSlimeIcon.GetComponent<Image>();
        nextSlimeIconComponent = _nextSlimeIcon.GetComponent<Image>();
    }

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
        SoundManager.Instance.PlaySFX("cannon_shoot");
        _thrownObject = Instantiate(objectToThrow, StartPosition.position, Quaternion.identity);
        _thrownObject.GetComponent<Rigidbody>().AddForce(StartPosition.forward * force, ForceMode.Impulse);
        _SlimeProjectile.RemoveAt(index);
    }


    public void LoadNextSlime()
    {
        index = Mathf.Clamp(index, 0, _SlimeProjectile.Count - 1);

        if (_SlimeProjectile.Count > 0)
        {
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

    public void DisplaySlime()
    {
        currentSlimeIconComponent.sprite = _SlimeProjectile[index].slimeOfThisProjectile.thisSlimeIcon;


        if (index - 1 >= 0) //check if there is previous slime
        {
            previousSlimeIconComponent.enabled = true;
            previousSlimeIconComponent.sprite = _SlimeProjectile[index - 1].slimeOfThisProjectile.thisSlimeIcon;
        }
        else
        {
            //_previousSlimeIcon.GetComponent<Image>().sprite = null;

            previousSlimeIconComponent.enabled = false;
        }

        if (index + 1 <= _SlimeProjectile.Count - 1) //check if there is next slime
        {
            nextSlimeIconComponent.enabled = true;
            nextSlimeIconComponent.sprite = _SlimeProjectile[index + 1].slimeOfThisProjectile.thisSlimeIcon;
        }
        else
        {
            //_nextSlimeIcon.GetComponent<Image>().sprite = null;
            nextSlimeIconComponent.enabled = false;
        }

        //Debug.Log(_currentSlimeIcon.name);
    }
}