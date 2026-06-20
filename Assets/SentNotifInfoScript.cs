using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SentNotifInfoScript : MonoBehaviour
{

    public TextMeshProUGUI TMPName;
    public TextMeshProUGUI TMPType;
    public TextMeshProUGUI TMPText;


    public GameObject InfoDisplay;
    public TextMeshProUGUI TMP;
    public long DisplayTicks;
    public int HideDelay;
    public float MouseY;
    public ButtonSizeAndPositioner BSAP;
    public bool MouseDown;
    public GameObject Menu;



#if PLATFORM_STANDALONE_WIN
    [DllImport("kernel32.dll")]
    static extern int GetLocaleInfoEx(
    String lpLocaleName,
    uint LCType,
    StringBuilder lpLCData,
    int cchData);
    const uint LOCALE_STIMEFORMAT = 0x00001003;
#endif
    void Update()
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

       // TMP.text = DateTime.FromBinary(DisplayTicks).ToString("t", CultureInfo.CurrentCulture);
        if (pattern.Contains("H"))
        {
          //  TMP.text = System.DateTime.FromBinary(DisplayTicks).ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
        }
        else
        {
           // TMP.text = System.DateTime.FromBinary(DisplayTicks).ToString();
        }
        //TMP.text = System.DateTime.FromBinary(DisplayTicks).ToLocalTime().ToString();
        if (HideDelay < 0)
        {
            if (MouseDown != Input.GetMouseButton(0) && !ClickedBox(GetEventSystemRaycastResults()))
            {
                Debug.Log("That'sWrong!");
                HideInfo();
            }
        }

        MouseDown = false;
        if (Input.GetMouseButton(0)) { MouseDown = true; }

        float HeightPercent = 0;
        HeightPercent = MouseY / Screen.height;
        BSAP.value = HeightPercent + .5f;

        if (!Menu.activeSelf) { HideInfo(); }
    }
    public bool ClickedBox(List<RaycastResult> eventSystemRaycastResults)
    {
        for (int i = 0; i < eventSystemRaycastResults.Count; i++)
        {
            RaycastResult result = eventSystemRaycastResults[i];
            if (result.gameObject.tag == "ExplainerDisplay")
            {

                return true;
            }
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

    private void FixedUpdate()
    {
        HideDelay -= 1;
    }
    public void HideInfo()
    {
        Debug.Log("Culprint");
        InfoDisplay.SetActive(false);
    }
    public void ShowInfo()
    {
        InfoDisplay.SetActive(true);

        MouseY = Input.mousePosition.y;
        Debug.Log("1/1/1/");
        HideDelay = 20;
    }
    public void ShowInfo(long L, string Name, string Text, string Type)
    {
        Debug.Log("1WHY!?");
        DisplayTicks = L;
        TMPName.text = Name;
        TMPText.text = Text;
        TMPType.text = Type;
        ShowInfo();
        Debug.Log("WHY!?");
    }
}
