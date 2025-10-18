using UnityEngine;

public class EarthOrbit : MonoBehaviour
{
    [Header("Orbit Settings")]
    public Transform sun;        // w ref to Sun's transform (orbit center)
    public float orbitDuration = 45f;  // Time (s) to complete one orbit

    [Header("Ellipse Parameters")]
    public float semiMajorAxis = 10f;   // Orbit radius along X-axis
    public float semiMinorAxis = 8f;    // Orbit radius along Z-axis

    private float orbitTimer = 0f;

    void Update()
    {
        if (sun == null) return;

        orbitTimer += Time.deltaTime;

        // Calculate orbit angle in rad (full orbit = 2 * PI)
        float orbitFraction = (orbitTimer / orbitDuration) % 1f;
        float angle = orbitFraction * 2f * Mathf.PI;

        // Elliptical orbit position relative to Sun
        float x = Mathf.Cos(angle) * semiMajorAxis;
        float z = Mathf.Sin(angle) * semiMinorAxis;

        // Set the position of EarthOrbit relative to Sun
        transform.position = sun.position + new Vector3(x, 0, z);
    }
}