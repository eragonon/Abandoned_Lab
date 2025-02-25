using UnityEngine;
using System.Collections;

public class Flashlight2 : MonoBehaviour
{
    private Light flashlight;

    [Header("Flashlight Settings")]
    public bool startWithFlashlightOn = false;

    void Start()
    {
        flashlight = GetComponent<Light>();
        flashlight.enabled = startWithFlashlightOn;
    }

    void Update()
    {
        // Prevent flashlight toggle if the game is paused
        if (!PauseMenus.isPaused && Input.GetKeyUp(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
}
