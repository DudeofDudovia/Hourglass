using UnityEngine;
using UnityEngine.UI;

public class MillisecondsAddedToggleScript : MonoBehaviour
{
    public Toggle MSToggle;
    public TimeControllerScript TCS;
    public void SaveMSToggle()
    {
        TCS.MSAddeds = MSToggle.isOn;
        int MStog = 0;
        if (MSToggle.isOn) { MStog = 1; }
        PlayerPrefs.SetInt("MillisecondsAddedToggle", MStog);
        PlayerPrefs.Save();
    }
    public void Awake()
    {
        if (PlayerPrefs.HasKey("MillisecondsAddedToggle"))
        {
            int Mstog = PlayerPrefs.GetInt("MillisecondsAddedToggle");
            if (Mstog == 1) { MSToggle.isOn = true; }
            if (Mstog == 0) { MSToggle.isOn = false; }
            
        }
        else { MSToggle.isOn = false; }
        TCS.MSAddeds = MSToggle.isOn;
    }
}
