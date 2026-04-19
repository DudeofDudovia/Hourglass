using TMPro;
using UnityEngine;

public class ProfileAdder : MonoBehaviour
{
    public TMP_InputField TMP;
    public void AddProfile()
    {
        GameObject[] TimeMarkers = GameObject.FindGameObjectsWithTag("TimeController");
        TimeMarkers[0].GetComponent<TimeControllerScript>().AddProfile(TMP.text);
        TMP.text= "";
    }
}
