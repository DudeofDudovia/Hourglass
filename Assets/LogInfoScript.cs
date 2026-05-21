using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using TMPro;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif
public class LogInfoScript : MonoBehaviour
{
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
#if PLATFORM_STANDALONE_WIN
        StringBuilder sb = new StringBuilder(80);

        GetLocaleInfoEx(
            null,
            LOCALE_STIMEFORMAT,
            sb,
            sb.Capacity
            );
        pattern = sb.ToString();
#endif
            TMP.text = DateTime.FromBinary(DisplayTicks).ToString("t", CultureInfo.CurrentCulture);
        if (pattern.Contains("H"))
        {
            TMP.text = System.DateTime.FromBinary(DisplayTicks).ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
        }
        else {
            TMP.text = System.DateTime.FromBinary(DisplayTicks).ToString();
        }
        //TMP.text = System.DateTime.FromBinary(DisplayTicks).ToLocalTime().ToString();
        if (HideDelay < 0)
        {
            if (MouseDown != Input.GetMouseButton(0))
            {
                HideLog();
            }
        }
        
        MouseDown = false;
        if (Input.GetMouseButton(0)) { MouseDown = true; }

        float HeightPercent = 0;
        HeightPercent = MouseY/Screen.height;
        BSAP.value = HeightPercent + .5f;

        if (Menu.activeSelf) {   HideLog();  }
    }
    private void FixedUpdate()
    {
        HideDelay -= 1;
    }
    public void HideLog()
    {
        InfoDisplay.SetActive(false);
    }
    public void ShowLog()
    {
        InfoDisplay.SetActive(true);
        TMP.text = System.DateTime.FromBinary(DisplayTicks).ToString();
        TMP.text = System.DateTime.FromBinary(DisplayTicks).ToShortDateString();

        MouseY = Input.mousePosition.y;
        HideDelay = 20;
    }
    public void ShowLog(long L)
    {
        DisplayTicks = L;
        ShowLog();
    }
}
