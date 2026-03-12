using System.Security.Cryptography;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TimerRunningScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI TMP;
    public TimeControllerScript TCS;
    public bool Milliseconds = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Milliseconds = TCS.MSTimer;
        if (Milliseconds)
        {
            if (!TCS.RunTimer)
            {
                TMP.text = "";
            }
            else if (TCS.RunningTime >= 60)
            {
                //TMP.text = (((int)(TCS.RunningTime/60f))).ToString() + " H";
                float seconds = TCS.RunningTime - TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                seconds -= 3600 * TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0);
                TMP.text = (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0).ToString() + "H : " + TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0).ToString() + "M : " + (TCS.TruncateForSeconds(seconds, 2) + "S"));
            }
            else if (TCS.RunningTime > 1)
            {
                float seconds = TCS.RunningTime - TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                TMP.text = (TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0).ToString() + "M : " + TCS.TruncateForSeconds(seconds, 2) + "S");
            }
            else if (TCS.RunningTime > 0 && TCS.RunningTime < 1)
            {
                float seconds = TCS.RunningTime - TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                TMP.text = (TCS.TruncateForSeconds(seconds, 2) + "S");
            }
            else if (TCS.RunningTime == 0)
            {
                TMP.text = "";
            }
            else
            {
                float seconds = TCS.RunningTime - TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                TMP.text = (TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0).ToString() + "M : " + TCS.TruncateForSeconds(seconds, 2) + "S");
            }
            if (TCS.pendingtimerreset)
            {
                TMP.text = "Cancel Timer?";
            }
        }
        else
        {
            if (!TCS.RunTimer)
            {
                TMP.text = "";
            }
            else if (TCS.RunningTime >= 60)
            {
                float seconds = TCS.RunningTime - TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                seconds -= 3600 * TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0);
                TMP.text = (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0).ToString() + "H : " + TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0).ToString() + "M : " + (TCS.TruncateForSecondsNM(seconds, 0) + "S"));
            }
            else if (TCS.RunningTime > 1)
            {
                float seconds = TCS.RunningTime - TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                TMP.text = (TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0).ToString() + "M : " + TCS.TruncateForSecondsNM(seconds, 0) + "S");
            }
            else if (TCS.RunningTime > 0 && TCS.RunningTime < 1)
            {
                float seconds = TCS.RunningTime - TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                TMP.text = (TCS.TruncateForSecondsNM(seconds, 0) + "S");
            }
            else if (TCS.RunningTime == 0)
            {
                TMP.text = "";
            }
            else
            {
                float seconds = TCS.RunningTime - TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                TMP.text = (TCS.Truncate((TCS.RunningTime - (TCS.Truncate(((TCS.RunningTime / 60f) % 60), 0)) * 60), 0).ToString() + "M : " + TCS.TruncateForSecondsNM(seconds, 0) + "S");
            }
            if (TCS.pendingtimerreset)
            {
                TMP.text = "Cancel Timer?";
            }
        }
    }
}
