using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("LabLevel");
    }

    public void QuitGame()
    {
        Application.Quit();

    }

    public void Credits()
    {
        SceneManager.LoadSceneAsync("Credits");
    }

    public void Controls()
    {
        SceneManager.LoadSceneAsync("Controls");
    }
}