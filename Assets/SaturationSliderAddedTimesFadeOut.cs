using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.LowLevelPhysics2D.PhysicsWorld;

public class SaturationSliderAddedTimesFadeOut : MonoBehaviour
{
    public Slider Slider;
    public void SaveSlider()
    {
        PlayerPrefs.SetFloat(TCS.Profile + "SaturationAddedTimesFadeOut", Slider.value);
        PlayerPrefs.Save();
    }
    public void Awake()
    {
        LoadProfile();
    }
    public void LoadProfile()
    {
        if (PlayerPrefs.HasKey(TCS.Profile + "SaturationAddedTimesFadeOut"))
        {
            Slider.value = PlayerPrefs.GetFloat(TCS.Profile + "SaturationAddedTimesFadeOut");
        }
        else { Slider.value = 1; }
    }
    public bool Ishovering = false;
    public bool MouseDown = false;
    public bool MouseD2 = false;
    public bool MouseUp = false;
    public int DoubleClick = 0;
    public Slider HueSlider;
    public bool Adjusted = false;
    public TextMeshProUGUI TMP;
    public TimeControllerScript TCS;
    public int OldProfile;
    public void Start()
    {
        LoadProfile();
        Adjusted = false;
        TCS.AddedTimesFadeOutSat = Slider.value;
    }
    public void Update()
    {
        TCS.AddedTimesFadeOutSat = Slider.value;
        MouseDown = false;
        if (Input.GetMouseButton(0)) { MouseDown = true; }
        if (MouseD2 != MouseDown && DoubleClick <= 29) { MouseUp = true; }
        MouseD2 = MouseDown;
        Ishovering = IsPointerOver(GetEventSystemRaycastResults());
        if (DoubleClick > 0 && MouseDown && Ishovering && MouseUp)
        {
            Slider.value = 1;
        }
        if (Ishovering && MouseDown)
        {
            DoubleClick = 30;
        }
        DoubleClick--;
        if (MouseDown) { MouseUp = false; }
        if (Adjusted) { TMP.color = Color.HSVToRGB(HueSlider.value, Slider.value, 1); }
        if (OldProfile != TCS.Profile)
        {
            LoadProfile();
        }
        OldProfile = TCS.Profile;
    }
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
    public void AdjSat()
    {
        Adjusted = true;
    }
    public void CloseMenu()
    {
        Adjusted = false;
    }
}
