using UnityEngine;
using UnityEngine.UI;
public class NotificationWarningTimeMaxSlider : MonoBehaviour
{
    public Slider Slide;
    public SavableValueScript SVS;
    public TimeControllerScript TCS;
    void Update()
    {
        Slide.maxValue = TCS.DefaultTime;
        SVS.SliderMaxValue = TCS.DefaultTime;
    }
}
