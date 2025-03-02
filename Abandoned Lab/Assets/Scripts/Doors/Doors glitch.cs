using UnityEngine;

public class GlitchingSlidingDoor : MonoBehaviour
{
    public Vector3 position1; // First position (set in Inspector)
    public Vector3 position2; // Second position (set in Inspector)
    public Vector3 position3; // Third position (set in Inspector)
    public float slideSpeed = 2.0f; // Speed of the door movement
    public float glitchIntensity = 0.1f; // Intensity of the glitch effect

    private Vector3[] positions; // Array to store the three positions
    private int currentTargetIndex = 0; // Index of the current target position

    void Start()
    {
        // Initialize the positions array with the three set positions
        positions = new Vector3[] { position1, position2, position3 };

        // Start at the first position
        transform.position = position1;
    }

    void Update()
    {
        // Move the door towards the current target position
        MoveDoor(positions[currentTargetIndex]);

        // Check if the door has reached the current target position
        if (Vector3.Distance(transform.position, positions[currentTargetIndex]) < 0.01f)
        {
            // Move to the next position in the array
            currentTargetIndex = (currentTargetIndex + 1) % positions.Length;
        }
    }

    void MoveDoor(Vector3 targetPosition)
    {
        // Add a glitch effect by introducing random noise
        Vector3 glitchOffset = new Vector3(
            Random.Range(-glitchIntensity, glitchIntensity),
            Random.Range(-glitchIntensity, glitchIntensity),
            Random.Range(-glitchIntensity, glitchIntensity)
        );

        // Move the door with the glitch effect
        transform.position = Vector3.MoveTowards(transform.position, targetPosition + glitchOffset, slideSpeed * Time.deltaTime);
    }
}