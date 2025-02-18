using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync("LabLevel1");
    }

    public void Exit()
    {
        Application.Quit();

    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

}