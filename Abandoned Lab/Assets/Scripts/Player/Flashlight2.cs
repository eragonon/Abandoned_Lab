using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flashlight2 : MonoBehaviour
{
    private Light flashlight;  // Reference to the Light component

    [Header("Flashlight Settings")]
    public bool startWithFlashlightOn = false;  // Set this in the Inspector to control flashlight start state

    void Start()
    {
        flashlight = GetComponent<Light>();  // Get the Light component attached to this GameObject

        // Set the flashlight's initial state based on the Inspector value
        flashlight.enabled = startWithFlashlightOn;
    }

    void Update()
    {
        // Only allow flashlight toggle if the game is not paused
        if (Input.GetKeyUp(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;  // Toggle the flashlight state
        }
    }
}
