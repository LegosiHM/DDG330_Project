using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    [Header("Slime Display Icon")]
    [SerializeField] private GameObject _slimeIcon;
    public GameObject slimeIcon => _slimeIcon;

    [Header("Slime Lifespan")]
    [SerializeField] private float _slimeDeathMaxTimer = 5f;
    [SerializeField] private float _slimeDeathTimeLeft;
    public float slimeDeathMaxTimer => _slimeDeathMaxTimer;
    public float slimeDeathTimeLeft => _slimeDeathTimeLeft;

    [SerializeField] private float _slimeManualStopTimer = 1f;
    [SerializeField] private float _slimeManualStopTimeLeft;

    public float slimeManualStopTimeLeft => _slimeManualStopTimeLeft;
    public float slimeManualStopTimer => _slimeManualStopTimer;

    //private bool slimeStopMoving = false;
    //private Vector3 normalScale;
    private SlimeMovement _SlimeNormalMovement;
    private SlimeClimbMovement _SlimeClimbMovement;
    private Rigidbody rb;

    private Vector3 _slimeOriginalPosition;
    public Vector3 slimeOriginalPosition => _slimeOriginalPosition;

    private bool _isDead = false;
    public bool isDead => _isDead;

    
    void Start()
    {
        //normalScale = transform.localScale;
        _slimeDeathTimeLeft = _slimeDeathMaxTimer;
        _slimeManualStopTimeLeft = _slimeManualStopTimer;
        _SlimeNormalMovement = GetComponent<SlimeMovement>();
        _SlimeClimbMovement = GetComponent<SlimeClimbMovement>();
        rb = GetComponent<Rigidbody>();
        SoundManager.Instance.PlaySFX("slime_land");
    }

    public void SlimeDeathCountDown()
    {
        if (_slimeDeathTimeLeft > 0)
        {
            _slimeDeathTimeLeft -= Time.deltaTime;

            if (!IsMovementInputPressed())
            {
                if (_slimeManualStopTimeLeft > 0)
                {
                    _slimeManualStopTimeLeft -= Time.deltaTime;
                }

                if (_slimeManualStopTimeLeft <= 0)
                {
                    _slimeDeathTimeLeft = 0;
                }
            }
            else
            {
                _slimeManualStopTimeLeft = _slimeManualStopTimer;
            }
        }
        else
        {
            //slimeStopMoving = true;
            _SlimeNormalMovement.enabled = false;
            _SlimeClimbMovement.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            //SlimeMovement.enabled = false;
            //enabled = false;
        }

    }
    bool IsMovementInputPressed() //check input
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 || Input.GetButton("Jump");
    }

    public void MakeSlimeDead()
    {
        _isDead = true;
        _slimeOriginalPosition = transform.position;
        Debug.Log("slime is dead");
    }
}
