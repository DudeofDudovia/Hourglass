using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StopStartTimerScript : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public TimeControllerScript TCS;
    public Button button;
    void Start()
    {
        
    }
    void Update()
    {
        if  (TCS.GetComponent<TimeControllerScript>().RunTimer) { TMP.text = " Stop Timer"; button.targetGraphic.color = Color.red; }
        if  (!TCS.GetComponent<TimeControllerScript>().RunTimer) { TMP.text = " Start Timer"; button.targetGraphic.color = Color.blue; }

    }
}
