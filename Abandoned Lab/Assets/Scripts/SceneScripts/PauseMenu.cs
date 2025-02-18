using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Time.timeScale = 0;
        isPaused = true;
        playerMovement.isPaused = true; // Disable movement and camera rotation
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        playerMovement.isPaused = false; // Enable movement and camera rotation
    }

    public void Mainmenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Mainmenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}