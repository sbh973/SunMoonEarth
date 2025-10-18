using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrbitScaler : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public EllipticalOrbit earthOrbit;
    public EllipticalOrbit moonOrbit;
    public Slider orbitSlider;
    public TMP_Text orbitText;

    [Header("Orbit Radii")]
    public float earthStylized = 5f;
    public float earthRealistic = 150f;
    public float moonStylized = 1.5f;
    public float moonRealistic = 5f;

    [Header("Text Positioning")]
    public float xOffset = 0f;  // horizontal offset from slider center
    public float yOffset = 30f; // vertical offset above slider

    private RectTransform textRect;
    private RectTransform sliderRect;

    private void Awake()
    {
        if (orbitText != null)
            textRect = orbitText.GetComponent<RectTransform>();
        if (orbitSlider != null)
            sliderRect = orbitSlider.GetComponent<RectTransform>();
    }

    private void Start()
    {
        orbitSlider.onValueChanged.AddListener(UpdateOrbitRadius);
        UpdateOrbitRadius(orbitSlider.value); // initialize
    }

    private void Update()
    {
        // Move text above the slider with offsets
        if (textRect != null && sliderRect != null)
        {
            Vector3 sliderWorldPos = sliderRect.position;
            textRect.position = sliderWorldPos + new Vector3(xOffset, yOffset, 0f);
        }
    }

    void UpdateOrbitRadius(float sliderValue)
    {
        // Earth orbit
        float earthRadius = Mathf.Lerp(earthStylized, earthRealistic, sliderValue);
        earthOrbit.semiMajorAxis = earthRadius;
        earthOrbit.semiMinorAxis = earthRadius;

        // Moon orbit
        float moonRadius = Mathf.Lerp(moonStylized, moonRealistic, sliderValue);
        moonOrbit.semiMajorAxis = moonRadius;
        moonOrbit.semiMinorAxis = moonRadius;

        // Update UI text
        float realismPercent = Mathf.Round(sliderValue * 100f);
        if (orbitText != null)
        {
            orbitText.text =
                $"Earth Orbit: {earthRadius:F1}\n" +
                $"Moon Orbit: {moonRadius:F1}\n" +
                $"Scale: {realismPercent}% realistic";
        }
    }
}