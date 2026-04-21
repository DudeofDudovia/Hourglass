using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SavableValueScript : MonoBehaviour
{
    public string Label;
    public bool Default;
    public Toggle Togg;
    public Slider Slide;
    public float SliderMinValue = -792;
    public float SliderMaxValue = -792;
    public TimeControllerScript TCS;
    public int OldProf;
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

    public bool LoadOnProfileChange;
    public bool FirstFrame = true;

    public bool Resetable = false;
    
    public bool ForceLoadSaved = false;

    public void SaveValue()
    {
        if (FirstFrame) { return; }
        if (toggle)
        {
            int tog = 0;
            if (Togg.isOn) { tog = 1; }
            if (VarToToggle != null)
            {
                VarToToggle.Invoke(Togg.isOn);
            }
            TCS.MKfile(ObjectLayer, DataLayer, tog.ToString(), Default);
            
        }
        if (flot)
        {
            VarToFloat.Invoke(Slide.value);
            TCS.MKfile(ObjectLayer, DataLayer, Slide.value.ToString(), Default);
            if (VarToFloat != null)
            {
                VarToFloat.Invoke(Slide.value);
            }
            if (Slide.value > SliderMinValue) { Slide.minValue = SliderMinValue; }
            if (Slide.value < SliderMaxValue) { Slide.maxValue = SliderMaxValue; }
        }
        if (integr)
        {
            VarToInt.Invoke((int)Slide.value);
            TCS.MKfile(ObjectLayer, DataLayer, Slide.value.ToString(), Default);
            if (VarToInt != null)
            {
                VarToInt.Invoke((int)Slide.value);
            }
            if (Slide.value > SliderMinValue) { Slide.minValue = SliderMinValue; }
            if (Slide.value < SliderMaxValue) { Slide.maxValue = SliderMaxValue; }
        }
    }
    public void Awake()
    {
       
        if (TCS == null) { TCS = Object.FindFirstObjectByType<TimeControllerScript>().gameObject.GetComponent<TimeControllerScript>(); }
        int Prof = 0;
        try { Prof = int.Parse(TCS.RDfile(0, 0, true)); }
        catch { Prof = 0; }
        LoadProfile(Prof);
    }
    public void Update()
    {


        FirstFrame = false;
        if (LoadOnProfileChange)
        {
            if (OldProf != TCS.Profile)
            {
                LoadProfile(TCS.Profile);
            }
            OldProf = TCS.Profile;
        }
        if (TCS.ResetValues)
        {
            LoadProfile(TCS.Profile);
        }
        if (TCS.BigReset)
        {
            LoadProfile(-2);
        }
        if (Resetable)
        {
            MouseDown = false;
            if (Input.GetMouseButton(0)) { MouseDown = true; }
            if (MouseD2 != MouseDown && DoubleClick <= 29) { MouseUp = true; }
            MouseD2 = MouseDown;
            Ishovering = IsPointerOver(GetEventSystemRaycastResults());
            if (DoubleClick > 0 && MouseDown && Ishovering && MouseUp)
            {
                Slide.value = DefaultFloat;
                if (integr) { Slide.value = DefaultInt; }
            }
            if (Ishovering && MouseDown)
            {
                DoubleClick = 30;
            }
            DoubleClick--;
            if (MouseDown) { MouseUp = false; }
        }

        if (ForceLoadSaved)
        {
            LoadProfile(TCS.Profile);
        }
    }
    public void LoadProfile()
    {
        LoadProfile(-1);
    }
    public void LoadProfile(int Prof)
    {
        if (Prof == -1) { Prof = TCS.Profile; }
       
        if (toggle)
        {
            if (Prof != -2) {
                try
                {
                    int tog = int.Parse(TCS.RDfile(ObjectLayer, DataLayer, Default));
                    if (tog == -792)
                    {
                        Togg.isOn = DefaultBool;
                    }
                    if (tog == 1) { Togg.isOn = true; }
                    if (tog == 0) { Togg.isOn = false; }

                }
                catch { Togg.isOn = DefaultBool; }
            }
            else { Togg.isOn = DefaultBool; }
            if (VarToToggle != null)
            {
                VarToToggle.Invoke(Togg.isOn);
            }
        }
        if (flot)
        {
            if (SliderMaxValue == -792) { SliderMaxValue = Slide.maxValue; }
            else { SliderMaxValue = Slide.maxValue; }
            if (SliderMinValue == -792) { SliderMinValue = Slide.minValue; }
            else { SliderMinValue = Slide.minValue; }
            if (Prof != -2)
            {
                try
                {
                    Slide.maxValue = SliderMaxValue;
                    Slide.minValue = SliderMinValue;
                    float val = float.Parse(TCS.RDfile(ObjectLayer, DataLayer, Default));
                    if (val == -792)
                    {
                        val = DefaultFloat;
                    }
                    if (val > Slide.maxValue) { Slide.maxValue = val; }
                    if (val < Slide.minValue) { Slide.minValue = val; }
                    Slide.value = val;
                }
                catch { Slide.value = DefaultFloat; }
            }
            else
            {
                try
                {
                    Slide.maxValue = SliderMaxValue;
                    Slide.minValue = SliderMinValue;
                }
                catch
                {
                    Slide.maxValue = 1;
                    Slide.minValue = 0;
                }
                Slide.value = DefaultFloat;
            }

            if (VarToFloat != null)
            {
                VarToFloat.Invoke(Slide.value);
            }
        }
        if (integr)
        {
            if (DefaultInt == -1) { DefaultInt = (int)DefaultFloat; }


            if (SliderMaxValue == -792) { SliderMaxValue = Slide.maxValue; }
            else { SliderMaxValue = Slide.maxValue; }
            if (SliderMinValue == -792) { SliderMinValue = Slide.minValue; }
            else { SliderMinValue = Slide.minValue; }
            if (Prof != -2)
            {
                try
                {
                    Slide.maxValue = SliderMaxValue;
                    Slide.minValue = SliderMinValue;
                    float vali = int.Parse(TCS.RDfile(ObjectLayer, DataLayer, Default));
                    if (vali == -792)
                    {
                        vali = DefaultInt;
                    }
                    if (vali > Slide.maxValue) { Slide.maxValue = vali; }
                    if (vali < Slide.minValue) { Slide.minValue = vali; }
                    Slide.value = vali;
                }
                catch { Slide.value = DefaultInt; }
            }
            else
            {
                try
                {
                    Slide.maxValue = SliderMaxValue;
                    Slide.minValue = SliderMinValue;
                }
                catch
                {
                    Slide.maxValue = 1;
                    Slide.minValue = 0;
                }
                Slide.value = DefaultInt;
            }
            if (VarToInt != null)
            {
                VarToInt.Invoke((int)Slide.value);
            }
        }
    }
    //Resetable
    public bool Ishovering = false;
    public bool MouseDown = false;
    public bool MouseD2 = false;
    public bool MouseUp = false;
    public int DoubleClick = 0;
    public bool IsPointerOver(List<RaycastResult> eventSystemRaycastResults)
    {
        for (int i = 0; i < eventSystemRaycastResults.Count; i++)
        {
            RaycastResult result = eventSystemRaycastResults[i];
            if (result.gameObject.layer == LayerMask.NameToLayer("Resetable Slider") && result.gameObject.transform.position == transform.position)
                return true;
        }
        return false;
    }
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults;
    }
}


