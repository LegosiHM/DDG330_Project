using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Slime : MonoBehaviour
{
    [SerializeField] private float _slimeDeathMaxTimer = 5f;
    [SerializeField] private float _slimeDeathTimeLeft;
    public float slimeDeathTimeLeft => _slimeDeathTimeLeft;

    [SerializeField] private float _slimeManualStopTimer = 1f;
    [SerializeField] private float _slimeManualStopTimeLeft;

    public float slimeManualStopTimeLeft => _slimeManualStopTimeLeft;

    //private bool slimeStopMoving = false;
    //private Vector3 normalScale;
    private SlimeMovement _SlimeNormalMovement;
    private SlimeClimbMovement _SlimeClimbMovement;
    private Rigidbody rb;

    
    void Start()
    {
        //normalScale = transform.localScale;
        _slimeDeathTimeLeft = _slimeDeathMaxTimer;
        _slimeManualStopTimeLeft = _slimeManualStopTimer;
        _SlimeNormalMovement = GetComponent<SlimeMovement>();
        _SlimeClimbMovement = GetComponent<SlimeClimbMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
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

            /*
            normalScale.y += Time.deltaTime;
            if (normalScale.y <= slimeDeathMaxTimer)
            {
                transform.localScale = normalScale;
            }
            else
            {
                normalScale.y = slimeDeathMaxTimer;
            }
            */
        }
        else
        {
            //slimeStopMoving = true;
            _SlimeNormalMovement.enabled = false;
            _SlimeClimbMovement.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            Debug.Log("Already Dead");

            //SlimeMovement.enabled = false;
            //enabled = false;
        }

    }
    bool IsMovementInputPressed() //check input
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 || Input.GetButton("Jump");
    }

}
