using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResetableValueScript : MonoBehaviour
{
    public bool Ishovering = false;
    public bool MouseDown = false;
    public bool MouseD2 = false;
    public bool MouseUp = false;
    public int DoubleClick = 0;
    public Slider Slide;
    public SavableValueScript SVS;
    public bool integ;
    public void Update()
    {
        MouseDown = false;
        if (Input.GetMouseButton(0)) { MouseDown = true; }
        if (MouseD2 != MouseDown && DoubleClick <= 29) { MouseUp = true; }
        MouseD2 = MouseDown;
        Ishovering = IsPointerOver(GetEventSystemRaycastResults());
        if (DoubleClick > 0 && MouseDown && Ishovering && MouseUp)
        {
            Slide.value = SVS.DefaultFloat;
            if (SVS.integr) { Slide.value = SVS.DefaultInt; }
        }
        if (Ishovering && MouseDown)
        {
            DoubleClick = 30;
        }
        DoubleClick--;
        if (MouseDown) { MouseUp = false; }

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
