using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;  // Make the cursor visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }
}