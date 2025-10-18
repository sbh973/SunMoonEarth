    using UnityEngine;
    using UnityEngine.UI;

    public class WaypointIndicator : MonoBehaviour
    {
        public Transform target; // Assign target GameObject (Sun at (0,0,0))
        public Image indicatorImage; // Assign UI Image
        public float borderOffset = 10f; // Adjust to prevent indicator from hugging screen edge

        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;
            if (indicatorImage == null)
            {
                indicatorImage = GetComponent<Image>();
            }
        }

        void Update()
        {
            if (target == null || mainCamera == null || indicatorImage == null)
                return;

            Vector3 screenPoint = mainCamera.WorldToScreenPoint(target.position);
            bool isOffscreen = screenPoint.z < 0 || 
                               screenPoint.x < 0 || screenPoint.x > Screen.width ||
                               screenPoint.y < 0 || screenPoint.y > Screen.height;

            if (isOffscreen)
            {
                // Calculate direction and rotate indicator
                Vector3 dir = (target.position - mainCamera.transform.position).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                indicatorImage.rectTransform.localEulerAngles = new Vector3(0, 0, angle - 90); // Adjust for sprite orientation

                // Clamp position to screen edges
                screenPoint.x = Mathf.Clamp(screenPoint.x, borderOffset, Screen.width - borderOffset);
                screenPoint.y = Mathf.Clamp(screenPoint.y, borderOffset, Screen.height - borderOffset);

                indicatorImage.rectTransform.position = screenPoint;
                indicatorImage.enabled = true; // Show indicator
            }
            else
            {
                // If on-screen, position directly on target or hide/change indicator
                indicatorImage.rectTransform.position = screenPoint;
                indicatorImage.rectTransform.localEulerAngles = Vector3.zero; // Reset rotation
                indicatorImage.enabled = true; // Show indicator
                // Idea: Change sprite to a "found" indicator?
            }
        }
    }
