using UnityEngine;
using UnityEngine.UI;

public class LoggedTimePreviewDisplay : MonoBehaviour
{
    public Image LoggedTimeImage;
    public Slider HueSlider;
    public Slider SaturationSlider;
    public Slider ValueSlider;
    void Update()
    {
        LoggedTimeImage.color = Color.HSVToRGB(HueSlider.value, SaturationSlider.value, ValueSlider.value);
    }
}
