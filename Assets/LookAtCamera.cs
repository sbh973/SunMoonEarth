using UnityEngine;

    public class LookAtCamera : MonoBehaviour
    {
        void Update()
        {
            // Make the waypoint look at the main cam position
            transform.LookAt(Camera.main.transform.position);
        }
    }