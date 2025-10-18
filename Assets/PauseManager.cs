using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    void Start()
    {
        // Start unpaused
        Time.timeScale = 1f;

        // Cursor starts hidden and locked
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0f; // freeze simulation

                // Show and unlock cursor so user can use sliders
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f; // resume simulation

                // Hide and lock cursor again
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                // Defocus any selected UI element (most importantly, the slider bc it kept moving around from WASD inputs)
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}