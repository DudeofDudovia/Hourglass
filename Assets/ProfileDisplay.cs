using TMPro;
using UnityEngine;

public class ProfileDisplay : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public TimeControllerScript TCS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TMP.text = TCS.GetComponent<TimeControllerScript>().ProfileName;
    }
}
