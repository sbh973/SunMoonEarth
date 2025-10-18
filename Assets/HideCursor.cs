using UnityEngine;

    public class HideCursor : MonoBehaviour
    {
        void Start()
        {
            Cursor.visible = false; // Set cursor to invisible
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Idea: press key to show cursor?
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // Press Esc to show cursor
            {
                Cursor.visible = true;
            }
        }
    }