
using System;
using UnityEngine; 

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")] 
    [SerializeField] private float speedMultiplier = 1f;
    [Range(1f, 10f)]
    [SerializeField] private float walkSpeed = 2f;
    [Range(1f, 10f)]
    [SerializeField] private float crouchSpeed = 1f;
    [Range(1f, 10f)]
    [SerializeField] private float sprintSpeed = 4f;
    
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = .05f;
    [SerializeField] private float rotationOffset = 45f;

    private float gravityValue = -15.0f;
    private float verticalVelocity; 
    private float animationBlend;
    private float targetRotation;
    private float rotationVelocity;
    private Vector3 bodyVelocity;
    private float bodySpeed;


    [Header("Animation Settings")]
    [SerializeField] private float animationSpeedRate = 5f;
    private float animationSpeed = 0f;
    private int blendSpeedAnimID;
    private int isStandingAnimID;
    private int motionApeedAnimID;


    private PlayerInputAsset playerInputAsset;
    private Animator animator;
    private CharacterController characterController; 


    private void Awake() 
    {
        playerInputAsset = GetComponent<PlayerInputAsset>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        blendSpeedAnimID = Animator.StringToHash("BlendSpeed");
        motionApeedAnimID = Animator.StringToHash("MotionSpeed");
        isStandingAnimID = Animator.StringToHash("IsStanding");
    }
    private void Update() 
    {
        bodyVelocity = characterController.velocity;
        bodySpeed = characterController.velocity.magnitude;

        MovePlayer();
        RotatePlayer();
        AnimatePlayer(); 
    } 
 
    private void MovePlayer()
    {
        if (playerInputAsset.Move != Vector2.zero)
        {
            var speed = playerInputAsset.PlayerState switch
            {
                PlayerState.Walk => walkSpeed,
                PlayerState.Sprint => sprintSpeed,
                PlayerState.CrouchWalk => crouchSpeed,
                _ => 0f,
            };

            // Calculate movement direction, but apply Y-axis offset by rotating it
            Vector3 targetDirection = new Vector3(playerInputAsset.Move.x, 0f, playerInputAsset.Move.y).normalized;

            // Apply rotation offset around Y-axis
            targetDirection = Quaternion.Euler(0f, rotationOffset, 0f) * targetDirection;

            // Move the character in the rotated direction, ignoring Y-axis velocity for now
            characterController.Move(speed * speedMultiplier * Time.deltaTime * targetDirection);
        }

        // Handle gravity
        if (characterController.isGrounded)
        {
            // Reset vertical velocity when grounded
            verticalVelocity = 0f;
        }
        else
        {
            // Apply gravity if not grounded
            verticalVelocity += gravityValue * Time.deltaTime;
        } 

        // Apply the calculated vertical velocity to keep player on the ground or falling
        Vector3 gravityMovement = new Vector3(0f, verticalVelocity, 0f);
        characterController.Move(gravityMovement * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        if (playerInputAsset.Move != Vector2.zero)
        {
            // Calculate the target rotation based on movement direction
            float targetRotation = Mathf.Atan2(playerInputAsset.Move.x, playerInputAsset.Move.y) * Mathf.Rad2Deg + rotationOffset;

            // Smoothly rotate the player towards the target direction
            float smoothedRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSpeed);

            transform.rotation = Quaternion.Euler(0.0f, smoothedRotation, 0.0f);
        }
    }
    
    private void AnimatePlayer()
    {
        float targetSpeed;

        switch (playerInputAsset.PlayerState)
        {
            case PlayerState.Idle:
                animator.SetBool(isStandingAnimID, true);
                targetSpeed = 0f;
            break;

            case PlayerState.Walk:
                animator.SetBool(isStandingAnimID, true);
                targetSpeed = .5f;
            break;

            case PlayerState.Sprint:
                animator.SetBool(isStandingAnimID, true);
                targetSpeed = 1f;
            break;

            case PlayerState.CrouchIdle:
                animator.SetBool(isStandingAnimID, false);
                targetSpeed = 0f;
            break;

            case PlayerState.CrouchWalk:
                animator.SetBool(isStandingAnimID, false);
                targetSpeed = 1f;
            break;
            
            default:
                targetSpeed = 0f;
            break;
        } 

        // Interpolate between the current speed and the target speed
        animationSpeed = Mathf.Lerp(animationSpeed, targetSpeed, Time.deltaTime * animationSpeedRate);

        // Set the interpolated speed to the animator
        animator.SetFloat(blendSpeedAnimID, animationSpeed);

        // Set motion speed to adjust the overall movement animation speed (if necessary)
        animationBlend = Mathf.Lerp(1f, targetSpeed, Time.deltaTime * animationSpeedRate);
        animator.SetFloat(motionApeedAnimID, animationBlend);
    }

}
