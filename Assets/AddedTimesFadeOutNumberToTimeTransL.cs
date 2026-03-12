using UnityEngine;
using UnityEngine.UI;

public class AddedTimesFadeOutNumberToTimeTransL : MonoBehaviour
{
    public TimeControllerScript TCS;
    public TMPro.TextMeshProUGUI TMP;
    public Slider ATFOS;
    void Update()
    {
        if (TCS.MSAddeds)
        {
            if ((ATFOS.value/60f) >= 60)
            {
                //TMP.text = (((int)((ATFOS.value/60f)/60f))).ToString() + " H";
                float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                seconds -= 3600 * TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0);
                TMP.text = (((TCS.Truncate((((ATFOS.value/60f) / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate(((ATFOS.value/60f) - (long)(TCS.Truncate((((ATFOS.value/60f) / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateForSeconds(seconds, 0) + "S"));
            }
            else if ((ATFOS.value/60f) > 1)
            {
                float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                // seconds -= 3600;
                TMP.text = (((float)TCS.Truncate(((ATFOS.value/60f) - (long)(TCS.Truncate((((ATFOS.value/60f) / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateForSeconds(seconds, 0) + "S");
            }
            else if ((ATFOS.value/60f) > 0 && (ATFOS.value/60f) < 1)
            {
                float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                //seconds -= 3600;
                TMP.text = (TCS.TruncateForSeconds(seconds, 0) + "S");
            }
            else if ((ATFOS.value/60f) == 0)
            {
                TMP.text = "";
            }
            else
            {
                if ((ATFOS.value/60f) <= -60)
                {
                    //TMP.text = (((int)((ATFOS.value/60f)/60f))).ToString() + " H";
                    float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    seconds -= 3600 * TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0);
                    TMP.text = (((TCS.Truncate((((ATFOS.value/60f) / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate(((ATFOS.value/60f) - (long)(TCS.Truncate((((ATFOS.value/60f) / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateForSeconds(seconds, 0) + "S"));
                }
                else if ((ATFOS.value/60f) < -1)
                {
                    float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    // seconds -= 3600;
                    TMP.text = (((float)TCS.Truncate(((ATFOS.value/60f) - (long)(TCS.Truncate((((ATFOS.value/60f) / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateForSeconds(seconds, 0) + "S");
                }
                else if ((ATFOS.value/60f) < 0 && (ATFOS.value/60f) > -1)
                {
                    float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    //seconds -= 3600;
                    TMP.text = (TCS.TruncateForSeconds(seconds, 0) + "S");
                }
            }
        }
        else
        {
            if ((ATFOS.value/60f) >= 60)
            {
                //TMP.text = (((int)((ATFOS.value/60f)/60f))).ToString() + " H";
                float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                seconds -= 3600 * TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0);
                TMP.text = (((TCS.Truncate((((ATFOS.value/60f) / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate(((ATFOS.value/60f) - (long)(TCS.Truncate((((ATFOS.value/60f) / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateFS(seconds, 0) + "S"));
            }
            else if ((ATFOS.value/60f) > 1)
            {
                float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                // seconds -= 3600;
                TMP.text = (((float)TCS.Truncate(((ATFOS.value/60f) - (long)(TCS.Truncate((((ATFOS.value/60f) / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateFS(seconds, 0) + "S");
            }
            else if ((ATFOS.value/60f) > 0 && (ATFOS.value/60f) < 1)
            {
                float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                //seconds -= 3600;
                TMP.text = (TCS.TruncateFS(seconds, 0) + "S");
            }
            else if ((ATFOS.value/60f) == 0)
            {
                TMP.text = "";
            }
            else
            {
                /*
                float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                //seconds -= 3600;
                TMP.text = (TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0).ToString() + "M : " + TCS.Truncate(seconds, 0) + "S");*/
                if ((ATFOS.value/60f) <= -60)
                {
                    //TMP.text = (((int)((ATFOS.value/60f)/60f))).ToString() + " H";
                    float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    seconds -= 3600 * TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0);
                    TMP.text = (((TCS.Truncate((((ATFOS.value/60f) / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate(((ATFOS.value/60f) - (long)(TCS.Truncate((((ATFOS.value/60f) / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateFS(seconds, 0) + "S"));
                }
                else if ((ATFOS.value/60f) < -1)
                {
                    float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    // seconds -= 3600;
                    TMP.text = (((float)TCS.Truncate(((ATFOS.value/60f) - (long)(TCS.Truncate((((ATFOS.value/60f) / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateFS(seconds, 0) + "S");
                }
                else if ((ATFOS.value/60f) < 0 && (ATFOS.value/60f) > -1)
                {
                    float seconds = (ATFOS.value/60f) - TCS.Truncate(((ATFOS.value/60f) - (TCS.Truncate((((ATFOS.value/60f) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    //seconds -= 3600;
                    TMP.text = (TCS.TruncateFS(seconds, 0) + "S");
                }
            }
        }
    }
}
