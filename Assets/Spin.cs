using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinSpeed = 30f; // Degrees per second. But this is arbitrary. Change to more exact later?

    void Update()
    {
        // Now, as seen from the Celestial north pole (top), the rotation on the axis is counterclockwise
        transform.Rotate(Vector3.up, -spinSpeed * Time.deltaTime);
    }
}