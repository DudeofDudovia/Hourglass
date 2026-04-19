using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangelogScript : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    void Start()
    {
        TMP.text = File.ReadAllText(Application.dataPath + @"\ChangeLog.txt");
    }
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
    public void Update()
    {
        MouseDown = false;
        if (Input.GetMouseButton(0)) { MouseDown = true; }
        Ishovering = IsPointerOver(GetEventSystemRaycastResults());
        if (MouseD2 != MouseDown) { MouseUp = true; }
        MouseD2 = MouseDown;
        if (Ishovering && MouseDown && MouseUp)
        {
            Application.OpenURL("https://github.com/DudeofDudovia/Hourglass");
        }
    }
}
