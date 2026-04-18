using TMPro;
using UnityEditor;
using UnityEngine;
public class TimeRemainingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI TMP;
    public TimeControllerScript TCS;
    void Update()
    {
        if (!TCS.CountUp)
        {
            if (TCS.MSUsedDisplay)
            {
                if (TCS.RemainingTime >= 60)
                {
                    float seconds = TCS.RemainingTime - TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    seconds -= 3600 * TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0);
                    TMP.text = (((TCS.Truncate(((TCS.RemainingTime / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate((TCS.RemainingTime - (long)(TCS.Truncate(((TCS.RemainingTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateForSeconds(seconds, 0) + "S"));
                }
                else if (TCS.RemainingTime >= 1)
                {
                    float seconds = TCS.RemainingTime - TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    TMP.text = (((float)TCS.Truncate((TCS.RemainingTime - (long)(TCS.Truncate(((TCS.RemainingTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateForSeconds(seconds, 0) + "S");
                }
                else if (TCS.RemainingTime >= 0 && TCS.RemainingTime < 1)
                {
                    float seconds = TCS.RemainingTime;
                    seconds *= 60;
                    TMP.text = (TCS.TruncateForSeconds(seconds, 0) + "S");
                }
                else
                {
                    if (TCS.RemainingTime <= -60)
                    {
                        float seconds = TCS.RemainingTime - TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0);
                        seconds *= 60;
                        seconds -= 3600 * TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0);
                        TMP.text = (((TCS.Truncate(((TCS.RemainingTime / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate((TCS.RemainingTime - (long)(TCS.Truncate(((TCS.RemainingTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateForSeconds(seconds, 0) + "S"));
                    }
                    else if (TCS.RemainingTime <= -1)
                    {
                        float seconds = TCS.RemainingTime - TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0);
                        seconds *= 60;
                        TMP.text = (((float)TCS.Truncate((TCS.RemainingTime - (long)(TCS.Truncate(((TCS.RemainingTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateForSeconds(seconds, 0) + "S");
                    }
                    else if (TCS.RemainingTime <= 0 && TCS.RemainingTime > -1)
                    {
                        float seconds = TCS.RemainingTime - TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0);
                        seconds *= 60;
                        TMP.text = (TCS.TruncateForSeconds(seconds, 0) + "S");
                        
                    }
                }
            }
           /* else if (TCS.RemainingTime == 0)
            {
                TMP.text = "00";
            }*/
            else
            {
                if (TCS.RemainingTime >= 60)
                {
                    float seconds = TCS.RemainingTime - TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    seconds -= 3600 * TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0);
                    TMP.text = (((TCS.Truncate(((TCS.RemainingTime / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate((TCS.RemainingTime - (long)(TCS.Truncate(((TCS.RemainingTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateFS(seconds, 0) + "S"));
                }
                else if (TCS.RemainingTime >= 1)
                {
                    float seconds = TCS.RemainingTime - TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    TMP.text = (((float)TCS.Truncate((TCS.RemainingTime - (long)(TCS.Truncate(((TCS.RemainingTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateFS(seconds, 0) + "S");
                }
                else if (TCS.RemainingTime >= 0 && TCS.RemainingTime < 1)
                {
                    float seconds = TCS.RemainingTime;
                    seconds *= 60;
                    TMP.text = (TCS.TruncateFS(seconds, 0) + "S");
                    //float seconds = TCS.RemainingTime - TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0);
                    //Debug.Log(TCS.RemainingTime);
                    //Debug.Log(TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0));

                }
                else
                {
                    if (TCS.RemainingTime <= -60)
                    {
                        float seconds = TCS.RemainingTime - TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0);
                        seconds *= 60;
                        seconds -= 3600 * TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0);
                        TMP.text = (((TCS.Truncate(((TCS.RemainingTime / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate((TCS.RemainingTime - (long)(TCS.Truncate(((TCS.RemainingTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateFS(seconds, 0) + "S"));
                    }
                    else if (TCS.RemainingTime <= -1)
                    {
                        float seconds = TCS.RemainingTime - TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0);
                        seconds *= 60;
                        TMP.text = (((float)TCS.Truncate((TCS.RemainingTime - (long)(TCS.Truncate(((TCS.RemainingTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateFS(seconds, 0) + "S");
                    }
                    else if (TCS.RemainingTime <= 0 && TCS.RemainingTime > -1)
                    {
                        float seconds = TCS.RemainingTime - TCS.Truncate((TCS.RemainingTime - (TCS.Truncate(((TCS.RemainingTime / 60f) % 60), 0)) * 60), 0);
                        seconds *= 60;
                        TMP.text = (TCS.TruncateFS(seconds, 0) + "S");
                    }
                }
            }
            if (TCS.RemainingTime <= 0) { TMP.color = Color.red; }
            else { TMP.color = Color.black; }
            if (TCS.pendingreset)
            {
                TMP.text = "Are you sure?";
                TMP.color = Color.white;
            }
        }
        else
        {
            if (TCS.MSLeftDisplay)
            {
                if (TCS.UsedTime >= 60)
                {
                    float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    seconds -= 3600 * TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0);
                    TMP.text = (((TCS.Truncate(((TCS.UsedTime / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate((TCS.UsedTime - (long)(TCS.Truncate(((TCS.UsedTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateForSeconds(seconds, 0) + "S"));
                }
                else if (TCS.UsedTime >= 1)
                {
                    float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    // seconds -= 3600;
                    TMP.text = (((float)TCS.Truncate((TCS.UsedTime - (long)(TCS.Truncate(((TCS.UsedTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateForSeconds(seconds, 0) + "S");
                }
                else if (TCS.UsedTime >= 0 && TCS.UsedTime < 1)
                {
                    //float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                    float seconds = TCS.UsedTime;
                    seconds *= 60;
                    //seconds -= 3600;
                    TMP.text = (TCS.TruncateForSeconds(seconds, 0) + "S");
                }
                else if (TCS.UsedTime == 0)
                {
                    TMP.text = "";
                }
                else
                {
                    if (TCS.UsedTime <= -60)
                    {
                        //TMP.text = (((int)(TCS.UsedTime/60f))).ToString() + " H";
                        float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                        seconds *= 60;
                        seconds -= 3600 * TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0);
                        TMP.text = (((TCS.Truncate(((TCS.UsedTime / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate((TCS.UsedTime - (long)(TCS.Truncate(((TCS.UsedTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateForSeconds(seconds, 0) + "S"));
                    }
                    else if (TCS.UsedTime <= -1)
                    {
                        float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                        seconds *= 60;
                        TMP.text = (((float)TCS.Truncate((TCS.UsedTime - (long)(TCS.Truncate(((TCS.UsedTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateForSeconds(seconds, 0) + "S");
                    }
                    else if (TCS.UsedTime <= 0 && TCS.UsedTime > -1)
                    {
                        //float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                        float seconds = TCS.UsedTime;
                        seconds *= 60;
                        TMP.text = (TCS.TruncateForSeconds(seconds, 0) + "S");
                    }
                }
            }
            else
            {
                if (TCS.UsedTime >= 60)
                {
                    float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    seconds -= 3600 * TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0);
                    TMP.text = (((TCS.Truncate(((TCS.UsedTime / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate((TCS.UsedTime - (long)(TCS.Truncate(((TCS.UsedTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateFS(seconds, 0) + "S"));
                }
                else if (TCS.UsedTime >= 1)
                {
                    float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                    seconds *= 60;
                    TMP.text = (((float)TCS.Truncate((TCS.UsedTime - (long)(TCS.Truncate(((TCS.UsedTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateFS(seconds, 0) + "S");
                }
                else if (TCS.UsedTime >= 0 && TCS.UsedTime < 1)
                {
                    //float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                    float seconds = TCS.UsedTime;
                    seconds *= 60;
                    TMP.text = (TCS.TruncateFS(seconds, 0) + "S");
                }
                else if (TCS.UsedTime == 0)
                {
                    TMP.text = "";
                }
                else
                {
                    if (TCS.UsedTime <= -60)
                    {
                        float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                        seconds *= 60;
                        seconds -= 3600 * TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0);
                        TMP.text = (((TCS.Truncate(((TCS.UsedTime / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate((TCS.UsedTime - (long)(TCS.Truncate(((TCS.UsedTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.TruncateFS(seconds, 0) + "S"));
                    }
                    else if (TCS.UsedTime <= -1)
                    {
                        float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                        seconds *= 60;
                        TMP.text = (((float)TCS.Truncate((TCS.UsedTime - (long)(TCS.Truncate(((TCS.UsedTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + TCS.TruncateFS(seconds, 0) + "S");
                    }
                    else if (TCS.UsedTime <= 0 && TCS.UsedTime > -1)
                    {
                        //float seconds = TCS.UsedTime - TCS.Truncate((TCS.UsedTime - (TCS.Truncate(((TCS.UsedTime / 60f) % 60), 0)) * 60), 0);
                        float seconds = TCS.UsedTime;
                        seconds *= 60;
                        TMP.text = (TCS.TruncateFS(seconds, 0) + "S");
                    }
                }
            }
        }
        if (TCS.RemainingTime <= 0) { TMP.color = Color.red; }
        else { TMP.color = Color.black; }
        if (TCS.pendingreset)
        {
            TMP.text = "Are you sure?";
            TMP.color = Color.white;
        }
    }
}
//