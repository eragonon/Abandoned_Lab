using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpPower = 7f;
    public float gravity = 20f;
    public float lookSpeed = 2f;
    public float lookXLimit = 90f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    public float crouchTransitionSpeed = 15f; // Speed of crouch height transition

    public Image StaminaBar;

    public float Stamina, MaxStamina;
    public float RunCost = 10f;
    public float ChargeRate = 5f;
    private Coroutine recharge;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    private bool isCurrentlyRunning = false; // Tracks running status
    private float targetHeight; // Target height for crouching/standing

    // Add a reference to the pause state
    public bool isPaused = false; // Track the pause state

    // Input System Variables
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction crouchAction;
    private InputAction runAction;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize Input System
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        crouchAction = playerInput.actions["Crouch"];
        runAction = playerInput.actions["Run"];

        // Set initial height
        targetHeight = defaultHeight;

        // Initialize Stamina
        Stamina = MaxStamina;
        if (StaminaBar != null)
        {
            StaminaBar.fillAmount = Stamina / MaxStamina;
        }
    }

    void Update()
    {
        if (isPaused) return; // Skip the update if paused

        // Get input values from the Input System
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        bool jumpPressed = jumpAction.triggered;
        bool crouchPressed = crouchAction.IsPressed();
        bool runPressed = runAction.IsPressed();

        // Determine if the player is grounded
        bool isGrounded = characterController.isGrounded;

        // Crouch logic (press and hold)
        if (crouchPressed && canMove)
        {
            targetHeight = crouchHeight;
        }
        else
        {
            // Check if there is enough space to stand up
            if (CanUncrouch())
            {
                targetHeight = defaultHeight;
            }
        }

        // Smoothly adjust height and center
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, crouchTransitionSpeed * Time.deltaTime);
        characterController.center = new Vector3(0, characterController.height / 2f, 0);

        // Adjust speed while crouching
        bool isCrouching = Mathf.Abs(characterController.height - crouchHeight) < 0.1f;
        if (isCrouching)
        {
            isCurrentlyRunning = false; // Cannot run while crouching
        }

        // Determine movement speed
        if (runPressed && isGrounded && !isCrouching)
        {
            if (Stamina > 0)
            {
                isCurrentlyRunning = true; // Start running if grounded, run is held, and stamina is available

                // Drain stamina
                Stamina -= RunCost * Time.deltaTime;
                if (Stamina < 0) Stamina = 0;
                StaminaBar.fillAmount = Stamina / MaxStamina;

                Debug.Log("Running, Stamina: " + Stamina);

                // Stop any ongoing recharge coroutine
                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
            }
            else
            {
                isCurrentlyRunning = false; // Stop running if stamina is depleted
            }
        }
        else if (isGrounded)
        {
            isCurrentlyRunning = false; // Stop running if grounded and run is not held
        }

        // Calculate movement speed based on current state (crouching, running, or walking)
        float currentSpeed = isCrouching ? crouchSpeed : (isCurrentlyRunning ? runSpeed : walkSpeed);
        float curSpeedX = canMove ? currentSpeed * moveInput.y : 0;
        float curSpeedY = canMove ? currentSpeed * moveInput.x : 0;

        // Preserve Y-axis movement
        float movementDirectionY = moveDirection.y;

        if (isGrounded)
        {
            // Update movement direction when grounded
            moveDirection = (transform.forward * curSpeedX) + (transform.right * curSpeedY);
        }
        else
        {
            // Maintain momentum while airborne
            Vector3 horizontalVelocity = new Vector3(moveDirection.x, 0, moveDirection.z);
            Vector3 inputVelocity = (transform.forward * curSpeedX) + (transform.right * curSpeedY);

            // Add input to current horizontal velocity
            if (inputVelocity != Vector3.zero)
            {
                horizontalVelocity = inputVelocity;
            }

            moveDirection = horizontalVelocity;
        }

        // Apply Y-axis movement
        moveDirection.y = movementDirectionY;

        // Jump logic
        if (jumpPressed && canMove && isGrounded)
        {
            moveDirection.y = jumpPower;
        }

        // Apply gravity if not grounded
        if (!isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);

        // Handle camera rotation
        if (canMove)
        {
            rotationX += -lookInput.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, lookInput.x * lookSpeed, 0);
        }
    }

    // Method to check if there is enough space to stand up
    private bool CanUncrouch()
    {
        Vector3 topOfPlayer = transform.position + Vector3.up * (characterController.height / 2f);
        Vector3 desiredTop = transform.position + Vector3.up * (defaultHeight / 2f);
        float distanceToDesiredTop = Vector3.Distance(topOfPlayer, desiredTop);

        if (Physics.Raycast(topOfPlayer, Vector3.up, distanceToDesiredTop))
        {
            Debug.Log("Obstacle above, cannot uncrouch.");
            return false;
        }

        return true;
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while (Stamina < MaxStamina)
        {
            Stamina += ChargeRate * Time.deltaTime;
            if (Stamina > MaxStamina) Stamina = MaxStamina;
            StaminaBar.fillAmount = Stamina / MaxStamina;
            yield return null;
        }
    }
}