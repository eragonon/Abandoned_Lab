using UnityEngine;
using System.Collections;

public class DoorController2 : MonoBehaviour
{
    // Door movement settings
    public float doorMoveSpeed = 2.0f; // Speed at which the door moves
    public float doorMoveDistance = 3.0f; // Distance the door should move
    public float doorMoveDelay = 1.0f; // Delay before the door starts moving after the vial is picked up

    // Reference to the VialScript
    public VialScript vialScript; // Assign this in the Inspector

    // Door positions
    private Vector3 doorStartPosition;
    private Vector3 doorTargetPosition;

    // Flag to check if the door should move
    private bool shouldMoveDoor = false;

    void Start()
    {
        // Store the starting position of the door
        doorStartPosition = transform.position;

        // Calculate the target position (move left)
        doorTargetPosition = doorStartPosition + Vector3.left * doorMoveDistance;

        // Ensure the door doesn't move at the start
        shouldMoveDoor = false;
    }

    void Update()
    {
        // Check if the vial has been picked up (via VialScript)
        if (vialScript != null && vialScript.IsVialPickedUp && !shouldMoveDoor)
        {
            // Start a coroutine to delay the door movement
            StartCoroutine(DelayDoorMovement(doorMoveDelay));
        }

        // Move the door if the flag is true
        if (shouldMoveDoor)
        {
            MoveDoor();
        }
    }

    IEnumerator DelayDoorMovement(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Start moving the door
        shouldMoveDoor = true;
    }

    void MoveDoor()
    {
        // Move the door towards the target position
        transform.position = Vector3.MoveTowards(transform.position, doorTargetPosition, doorMoveSpeed * Time.deltaTime);

        // Stop moving if the door reaches the target position
        if (transform.position == doorTargetPosition)
        {
            shouldMoveDoor = false;
        }
    }
}