using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public VialScript vialScript;  // Reference to the VialScript on the Vial object

    // Called when something enters the trigger collider of the exit
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit the exit collider!");
            // Call the OnExitTouch method from VialScript when the player touches the exit
            vialScript.OnExitTouch(other);
        }
    }
}
