using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class VialScript : MonoBehaviour
{
    public GameObject exitCollider;  // Reference to the exit's collider (set this in the Inspector)

    private bool vialPickedUp = false;  // Flag to track if the vial has been picked up

    // Start is called before the first frame update
    void Start()
    {
        exitCollider.SetActive(false);  // Ensure the exit is initially inactive
    }

    // Called when something enters the trigger collider of the vial
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has touched the vial object
        if (other.gameObject.CompareTag("Player") && !vialPickedUp)
        {
            // Log the event for debugging
            Debug.Log("Player picked up the vial!");

            // Activate the exit collider after the vial is picked up
            exitCollider.SetActive(true);

            // Mark the vial as picked up
            vialPickedUp = true;

            // Optionally destroy the vial object after it has been picked up
            Destroy(gameObject);  // Destroy the vial object
        }
    }

    // This function can be attached to the exit collider's trigger
    public void OnExitTouch(Collider other)
    {
        // Check if the player touched the exit collider after the vial is picked up
        if (other.gameObject.CompareTag("Player") && vialPickedUp)
        {
            // Log the event for debugging
            Debug.Log("Player touched the exit and is going to the next scene!");

            // Load the next scene asynchronously using the next scene's build index
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // Ensure the next scene index is valid
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                // Load the next scene asynchronously
                SceneManager.LoadSceneAsync("YouWinf");
            }
            else
            {
                Debug.LogError("No next scene found in the build settings.");
            }
        }
    }
}