using UnityEngine;
using UnityEngine.UI;

public class MillisecondsUsedScript : MonoBehaviour
{
    public Toggle MSToggle;
    public TimeControllerScript TCS;
    public void SaveMSToggle()
    {
            TCS.MSUsedDisplay = MSToggle.isOn;
            int MStog = 0;
            if (MSToggle.isOn) { MStog = 1; }
            PlayerPrefs.SetInt("MillisecondsUsedToggle", MStog);
            PlayerPrefs.Save();
    }
    public void Awake()
    {
        if (PlayerPrefs.HasKey("MillisecondsUsedToggle"))
        {
            int Mstog = PlayerPrefs.GetInt("MillisecondsUsedToggle");
            if (Mstog == 1) { MSToggle.isOn = true; }
            if (Mstog == 0) { MSToggle.isOn = false; }
        }
        else { MSToggle.isOn = false; }
        TCS.MSUsedDisplay = MSToggle.isOn;
    }

}
