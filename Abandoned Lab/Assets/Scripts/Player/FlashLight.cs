using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flashlight : MonoBehaviour
{
    Light light;
    public PauseMenus pauseMenu; // Reference to the PauseMenu script

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        // Only allow flashlight toggle if the game is not paused
        if (Input.GetKeyUp(KeyCode.F))
        {
            light.enabled = !light.enabled;
        }
    }
}