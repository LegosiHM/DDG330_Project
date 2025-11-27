using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeClimbMovement : MonoBehaviour
{
    //[SerializeField] private float _climbingSpeed = 3f;
    [SerializeField] private float _wallDetectRadius = 1f;
    [SerializeField] private float _groundDetectOffsetY;
    [SerializeField] private float _groundDetectRadius = 1.5f;

    //private bool slimeStopMoving = false;
    //private Vector3 normalScale;
    private SlimeMovement SlimeMovement;
    private Rigidbody rb;

    private Vector3 _groundSurface;
    private Vector3 _wallSurfaceUp;
    private Vector3 _wallSurfaceRight;

    private bool _isClimbing = false;
    private bool _isTouchingGround;

    private SlimeMovementSO slimeMovementSO;

    [SerializeField] private CharacterController controller;

    void Start()
    {
        SlimeMovement = GetComponent<SlimeMovement>();
        slimeMovementSO = SlimeMovement.slimeMovementSO;
        rb = GetComponent<Rigidbody>();
    }

    public void SlimeClimbing()
    {
        WallDetect();

        if (_isClimbing)
        {
            Debug.Log("isClimbing");
            Climbing();
            if (!_isTouchingGround)
            {
                SlimeMovement.enabled = false; //if not touching ground, disable player movement
            }
            else
            {
                SlimeMovement.enabled = true; //if touching ground, enable both playerm movement and climbing
            }
        }
        else
        {
            SlimeMovement.enabled = true;
        }

    }

    private void WallDetect()
    {
        //Debug.Log(_isClimbing);
        Vector3 raycastPosition = transform.position;
        raycastPosition.y += _groundDetectOffsetY;

        RaycastHit wallDetectRaycast;
        if (Physics.Raycast(raycastPosition, transform.forward, out wallDetectRaycast, _wallDetectRadius))
        {
            _isClimbing = true;
            _groundSurface = wallDetectRaycast.normal;
            _wallSurfaceUp = Vector3.Cross(transform.right, _groundSurface).normalized;
            _wallSurfaceRight = Vector3.Cross(_groundSurface, _wallSurfaceUp).normalized;
        }
        else
        {
            _isClimbing = false;
        }

        RaycastHit groundDetectRaycast;
        if (Physics.Raycast(transform.position, -transform.up, out groundDetectRaycast, _groundDetectRadius))
        {
            _isTouchingGround = true;
        }
        else
        {
            _isTouchingGround = false;
        }
    }

    private void Climbing()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = (_wallSurfaceRight * horizontal + _wallSurfaceUp * vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            //Vector3 moveDir = Quaternion.Euler(0f, direction, 0f) * Vector3.up;
            //transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
            controller.Move(moveDirection.normalized * slimeMovementSO.climbSpeed * Time.deltaTime);
        }

        if (moveDirection.magnitude >= 0.1f)
        {
            SoundManager.Instance.PlayContinuous("slime_move", 1f);
        }
        else
        {
            SoundManager.Instance.StopContinuous("slime_move");
        }
        //Quaternion targetRotation = Quaternion.FromToRotation(transform.up, _groundSurface) * transform.rotation;
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }
}
