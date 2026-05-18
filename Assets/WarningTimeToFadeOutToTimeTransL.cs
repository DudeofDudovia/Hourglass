using UnityEngine;
using UnityEngine.UI;

public class WarningTimeToFadeOutToTimeTransL : MonoBehaviour
{
    public TimeControllerScript TCS;
    public TMPro.TextMeshProUGUI TMP;
    public Slider ATFOS;
    void Update()
    {
        float Val = ATFOS.value;
        if (TCS.MSAddeds)
        {
            if ((Val) >= 60)
            {
                float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                seconds -= 3600 * TCS.Truncate((((Val) / 60f) % 60), 0);
                TMP.text = (((TCS.Truncate((((Val) / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate(((Val) - (long)(TCS.Truncate((((Val) / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateForSeconds(seconds, 0) + "S"));
            }
            else if ((Val) > 1)
            {
                float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                TMP.text = (((float)TCS.Truncate(((Val) - (long)(TCS.Truncate((((Val) / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateForSeconds(seconds, 0) + "S");
            }
            else if ((Val) > 0 && (Val) < 1)
            {
                float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                TMP.text = (TCS.TruncateForSeconds(seconds, 0) + "S");
            }
            else if ((Val) == 0)
            {
                TMP.text = "";
            }
            else
            {
                if ((Val) <= -60)
                {
                    float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    seconds -= 3600 * TCS.Truncate((((Val) / 60f) % 60), 0);
                    TMP.text = (((TCS.Truncate((((Val) / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate(((Val) - (long)(TCS.Truncate((((Val) / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateForSeconds(seconds, 0) + "S"));
                }
                else if ((Val) < -1)
                {
                    float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    TMP.text = (((float)TCS.Truncate(((Val) - (long)(TCS.Truncate((((Val) / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateForSeconds(seconds, 0) + "S");
                }
                else if ((Val) < 0 && (Val) > -1)
                {
                    float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    TMP.text = (TCS.TruncateForSeconds(seconds, 0) + "S");
                }
            }
        }
        else
        {
            if ((Val) >= 60)
            {
                float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                seconds -= 3600 * TCS.Truncate((((Val) / 60f) % 60), 0);
                TMP.text = (((TCS.Truncate((((Val) / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate(((Val) - (long)(TCS.Truncate((((Val) / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateFS(seconds, 0) + "S"));
            }
            else if ((Val) > 1)
            {
                float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                TMP.text = (((float)TCS.Truncate(((Val) - (long)(TCS.Truncate((((Val) / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateFS(seconds, 0) + "S");
            }
            else if ((Val) > 0 && (Val) < 1)
            {
                float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                seconds *= 60;
                TMP.text = (TCS.TruncateFS(seconds, 0) + "S");
            }
            else if ((Val) == 0)
            {
                TMP.text = "";
            }
            else
            {
                if ((Val) <= -60)
                {
                    float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    seconds -= 3600 * TCS.Truncate((((Val) / 60f) % 60), 0);
                    TMP.text = (((TCS.Truncate((((Val) / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate(((Val) - (long)(TCS.Truncate((((Val) / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateFS(seconds, 0) + "S"));
                }
                else if ((Val) < -1)
                {
                    float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    TMP.text = (((float)TCS.Truncate(((Val) - (long)(TCS.Truncate((((Val) / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateFS(seconds, 0) + "S");
                }
                else if ((Val) < 0 && (Val) > -1)
                {
                    float seconds = (Val) - TCS.Truncate(((Val) - (TCS.Truncate((((Val) / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    TMP.text = (TCS.TruncateFS(seconds, 0) + "S");
                }
            }
        }
        if (ATFOS.value == 0) { TMP.text = "00S"; }
        if (ATFOS.value == 0 && TCS.MSAddeds) { TMP.text = "00.00S"; }
    }
}
