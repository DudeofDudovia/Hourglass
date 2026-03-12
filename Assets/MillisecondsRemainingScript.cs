using UnityEngine;
using UnityEngine.UI;

public class MillisecondsRemainingScript : MonoBehaviour
{
    public Toggle MSToggle;
    public TimeControllerScript TCS;
    public void SaveMSToggle()
    {
        TCS.MSLeftDisplay = MSToggle.isOn;
        int MStog = 0;
        if (MSToggle.isOn) { MStog = 1; }
        PlayerPrefs.SetInt("MillisecondsRemainingToggle", MStog);
        PlayerPrefs.Save();
    }
    public void Awake()
    {
        if (PlayerPrefs.HasKey("MillisecondsRemainingToggle"))
        {
            int Mstog = PlayerPrefs.GetInt("MillisecondsRemainingToggle");
            if (Mstog == 1) { MSToggle.isOn = true; }
            if (Mstog == 0) { MSToggle.isOn = false; }

        }
        else { MSToggle.isOn = false; }
        TCS.MSLeftDisplay = MSToggle.isOn;
    }
}
