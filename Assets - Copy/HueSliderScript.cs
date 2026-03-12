using System.Collections.Generic;
using System.IO.Pipes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HueSliderScript : MonoBehaviour
{
    public Slider Slider;

    public void SaveSlider()
    {
        PlayerPrefs.SetFloat("Hue", Slider.value);
        PlayerPrefs.Save();
    }
    public void Awake()
    {
        if (PlayerPrefs.HasKey("Hue"))
        {
            Slider.value = PlayerPrefs.GetFloat("Hue");
        }
        else { Slider.value = 0.1719923f; }
    }

    public bool Ishovering = false;
    public bool MouseDown = false;
    public bool MouseD2 = false;
    public bool MouseUp = false;
    public int DoubleClick = 0;
    public bool Adjusted = false;
    public TextMeshProUGUI TMP;
    public void Start()
    {
        Adjusted = false;
    }
    public void Update()
    {
        MouseDown = false;
        if (Input.GetMouseButton(0)) { MouseDown = true;  }
        if (MouseD2 != MouseDown && DoubleClick <= 29) { MouseUp = true; }
        MouseD2 = MouseDown;
        Ishovering = IsPointerOver(GetEventSystemRaycastResults());
        if (DoubleClick > 0 && MouseDown && Ishovering && MouseUp)
        {
            Slider.value = 0.1719923f;
        }
        if (Ishovering && MouseDown)
        {
            DoubleClick = 30;
        }
        DoubleClick--;
        if (MouseDown) { MouseUp = false; }
        if (Adjusted) { TMP.color = Color.HSVToRGB(Slider.value, 1, 1); }
        
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
    public void AdjHue()
    {
        Adjusted = true;
    }
    public void CloseMenu()
    {
        Adjusted = false;
    }
}
