using UnityEngine;

public class EllipticalOrbit : MonoBehaviour
{
    public Transform orbitCenter;          // objects orbit around
    public float semiMajorAxis = 20f;      // a
    public float semiMinorAxis = 18f;      // b
    public float orbitSpeed = 1f;          // (rad/s)

    private float angle = 0f;

    void Update()
    {
        if (orbitCenter == null) return;

        angle += orbitSpeed * Time.deltaTime;

        float x = semiMajorAxis * Mathf.Cos(angle);
        float z = semiMinorAxis * Mathf.Sin(angle);

        transform.position = orbitCenter.position + new Vector3(x, 0f, z);
    }
}