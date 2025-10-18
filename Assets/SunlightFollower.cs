using UnityEngine;

public class SunlightFollower : MonoBehaviour
{
    public Transform sun;
    public Transform earth;

    void Update()
    {
        if (sun == null || earth == null) return;

        Vector3 direction = (earth.position - sun.position).normalized;

        transform.rotation = Quaternion.LookRotation(direction);

        transform.position = sun.position;

        Debug.DrawRay(sun.position, direction * 10f, Color.yellow);
    }
}