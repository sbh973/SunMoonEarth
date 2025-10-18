using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectScaleSliderWithText : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider scaleSlider;
    public TMP_Text scaleText;

    [Header("Objects to Scale")]
    public Transform sun;
    public Transform earth;
    public Transform moon;

    [Header("Base Scales (slider = 1)")]
    public Vector3 sunBaseScale = new(2f, 2f, 2f);
    public Vector3 earthBaseScale = new(1f, 1f, 1f);
    public Vector3 moonBaseScale = new(0.27f, 0.27f, 0.27f);

    [Header("Scale Limits")]
    public float minScale = 0.5f;
    public float maxScale = 2f;

    private void Start()
    {
        if (!scaleSlider)
        {
            Debug.LogError("Scale slider not assigned!");
            enabled = false; // disable script to avoid null refs (just incase)
            return;
        }

        scaleSlider.minValue = minScale;
        scaleSlider.maxValue = maxScale;
        scaleSlider.wholeNumbers = false;
        scaleSlider.value = 1f;

        scaleSlider.onValueChanged.AddListener(UpdateObjectScale);
        UpdateObjectScale(scaleSlider.value);
    }

    private void UpdateObjectScale(float value)
    {
        if (sun) sun.localScale = sunBaseScale * value;
        if (earth) earth.localScale = earthBaseScale * value;
        if (moon) moon.localScale = moonBaseScale * value;

        if (scaleText)
            scaleText.text = $"Scale: {value:F2}×";
    }
}