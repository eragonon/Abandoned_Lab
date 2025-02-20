using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public VialScript vialScript;  // Reference to the VialScript on the Vial object

    // Called when something enters the trigger collider of the exit
    private void OnTriggerEnter(Collider other)
    {
        // Call the OnExitTouch function in the VialScript when the player touches the exit
        vialScript.OnExitTouch(other);
    }
}