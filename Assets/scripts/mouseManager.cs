using UnityEngine;

public class mouseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        // Hide the mouse cursor
        Cursor.visible = false;
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Optional: Add logic to show the cursor (e.g., for a menu)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
