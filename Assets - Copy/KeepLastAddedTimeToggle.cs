using UnityEngine;

public class KeepLastAddedToggle : MonoBehaviour
{
    public TimeControllerScript TCS;
    public void KeepLastAddedTime()
    {
        TCS.KeepTimeInAddBox = !TCS.KeepTimeInAddBox;
        int KLA = 0;
        if (TCS.KeepTimeInAddBox) { KLA = 1; }
        PlayerPrefs.SetInt("TCS.KeepTimeInAddBoxTime", KLA);
        PlayerPrefs.Save();
    }
    void Awake()
    {
        if (PlayerPrefs.HasKey("TCS.KeepTimeInAddBoxTime"))
        {
            int KLAT = PlayerPrefs.GetInt("TCS.KeepTimeInAddBoxTime");
            if (KLAT == 1) { TCS.KeepTimeInAddBox = true; }
            if (KLAT == 0) { TCS.KeepTimeInAddBox = false; }
        }
        else { TCS.KeepTimeInAddBox = false; }
    }
}
