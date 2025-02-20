using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flashliight : MonoBehaviour
{
    Light liight;

    void Start()
    {
        liight = GetComponent<Light>();
    }

    void Update()
    {
        // Only allow flashlight toggle if the game is not paused
        if (Input.GetKeyUp(KeyCode.F))
        {
            liight.enabled = !liight.enabled;
        }
    }
}