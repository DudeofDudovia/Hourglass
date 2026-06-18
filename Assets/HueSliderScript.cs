using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HueSliderScript : MonoBehaviour
{
    public Slider Slider;


    public bool Ishovering = false;
    public bool MouseDown = false;
    public bool MouseD2 = false;
    public bool MouseUp = false;
    public int DoubleClick = 0;
    public bool Adjusted = false;
    public TextMeshProUGUI TMP;
    public TimeControllerScript TCS;
    public int OldProfile;
    public ExplainerTag ET;

    public void Start()
    {
        Adjusted = false;
    }
    public void Update()
    {
        if (Adjusted) { 
            TMP.color = Color.HSVToRGB(Slider.value, 1, 1);
            ET.NameColor = Color.HSVToRGB(Slider.value, 1, 1);
        }

    }

    public void AdjHue()
    {
        Adjusted = true;
    }
    public void CloseMenu()
    {
        Adjusted = false;
    }
}
