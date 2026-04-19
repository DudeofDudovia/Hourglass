using TMPro;
using UnityEngine;

public class ProfileDisplay : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public TimeControllerScript TCS;
    void Update()
    {
        TMP.text = TCS.GetComponent<TimeControllerScript>().ProfileName;
    }
}
