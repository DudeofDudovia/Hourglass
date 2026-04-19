using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ValueSliderScript : MonoBehaviour
{
    public Slider Slider;
    public bool Ishovering = false;
    public bool MouseDown = false;
    public bool MouseD2 = false;
    public bool MouseUp = false;
    public int DoubleClick = 0;
    public Slider HueSlider;
    public Slider SatSlider;
    public bool Adjusted = false;
    public TextMeshProUGUI TMP;
    public TimeControllerScript TCS;
    public int OldProfile;
    public void Start()
    {
        Adjusted = false;
    }
    public void Update()
    {
        if (Adjusted) { TMP.color = Color.HSVToRGB(HueSlider.value, SatSlider.value, Slider.value); }
    }
    public void AdjVal()
    {
        Adjusted = true;
    }
    public void CloseMenu()
    {
        Adjusted = false;
    }
}
