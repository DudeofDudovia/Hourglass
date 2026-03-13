using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class SavableValueScript : MonoBehaviour
{
    public string Label;
    public Toggle Togg;
    public Slider Slide;
    public float SliderMaxValue = -1;
    public TimeControllerScript TCS;
    public int ObjectLayer = 3;
    public int DataLayer = 0;
    public UnityEvent<bool> VarToToggle;
    public UnityEvent<float> VarToFloat;
    public UnityEvent<int> VarToInt;
    public bool toggle;
    public bool DefaultBool = false;
    public bool flot;
    public float DefaultFloat = -1;
    public bool integr;
    public int DefaultInt = -1;
    public void SaveValue()
    {
        if (toggle)
        {
            VarToToggle.Invoke(Togg.isOn);
            int tog = 0;
            if (Togg.isOn) { tog = 1; }
            TCS.MKfile(ObjectLayer, DataLayer, tog.ToString());
        }
        if (flot)
        {
            VarToFloat.Invoke(Slide.value);
            TCS.MKfile(ObjectLayer, DataLayer, Slide.value.ToString());
        }

    }
    public void Awake()
    {
        if (toggle)
        {
            try
            {
                int tog = int.Parse(TCS.RDfile(ObjectLayer, DataLayer));
                if (tog == 1) { Togg.isOn = true; }
                if (tog == 0) { Togg.isOn = false; }
            }
            catch { Togg.isOn = DefaultBool; }
            if (VarToToggle != null)
            {
                VarToToggle.Invoke(Togg.isOn);
            }
        }
        if (flot)
        {
            if (SliderMaxValue == -1) { SliderMaxValue = Slide.maxValue; }
            else { SliderMaxValue = Slide.maxValue; }
            try
            {
                float val = float.Parse(TCS.RDfile(ObjectLayer, DataLayer));
                if (val > SliderMaxValue) { SliderMaxValue = val; }
                Slide.value = val;
            }
            catch { Slide.value = DefaultFloat; }
            if (VarToFloat != null)
            {
                VarToFloat.Invoke(Slide.value);
            }
        }
        if (integr)
        {
            if (DefaultInt == -1) { DefaultInt = (int)DefaultFloat; }
        }
    }
    public void SetSlider(float val)
    {
        if (val > SliderMaxValue) { SliderMaxValue = val; }
        Slide.value = val;
        VarToFloat.Invoke(Slide.value);
        TCS.MKfile(ObjectLayer, DataLayer, Slide.value.ToString());

    }
}


