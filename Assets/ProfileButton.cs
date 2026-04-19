using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public int AddedId = 0;
    public string ProfileName = "Profile ";

    public bool BClicked1 = false;
    public bool BClicked2 = false;
    public bool MouseDown = false;
    public bool MouseD2 = false;
    public bool MouseUp = false;
    public bool WasMouseUp = false;
    public float initpos = 0;
    public float initposY = 0;
    public float diff = 0;
    public float exitdiff = 300;
    public bool Ishovering = false;
    public float OriginalPosX = 0;
    public float OriginalPosY = 0;
    public float diffY = 0;
    public float exitdiffY = 40;
    public Image ButtonToColorChange;
    public TimeControllerScript TCS;
    private void Start()
    {
        if (TCS == null) { TCS = Object.FindFirstObjectByType<TimeControllerScript>().gameObject.GetComponent<TimeControllerScript>(); }
        AddedId = transform.parent.GetComponent<TimesAdded>().TimesAppeneded;
        transform.parent.GetComponent<TimesAdded>().TimesAppeneded += 1;
        transform.localPosition = new Vector3(0, AddedId * -30 + 10, 0);

        transform.localPosition = new Vector3(98, AddedId * -30 + 10, 0);
        ProfileName = PlayerPrefs.GetString("Profile" + AddedId.ToString() + "Name", "Profile " + AddedId);
        try
        {
            ProfileName = TCS.RDfile(1, AddedId, true);
        }
        catch { }
        string ProfName = ("Profile" + AddedId.ToString()).ToString();
        try
        {
            if (int.Parse(TCS.RDfile(1, AddedId, true)) == -792) { ProfileName = ProfName; }
        }
        catch { }
        
    }
    public bool IsPointerOver(List<RaycastResult> eventSystemRaycastResults)
    {
        for (int i = 0; i < eventSystemRaycastResults.Count; i++)
        {
            RaycastResult result = eventSystemRaycastResults[i];
            if (result.gameObject.layer == LayerMask.NameToLayer("AddedProfiles") && result.gameObject.transform.position == transform.position)
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
    void Update()
    {
        transform.localPosition = new Vector3(94.25f, (AddedId + 1) * -33 + 10, 0);
        TMP.text = ProfileName;


        MouseDown = false;
        if (Input.GetMouseButton(0)) { MouseDown = true; }
        ButtonToColorChange.color = Color.white;
        if (MouseUp) { WasMouseUp = true; }
        if (BClicked1 && !BClicked2 && MouseUp)
        {
            WasMouseUp = true;
            OriginalPosX = transform.position.x;
            OriginalPosY = transform.position.y;
            initpos = Input.mousePosition.x;
            initposY = Input.mousePosition.y;
            BClicked2 = true;
        }
        if (BClicked1 && BClicked2 && WasMouseUp)
        {

            diff = Input.mousePosition.x - initpos;
            diffY = Input.mousePosition.y - initposY;
            transform.position += new Vector3(diff, 0, 0);
            diff = Mathf.Abs(diff);

            if (diff > exitdiff) { ButtonToColorChange.color = Color.red; }
            if (Mathf.Abs(diffY) > exitdiffY)
            {
                BClicked1 = false;
                BClicked2 = false;
            }
        }
        if (!BClicked1 && BClicked2)
        {
            if (diff > exitdiff) { RemoveProf(); }
            BClicked2 = false;
        }
        if (!BClicked2)
        {
            WasMouseUp = false;
        }
        if (!MouseDown) { BClicked1 = false; }
        Ishovering = IsPointerOver(GetEventSystemRaycastResults());
        if (MouseDown && Ishovering) { BClicked1 = true; }
        if (MouseDown) { MouseUp = false; }
        if (MouseD2 != MouseDown && Ishovering) { MouseUp = true; }
        MouseD2 = MouseDown;
    }
    public void ChangeProfile()
    {
        GameObject[] TimeMarkers = GameObject.FindGameObjectsWithTag("TimeController");
        TimeMarkers[0].GetComponent<TimeControllerScript>().UpdateProfile(AddedId);
        
    }
    public void RemoveProf()
    {
        gameObject.SetActive(false); Destroy(gameObject);
        GameObject TCS = Object.FindFirstObjectByType<TimeControllerScript>().gameObject;
        TCS.GetComponent<TimeControllerScript>().DelProf(AddedId);
    }
}
