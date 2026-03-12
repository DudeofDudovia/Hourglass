using UnityEngine;
using UnityEngine.UI;
public class AppBackgroundScript : MonoBehaviour
{
    public Image BackgroundImage;
    public Slider HueSlider;
    public Slider SaturationSlider;
    public Slider ValueSlider;

    void Update()
    {
            BackgroundImage.color = Color.HSVToRGB(HueSlider.value, SaturationSlider.value, ValueSlider.value);
    }
}
