using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
public class AppBackgroundScript : MonoBehaviour
{
    public Image BackgroundImage;
    public Slider HueSlider;
    public Slider SaturationSlider;
    public Slider ValueSlider;
    public Material HSMat;
    void Update()
    {
            //HSMat.SetFloat("_Hue", WrapOne((float)HueSlider.value - 0.3122357f));
            HSMat.SetFloat("_Hue", WrapOne((float)HueSlider.value - 1f/3f));
            HSMat.SetFloat("_Val", SaturationSlider.value);
            HSMat.SetFloat("_Sat", ValueSlider.value);
    }
    private float WrapOne(float input)
    {
        float output = input;
        if (output < 0)
        {
            output = 1 - - input;
        }
        return output;
    }
}
