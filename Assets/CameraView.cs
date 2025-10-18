using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class CameraController_NewInput : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSpeed = 2f;

    private PlayerControls controls;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float upDownInput;

    private float yaw = 0f;
    private float pitch = 0f;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Camera.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Camera.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Camera.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Camera.Look.canceled += ctx => lookInput = Vector2.zero;

        controls.Camera.UpDown.performed += ctx => upDownInput = ctx.ReadValue<float>();
        controls.Camera.UpDown.canceled += ctx => upDownInput = 0f;
    }

    private void OnEnable()
    {
        controls.Camera.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        controls.Camera.Disable();
    }

    private void Update()
    {
        if (Time.timeScale == 0f || EventSystem.current.IsPointerOverGameObject())
            return; // freeze camera movement while paused

        // Rotate camera
        yaw += lookInput.x * lookSpeed;
        pitch -= lookInput.y * lookSpeed;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);

        // Move camera
        Vector3 direction = transform.forward * moveInput.y + transform.right * moveInput.x + Vector3.up * upDownInput;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}