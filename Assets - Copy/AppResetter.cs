using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppResetter : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public TimeControllerScript TCS;
    public Image IMG;

    void Start()
    {
        
    }
    void Update()
    {
        if (TCS.pendingappreset)
        {
            IMG.color = Color.red;
        }
        else
        {
            IMG.color = Color.white;
        }
        if (TCS.pendingappreset)
        {
            TMP.text = "Are you sure?";
            TMP.color = Color.white;
        }
        else
        {
            TMP.text = "Reset App";
            TMP.color = Color.red;
        }
    }
}
