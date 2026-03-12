using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddedTimesFadeOutAnimScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Slider Slider;
    public TimeControllerScript TCS;
    public void SaveSlider()
    {
        TCS.DeletedProfLifeTime = (int)Slider.value;
        PlayerPrefs.SetFloat("AddedTimesFadeOutAnimTime", Slider.value);
        PlayerPrefs.Save();
    }
    public void Awake()
    {
        if (PlayerPrefs.HasKey("AddedTimesFadeOutAnimTime"))
        {
            if (PlayerPrefs.GetFloat("AddedTimesFadeOutAnimTime") > 150)
            {
                Slider.maxValue = PlayerPrefs.GetFloat("AddedTimesFadeOutAnimTime");
            }
            Slider.value = PlayerPrefs.GetFloat("AddedTimesFadeOutAnimTime");
        }
        else { Slider.value = 120; }
    }
    public bool Ishovering = false;
    public bool MouseDown = false;
    public bool MouseD2 = false;
    public bool MouseUp = false;
    public int DoubleClick = 0;
    public bool Adjusted = false;
    public void Start()
    {
        Adjusted = false;
    }
    public void Update()
    {
        MouseDown = false;
        if (Input.GetMouseButton(0)) { MouseDown = true; }
        if (MouseD2 != MouseDown && DoubleClick <= 29) { MouseUp = true; }
        MouseD2 = MouseDown;
        Ishovering = IsPointerOver(GetEventSystemRaycastResults());
        if (DoubleClick > 0 && MouseDown && Ishovering && MouseUp)
        {
            Slider.value = 120f;
            TCS.DeletedProfLifeTime = (int)Slider.value;
        }
        if (Ishovering && MouseDown)
        {
            DoubleClick = 30;
        }
        DoubleClick--;
        if (MouseDown) { MouseUp = false; }
        if (Adjusted) { }

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
}
