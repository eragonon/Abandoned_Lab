using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class VialScript : MonoBehaviour
{
    public GameObject exitCollider;  // Reference to the exit's collider (set this in the Inspector)
    public GameObject pickupUI; // UI prompt to press E
    public GameObject objectiveUI; // UI for new objective after pickup

    private bool vialPickedUp = false;  // Flag to track if the vial has been picked up
    private bool isPlayerNearby = false; // Flag to check if player is near the vial

    // Start is called before the first frame update
    void Start()
    {
        exitCollider.SetActive(false);  // Ensure the exit is initially inactive
        pickupUI.SetActive(false); // Ensure the pickup UI is initially inactive
        objectiveUI.SetActive(false); // Ensure the objective UI is initially inactive
    }

    // Called when something enters the trigger collider of the vial
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the vial's trigger zone
        if (other.gameObject.CompareTag("Player") && !vialPickedUp)
        {
            isPlayerNearby = true;
            pickupUI.SetActive(true); // Show the pickup UI
        }
    }

    // Called when something exits the trigger collider of the vial
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;
            pickupUI.SetActive(false); // Hide the pickup UI when the player leaves the area
        }
    }

    void Update()
    {
        // Check if the player is nearby and presses the E key to pick up the vial
        if (isPlayerNearby && !vialPickedUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUpVial();
        }
    }

    private void PickUpVial()
    {
        Debug.Log("Player picked up the vial!");

        // Activate the exit collider after the vial is picked up
        exitCollider.SetActive(true);

        // Mark the vial as picked up
        vialPickedUp = true;

        // Hide the pickup UI and show the objective UI
        pickupUI.SetActive(false);
        objectiveUI.SetActive(true);

        // Destroy the vial object
        Destroy(gameObject);
    }

    // This function can be attached to the exit collider's trigger
    public void OnExitTouch(Collider other)
    {
        // Check if the player touched the exit collider after the vial is picked up
        if (other.gameObject.CompareTag("Player") && vialPickedUp)
        {
            Debug.Log("Player touched the exit and is going to the next scene!");

            // Load the "YouWin" scene when the player touches the exit
            SceneManager.LoadScene("YouWin");
        }
    }
}

