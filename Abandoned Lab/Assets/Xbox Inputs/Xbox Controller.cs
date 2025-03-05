using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XboxControls : MonoBehaviour
{
    [SerializeField] private Rigidbody rbD;
    [SerializeField] private float speed;
    [SerializeField] private float sprintMultiplier = 2.0f;
    private Vector3 moveInputValue;
    private bool isSprinting = false;

    [SerializeField] private Transform cameraHolder;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float lookSpeed = 2.0f;
    private Vector2 lookInputValue;
    private float xRotation = 0f;

    [SerializeField] private Light flashlight;
    private bool isFlashlightOn = false;

    [SerializeField] private Transform playerTransform;
    private bool isCrouching = false;
    private Vector3 originalScale;

    private PlayerInput playerInput;
    private GameObject nearbyPickup;

    private void Awake()
    {
        rbD = GetComponent<Rigidbody>();
        originalScale = playerTransform.localScale;
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        if (playerInput != null)
        {
            var lookAction = playerInput.actions["Look"];
            lookAction.performed += OnLookInput;
            lookAction.canceled += OnLookInput;
        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            var lookAction = playerInput.actions["Look"];
            lookAction.performed -= OnLookInput;
            lookAction.canceled -= OnLookInput;
        }
    }

    private void OnLookInput(InputAction.CallbackContext ctx)
    {
        lookInputValue = ctx.ReadValue<Vector2>();
    }

    private void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
    }

    private void OnSprint(InputValue button)
    {
        isSprinting = button.isPressed;
    }

    private void OnFlashlight(InputValue button)
    {
        isFlashlightOn = !isFlashlightOn;
        flashlight.enabled = isFlashlightOn;
    }

    private void OnCrouch(InputValue button)
    {
        isCrouching = !isCrouching;
        playerTransform.localScale = isCrouching ? new Vector3(1, 0.5f, 1) : originalScale;
    }

    private void OnPickup(InputValue button)
    {
        Debug.Log("Pickup button pressed");

        if (nearbyPickup != null)
        {
            Debug.Log("Picked up: " + nearbyPickup.name);
            Destroy(nearbyPickup);
            nearbyPickup = null;
        }
        else
        {
            Debug.Log("No pickup nearby");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            Debug.Log("Pickup detected: " + other.gameObject.name);
            nearbyPickup = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == nearbyPickup)
        {
            nearbyPickup = null;
        }
    }

    private void MoveLogicMethod()
    {
        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;
        Vector3 movement = new Vector3(moveInputValue.x, 0, moveInputValue.y) * currentSpeed;
        rbD.velocity = new Vector3(movement.x, rbD.velocity.y, movement.z);
    }

    private void LookAround()
    {
        float mouseX = lookInputValue.x * lookSpeed;
        float mouseY = lookInputValue.y * lookSpeed;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        cameraTransform.rotation = cameraHolder.rotation;
        transform.Rotate(Vector3.up * mouseX);
    }

    private void FixedUpdate()
    {
        MoveLogicMethod();
    }

    private void Update()
    {
        LookAround();
    }
}
