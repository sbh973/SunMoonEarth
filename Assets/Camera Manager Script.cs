using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Targets")]
    public Transform sun;
    public Transform earth;
    public Transform moon;

    [Header("Focus Settings")]
    public float distance = 10f;
    public float minDistance = 3f;
    public float maxDistance = 50f;
    public float orbitSpeed = 100f;
    public float zoomSpeed = 5f;

    private Transform target;
    private bool freeMode = true;

    private float yaw = 0f;
    private float pitch = 20f;

    private CameraController_NewInput freeCamera;

    void Start()
    {
        freeCamera = GetComponent<CameraController_NewInput>();
        EnableFreeMode();
    }

    void Update()
    {
        // Toggling free/focus
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (freeMode) EnableFocusMode(earth);
            else EnableFreeMode();
        }

        if (!freeMode)
        {
            // Switching the target
            if (Input.GetKeyDown(KeyCode.Alpha1)) EnableFocusMode(sun);
            if (Input.GetKeyDown(KeyCode.Alpha2)) EnableFocusMode(earth);
            if (Input.GetKeyDown(KeyCode.Alpha3)) EnableFocusMode(moon);

            // Mouse orbit
            if (Input.GetMouseButton(1))
            {
                yaw += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
                pitch -= Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;
                pitch = Mathf.Clamp(pitch, -80f, 80f);
            }

            // Mouse scroll/zoom
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);

            // Touch pinch/zoom
            if (Input.touchCount == 2)
            {
                Touch t0 = Input.GetTouch(0);
                Touch t1 = Input.GetTouch(1);

                float prevDist = (t0.position - t0.deltaPosition - (t1.position - t1.deltaPosition)).magnitude;
                float curDist = (t0.position - t1.position).magnitude;
                float pinchDelta = prevDist - curDist;

                distance = Mathf.Clamp(distance + pinchDelta * 0.01f, minDistance, maxDistance);
            }
        }
    }

    void LateUpdate()
    {
        if (!freeMode && target != null)
        {
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 offset = rotation * new Vector3(0, 0, -distance);

            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }

    void EnableFreeMode()
    {
        freeMode = true;
        freeCamera.enabled = true;
        target = null;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void EnableFocusMode(Transform newTarget)
    {
        freeMode = false;
        freeCamera.enabled = false;
        target = newTarget;
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
