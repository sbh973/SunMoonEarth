using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrbitalScaleUI : MonoBehaviour
{
    [Header("Slider & Text")]
    public Slider orbitalScaleSlider;   // assign in inspector
    public TMP_Text orbitalScaleText;   // assign in inspector

    [Header("Original Orbit Parameters - Earth")]
    public float baseSemiMajorEarth = 20f;
    public float baseSemiMinorEarth = 18f;

    [Header("Original Orbit Parameters - Moon")]
    public float baseSemiMajorMoon = 5f;
    public float baseSemiMinorMoon = 4.8f;

    private float scaledSemiMajorEarth;
    private float scaledSemiMinorEarth;
    private float scaledSemiMajorMoon;
    private float scaledSemiMinorMoon;

    void Update()
    {
        if (orbitalScaleSlider == null || orbitalScaleText == null) return;

        float scale = orbitalScaleSlider.value;

        // Scale Earth orbit
        scaledSemiMajorEarth = baseSemiMajorEarth * scale;
        scaledSemiMinorEarth = baseSemiMinorEarth * scale;

        // Scale Moon orbit
        scaledSemiMajorMoon = baseSemiMajorMoon * scale;
        scaledSemiMinorMoon = baseSemiMinorMoon * scale;

        // Display
        orbitalScaleText.text =
            $"Scale: {scale:F2}\n" +
            $"Earth → a = {scaledSemiMajorEarth:F2}, b = {scaledSemiMinorEarth:F2}\n" +
            $"Moon  → a = {scaledSemiMajorMoon:F2}, b = {scaledSemiMinorMoon:F2}";
    }
}