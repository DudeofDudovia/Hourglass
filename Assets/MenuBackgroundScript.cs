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
    public Material HSMat;
    public void Start()
    {
        Adjusted = false;
    }
    void Update()
    {
        if (Adjusted && !Clear) { 
            HSMat.SetFloat("_Hue", WrapOne((float)HueSlider.value - 1f / 3f));
            HSMat.SetFloat("_Val", SaturationSlider.value);
            HSMat.SetFloat("_Sat", ValueSlider.value);
        }
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
    private float WrapOne(float input)
    {
        float output = input;
        if (output < 0)
        {
            output = 1 - -input;
        }
        return output;
    }
}
