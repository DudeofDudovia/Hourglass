using System;
using System.Globalization;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TimeAdder: MonoBehaviour
{

    public RectTransform RT;
    public TimeControllerScript TCS;
    public TMP_InputField TMP;
    public float value = 0;
    public GameObject StoreValues;
    public GameObject ViewportContent;
    public bool KPDown = false;
    public bool RTDown = false;
    void Update()
    {
        RT.anchoredPosition = new Vector3(0, (-Screen.height) + Screen.height * value, 0);
        RT.localScale = new Vector3(Screen.height / 481.6f, Screen.height / 481.6f, Screen.height / 481.6f) * 1.3f;
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && !KPDown) { AppenedTime(); KPDown = true; }
        if (Input.GetKeyDown(KeyCode.Return) && !RTDown) { AppenedTime(); RTDown = true; }
        if (Input.GetKeyUp(KeyCode.KeypadEnter)) { KPDown = false; }
        if (Input.GetKeyUp(KeyCode.Return)) { RTDown = false; }

    }
    public void AppenedTime()
    {
        try
        {
            float HRegEx = 0;
            float MRegEx = 0;
            float SRegEx = 0;
            bool Multiple = false;
            string filteredspaces = Regex.Replace(TMP.text, @"\s+", "");
            string[] Times = filteredspaces.Split(':');
            //var match = Regex.Match(filteredspaces, @"(?i)(?:(\d+)h)?(?:(\d+)m)?(?:(\d+)s)?");
            //var match = Regex.Match(filteredspaces, @"(?i)(?:(\d *\.?\d +)h)?(?: (\d *\.?\d +)m)?(?: (\d *\.?\d +)s)?");
            var match = Regex.Match(filteredspaces, @"(?i)(?:(\d*\.?\d+)h)?(?:(\d*\.?\d+)m)?(?:(\d*\.?\d+)s)?");
            //var match = Regex.Match(([+-] ? (?=\.\d |\d)(?:\d +)?(?:\.?\d *))(?: [Ee]([+-] ?\d +)) ?\dh\dH\dM\dm[0 - 9] + S[0 - 9] + s);
            float.TryParse(match.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out HRegEx);
            float.TryParse(match.Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out MRegEx);
            float.TryParse(match.Groups[3].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out SRegEx);
            if (Times.Length > 1)
                                Multiple = true;
            if (Multiple)
            {
                for (int i = 0; i < Times.Length; i++)
                {

                    if (i == 0)
                    {
                        TCS.UsedTime += float.Parse(Times[i]) * 60f;
                    }
                    if (i == 1)
                    {
                        TCS.UsedTime += float.Parse(Times[i]);
                    }
                    if (i == 2)
                    {
                        TCS.UsedTime += float.Parse(Times[i]) / 60f;
                    }
                }
                Instantiate(StoreValues, new Vector3((float.Parse(Times[0]) * 60f) + float.Parse(Times[1]) + float.Parse(Times[2]) / 60f + 0.0001f, transform.position.y, transform.position.z), transform.rotation, ViewportContent.transform);
            }
            else if (match.Length > 0)
            {
                TCS.UsedTime += HRegEx * 60f;
                TCS.UsedTime += MRegEx;
                TCS.UsedTime += SRegEx / 60f;
                Instantiate(StoreValues, new Vector3(HRegEx * 60f + MRegEx + SRegEx / 60f + 0.0001f, transform.position.y, transform.position.z), transform.rotation, ViewportContent.transform);

            }
            else if (!Multiple)
            {
                TCS.UsedTime += float.Parse(TMP.text, CultureInfo.InvariantCulture.NumberFormat);
                Instantiate(StoreValues, new Vector3(float.Parse(TMP.text, CultureInfo.InvariantCulture.NumberFormat), transform.position.y, transform.position.z), transform.rotation, ViewportContent.transform);
                
            }
            if (!TCS.KeepTimeInAddBox)
            {
                TMP.text = "";
            }
        }
        catch { TMP.text = "INVALID"; }

    }
}
