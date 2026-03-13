using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleEvent : UnityEvent<bool> { }
public class OptionToggleScript : MonoBehaviour
{
    public Toggle Togg;
    public TimeControllerScript TCS;
    public int ObjectLayer = 2;
    public int DataLayer = 0;
    public UnityEvent<bool> VarToToggle;
    public void SaveMSToggle()
    {
        VarToToggle.Invoke(Togg.isOn);
        int tog = 0;
        if (Togg.isOn) { tog = 1; }
        /*PlayerPrefs.SetInt("MillisecondsTimerToggle", tog);
        PlayerPrefs.Save();*/
        TCS.MKfile(ObjectLayer,DataLayer,tog.ToString());
    }
    public void Awake()
    {
        try {
            int tog = int.Parse(TCS.RDfile(ObjectLayer,DataLayer));
            if (tog == 1) { Togg.isOn = true; }
            if (tog == 0) { Togg.isOn = false; }
        }
        catch { Togg.isOn = true; }
        VarToToggle.Invoke(Togg.isOn);
        /*
        if (PlayerPrefs.HasKey("MillisecondsTimerToggle"))
        {
            int tog = PlayerPrefs.GetInt("MillisecondsTimerToggle");
            if (tog == 1) { Togg.isOn = true; }
            if (tog == 0) { Togg.isOn = false; }

        }
        else { Togg.isOn = true; }
        VarToToggle.Invoke(Togg.isOn);*/
    }
}
