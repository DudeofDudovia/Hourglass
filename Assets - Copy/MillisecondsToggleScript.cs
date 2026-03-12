using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UI;
public class MillisecondsToggleScript : MonoBehaviour
{
    public Toggle MSToggle;
    public TimeControllerScript TCS;
    public void SaveMSToggle()
    {
        TCS.MSTimer = MSToggle.isOn;
        int MStog = 0;
        if (MSToggle.isOn) { MStog = 1; }
        PlayerPrefs.SetInt("MillisecondsTimerToggle", MStog);
        PlayerPrefs.Save();
    }
    public void Awake()
    {
        if (PlayerPrefs.HasKey("MillisecondsTimerToggle"))
        {
            int Mstog = PlayerPrefs.GetInt("MillisecondsTimerToggle");
            if (Mstog == 1) { MSToggle.isOn = true; }
            if (Mstog == 0) { MSToggle.isOn = false; }
      
        }
        else { MSToggle.isOn = true; }
        TCS.MSTimer = MSToggle.isOn;
    }
}
