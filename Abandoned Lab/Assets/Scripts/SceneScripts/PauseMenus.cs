using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenus : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;

    // Reference to PlayerMovement script
    public PlayerMovement playerMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        playerMovementScript.isPaused = false;  // Player movement is not paused at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;

        // Disable player movement and camera control
        playerMovementScript.isPaused = true;

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game...");  // Add this line for debugging
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;

        // Enable player movement and camera control
        playerMovementScript.isPaused = false;

        // Lock cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // You can replace this with your actual main menu scene
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
