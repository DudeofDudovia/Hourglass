using UnityEngine;
using UnityEngine.UI;
public class NotificationWarningTimeMaxSlider : MonoBehaviour
{
    public Slider Slide;
    public SavableValueScript SVS;
    public TimeControllerScript TCS;
    void Update()
    {
        if (SVS.Slide.value < TCS.DefaultTime) { SVS.SliderMaxValue = TCS.DefaultTime;}
    }
}
