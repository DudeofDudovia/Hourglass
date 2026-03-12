using UnityEngine;
using UnityEngine.UI;

public class MenuBackgroundScript : MonoBehaviour
{
    public Image BackgroundImage;
    public Slider HueSlider;
    public Slider SaturationSlider;
    public Slider ValueSlider;
    public Color DefaultColor;
    public bool Adjusted;
    public Color ClearColor;
    public bool Clear;
    public void Start()
    {
        Adjusted = false;
    }
    void Update()
    {
        if (Adjusted && !Clear) { BackgroundImage.color = Color.HSVToRGB(HueSlider.value, SaturationSlider.value, ValueSlider.value); }
        else if (Adjusted && Clear)
        {
            BackgroundImage.color = ClearColor;
        }
        else { 
            BackgroundImage.color = DefaultColor;
        }
    }
    public void AdjBackground()
    {
        Adjusted = true;
    }
    public void CloseMenu()
    {
        Adjusted = false;
    }
}
