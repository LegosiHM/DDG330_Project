using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera _camera;
    public Camera slimeCamera => _camera;
    [SerializeField] private CinemachineFreeLook _cinemachine;
    public CinemachineFreeLook slimeCinemachine => _cinemachine;
    [SerializeField] private SlimeMovementSO _slimeMovementSO;
    public SlimeMovementSO slimeMovementSO => _slimeMovementSO;

    //[SerializeField] private float speed = 6;
    private float gravity = -9.81f;
    //[SerializeField] private float jumpHeight = 3;
    private Vector3 velocity;
    private bool _isGrounded;
    public bool isGrounded => _isGrounded;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    float turnSmoothVelocity;
    [SerializeField] private float turnSmoothTime = 0.1f;

    [SerializeField] private float externalVelocityDampening = 2f;
    private Vector3 externalVelocity;

    public void SlimeMoving()
    {
        if(!enabled) return; //if disable by other script, return (function still called, but not doing anything)

        //Debug.Log("isMoving");
        //jump
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log("IsGrounded:" + isGrounded);

        /*
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        */

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            velocity.y = Mathf.Sqrt(slimeMovementSO.jumpHeight * -2 * gravity);
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;

        externalVelocity = Vector3.Lerp(externalVelocity, Vector3.zero, externalVelocityDampening * Time.deltaTime); //fade out external velocity overtime

        Vector3 combineVelocity = velocity + externalVelocity;

        controller.Move(combineVelocity * Time.deltaTime);
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * _slimeMovementSO.speed * Time.deltaTime);
        }

    }

    public void ApplyExternalVelocity(Vector3 exVelocity)
    {
        externalVelocity += exVelocity;
    }

    public void ChangeSlimeMovementSO(SlimeMovementSO newSlimeMovementSO)
    {
        _slimeMovementSO = newSlimeMovementSO;
    }
}
