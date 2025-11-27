using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera _camera;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 6;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3;
    private Vector3 velocity;
    private bool isGrounded;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    float turnSmoothVelocity;
    [SerializeField] private float turnSmoothTime = 0.1f;

    // Animator 
    private Animator animator; 
    private string speedParam = "Speed";
    private string _strafe = "Strafe";

    private string isJumpingParam = "IsJumping";

    //private bool isWalkingSoundPlaying = false;

    // Update is called once per frame
    public void PlayerMoving()
    {
        animator = GetComponent<Animator>();

        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log("IsGrounded:" + isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool(isJumpingParam, false);
        }
        


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            animator.SetBool(isJumpingParam, true);
            SoundManager.Instance.PlaySFX("player_jump");
        }

        /*if (isGrounded)
        {
            animator.SetBool(isJumpingParam, false);
        }*/

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

     

        //float speed = vertical * moveState;
        float strafe = horizontal;

        //animator.SetFloat(speedParam, speed, 0.1f, Time.deltaTime);
        animator.SetFloat(_strafe, strafe, 0.1f, Time.deltaTime);

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        animator.SetFloat("VelocityX", horizontal, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityY", vertical, 0.1f, Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        float currentSpeed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude; 
        animator.SetFloat(speedParam, currentSpeed);

        bool isMoving = (horizontal != 0 || vertical != 0);

        /*if (isGrounded && isMoving && !isWalkingSoundPlaying)
        {
            SoundManager.Instance.PlayContinuous("player_walk", 1f);
            isWalkingSoundPlaying = true;
        }

        if (!isGrounded || !isMoving)
        {
            if (isWalkingSoundPlaying)
            {
                SoundManager.Instance.StopContinuous("player_walk");
                isWalkingSoundPlaying = false;
            }
        }*/
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("TRY PLAY HUMAN BGM");
            SoundManager.Instance.FadeMusic("bgm_human", 1f);
        }
    }
}
