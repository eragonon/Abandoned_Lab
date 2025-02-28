using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    // Speed at which the door moves
    public float moveSpeed = 2.0f;

    // Distance the door should move to the left
    public float moveDistance = 3.0f;

    // Delay before the door starts moving (in seconds)
    public float delaySeconds = 1.0f;

    // Original position of the door
    private Vector3 originalPosition;

    // Target position for the door
    private Vector3 targetPosition;

    // Flag to check if the door has reached the target position
    private bool isMoving = false;

    void Start()
    {
        // Store the original position of the door
        originalPosition = transform.position;

        // Calculate the target position by subtracting the moveDistance from the X-coordinate
        targetPosition = originalPosition + Vector3.left * moveDistance;

        // Start the coroutine to delay the movement
        StartCoroutine(DelayMovement());
    }

    IEnumerator DelayMovement()
    {
        // Wait for the specified delay before starting the movement
        yield return new WaitForSeconds(delaySeconds);

        // Start moving the door
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            // Move the door towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the door has reached the target position
            if (transform.position == targetPosition)
            {
                // Stop moving the door
                isMoving = false;
            }
        }
    }
}