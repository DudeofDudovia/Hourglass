using System;
using UnityEngine;
using UnityEngine.UI;

public class RainbowAddedTimesToggleScript : MonoBehaviour
{
    public TimeControllerScript TCS;
    public Toggle RainbowAddedTimesToggle;
    public int OldProfile;
    public void LastAddedTimesRainbow()
    {
        TCS.AddedTimesRainbow = RainbowAddedTimesToggle.isOn;
        int ATR = 0;
        if (TCS.AddedTimesRainbow) { ATR = 1; }
        PlayerPrefs.SetInt(TCS.Profile + "AddedTimesRainbow", ATR);
        PlayerPrefs.Save();
    }
    void Awake()
    {
        if (PlayerPrefs.HasKey(TCS.Profile + "AddedTimesRainbow"))
        {
            int ATR = PlayerPrefs.GetInt(TCS.Profile + "AddedTimesRainbow");
            if (ATR == 1) { TCS.AddedTimesRainbow = true; }
            if (ATR == 0) { TCS.AddedTimesRainbow = false; }
        }
        else { TCS.AddedTimesRainbow = false; }
        RainbowAddedTimesToggle.isOn = TCS.AddedTimesRainbow;
    }
    public void LoadProfile()
    {
        if (PlayerPrefs.HasKey(TCS.Profile + "AddedTimesRainbow"))
        {
            int ATR = PlayerPrefs.GetInt(TCS.Profile + "AddedTimesRainbow");
            if (ATR == 1) { TCS.AddedTimesRainbow = true; }
            if (ATR == 0) { TCS.AddedTimesRainbow = false; }
        }
        else { TCS.AddedTimesRainbow = false; }
        RainbowAddedTimesToggle.isOn = TCS.AddedTimesRainbow;
    }
    public void Start()
    {
        LoadProfile();
    }
    public void Update()
    {
        if (OldProfile != TCS.Profile)
        {
            LoadProfile();
        }
        OldProfile = TCS.Profile;
    }
}
