using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SentNotificationScript : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public int AddedId = 0;
    //public string ProfileName = "Profile ";

    public long ScheduledTime;
    public int Channel;
    public string ChannelId;

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
        if (TCS == null) { TCS = UnityEngine.Object.FindFirstObjectByType<TimeControllerScript>().gameObject.GetComponent<TimeControllerScript>(); }
        AddedId = transform.parent.GetComponent<TimesAdded>().TimesAppeneded;
        transform.parent.GetComponent<TimesAdded>().TimesAppeneded += 1;
        transform.localPosition = new Vector3(0, AddedId * -30 + 10, 0);

       /* transform.localPosition = new Vector3(98, AddedId * -30 + 10, 0);
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
        catch { }*/

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
    public string findpattern()
    {
        string pattern = "h";
#if UNITY_ANDROID && !UNITY_EDITOR
        //var activity = UnityPlayer.GetStatic<AndroidJavaObject>("com.unity3d.player.UnityPlayer")
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var context = activity.Call<AndroidJavaObject>("getApplicationContext"))
        using (var dateFormat = new AndroidJavaClass("android.text.format.DateFormat"))
        {
            if (dateFormat.CallStatic<bool>("is24HourFormat", context)) { pattern = "H"; }
            if (dateFormat.CallStatic<bool>("is24HourFormat", activity)) { pattern = "H"; }
        }
#endif
#if PLATFORM_STANDALONE_WIN// || UNITY_EDITOR_WIN
        StringBuilder sb = new StringBuilder(80);

        GetLocaleInfoEx(
            null,
            LOCALE_STIMEFORMAT,
            sb,
            sb.Capacity
            );
        pattern = sb.ToString();
#endif
        return pattern;
    }
    void Update()
    {
        transform.localPosition = new Vector3(80, (AddedId + 1) * -33 + 10, 0);
        string pattern = findpattern();


        TMP.text = DateTime.FromBinary(ScheduledTime).ToString("t", CultureInfo.CurrentCulture);
        if (pattern.Contains("H"))
        {
            TMP.text = System.DateTime.FromBinary(ScheduledTime).ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
        }
        else
        {
            TMP.text = System.DateTime.FromBinary(ScheduledTime).ToString();
        }

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
        GameObject TCS = UnityEngine.Object.FindFirstObjectByType<TimeControllerScript>().gameObject;
        TCS.GetComponent<TimeControllerScript>().DelProf(AddedId);
    }
    public void ShowInfo()
    {

        string pattern = findpattern();


        //TMP.text = DateTime.FromBinary(ScheduledTime).ToString("t", CultureInfo.CurrentCulture);
        string Sched = "9 P.M.";
        if (pattern.Contains("H"))
        {
            Sched = System.DateTime.FromBinary(ScheduledTime).ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
        }
        else
        {
            Sched = System.DateTime.FromBinary(ScheduledTime).ToString();
        }

        string Name = "Sent Warning";
        string Type = "Custom Warning";
        string Text = "Yay!";
        if (Channel == 8)
        {
            Type = "Time Empty";
            Text = "This warning will go off when the timer exceeds the remaining time.\n This will happen at: " + Sched;
        }
        if (Channel == 9)
        {
            Type = "Time Low";
            Text = "This warning will go off when the timer is the warning minutes away from being empty.\n This will happen at: " + Sched;
        }
        if (Channel == 10)
        {
            Type = "Half Time";
            Text = "This warning will go off when the timer reaches half remaining time.\n This will happen at: " + Sched;
        }
        if (Channel == 11)
        {
            Type = "Time Used";
            Text = "This warning will go off when the timer runs for the Warn Time.\n This will happen at: " + Sched;
        }



        SentNotifInfoScript SNIS = UnityEngine.Object.FindFirstObjectByType<SentNotifInfoScript>();
        SNIS.ShowInfo(ScheduledTime,Name,Text,Type);
    }
    public void DisplaySchedueled()
    {
        /*
        if (TCS.gameObject.GetComponent<TimeControllerScript>() != null)
        {
            if (TCS.gameObject.GetComponent<TimeControllerScript>().MSAddeds)
            {
                if (Mathf.Abs(MinutesAddedDisplay) >= 60)
                {
                    TMP.text = (((long)(Truncate((MinutesAddedDisplay / 60f), 0))).ToString() + "H : " + Truncate(((float)MinutesAddedDisplay - ((long)(Truncate((MinutesAddedDisplay / 60f), 0))) * 60), 0).ToString() + "M : " + TCS.gameObject.GetComponent<TimeControllerScript>().TruncateForSeconds(seconds, 2) + "S");
                }
                else if (Mathf.Abs(MinutesAdded) >= 1)
                {
                    TMP.text = (Truncate(((float)MinutesAddedDisplay - ((long)(Truncate((MinutesAddedDisplay / 60f), 0))) * 60), 0).ToString() + "M : " + TCS.gameObject.GetComponent<TimeControllerScript>().TruncateForSeconds(seconds, 2) + "S");
                }
                else if (Mathf.Abs(MinutesAdded) < 1)
                {
                    TMP.text = (TCS.gameObject.GetComponent<TimeControllerScript>().TruncateForSeconds(seconds, 2) + "S");
                }
            }
            else
            {
                if (Mathf.Abs(MinutesAdded) >= 60)
                {
                    TMP.text = (((long)(Truncate((MinutesAddedDisplay / 60f), 0))).ToString() + "H : " + Truncate(((float)MinutesAddedDisplay - ((long)(Truncate((MinutesAddedDisplay / 60f), 0))) * 60), 0).ToString() + "M : " + TruncateFS(seconds, 0) + "S");
                }
                else if (Mathf.Abs(MinutesAdded) >= 1)
                {
                    TMP.text = (Truncate(((float)MinutesAddedDisplay - ((long)(Truncate((MinutesAddedDisplay / 60f), 0))) * 60), 0).ToString() + "M : " + (TruncateFS(seconds, 0) + "S"));
                }
                else if (Mathf.Abs(MinutesAdded) < 1)
                {
                    TMP.text = (TruncateFS(seconds, 0) + "S");
                }
            }
        }
        else
        {
            TMP.text = (Truncate(((MinutesAddedDisplay / 60f) % 60), 0).ToString() + "H : " + Truncate((MinutesAddedDisplay - (Truncate(((MinutesAddedDisplay / 60f) % 60), 0)) * 60), 0).ToString() + "M : " + (TruncateFS(seconds, 0) + "S"));
        }*/
    }
}
