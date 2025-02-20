using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Youwin : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync("LabLevel1");
    }

    public void QuitGame()
    {
        Application.Quit();

    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}

