using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class DefaultTimeSetter : MonoBehaviour
{
    public RectTransform RT;
    public TimeControllerScript TCS;
    public TMP_InputField TMP;
    public float value = 0;
    public float horizontal = 1;
    public float scale = 1;
    void Start()
    {
        TMP.text = TCS.DefaultTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        RT.anchoredPosition = new Vector3((-Screen.width) + Screen.width * horizontal, (-Screen.height) + Screen.height * value, 0);
        RT.localScale = new Vector3(Screen.height / 481.6f, Screen.height / 481.6f, Screen.height / 481.6f) * scale;
        float Ratio = (float)Screen.width / (float)Screen.height;
        if (Ratio > 0.5 && Ratio <= 1)
        {
            //Debug.Log("Looks Wrong");
            Debug.Log(Ratio);
            float fr = 0.4f / Ratio;
            float hoz = horizontal - 1;
            float ASPRAT = (float)Screen.width / (float)Screen.height;
            float IASPRAT = 1080f / 2408f;
            //float TOPOW = ASPRAT / 2f;
            float TOPOW = ASPRAT / 2f;
            TOPOW = Mathf.Pow(TOPOW, 1f);
            TOPOW = -TOPOW;
            float IDASPRAT = Mathf.Pow(ASPRAT, IASPRAT);
            IDASPRAT = Mathf.Pow(IDASPRAT, TOPOW);
            RT.anchoredPosition = new Vector3((float)(Screen.width * hoz * fr * 1 * IDASPRAT), (-Screen.height) + Screen.height * value, 0);
        }
        if (Screen.width > Screen.height)
        {
            Debug.Log("re");
            float fr = 0.35f;
            float hoz = horizontal - 1;
            float ASPRAT = (float)Screen.width / (float)Screen.height;
            float IASPRAT = 1080f / 2408f;
            //float TOPOW = ASPRAT / 2f;
            float TOPOW = ASPRAT / 2f;
            TOPOW = Mathf.Pow(TOPOW, 1f);
            TOPOW = -TOPOW;
            float IDASPRAT = Mathf.Pow(ASPRAT, IASPRAT);
            IDASPRAT = Mathf.Pow(IDASPRAT, TOPOW);
            RT.anchoredPosition = new Vector3((float)(Screen.width * hoz * fr * 1 * IDASPRAT), (-Screen.height) + Screen.height * value, 0);
        }

    }
    public void ChangeDefaultTime()
    {
        try {
            float HRegEx = 0;
            float MRegEx = 0;
            float SRegEx = 0;
            bool Multiple = false;
            string filteredspaces = Regex.Replace(TMP.text, @"\s+", "");
            string[] Times = filteredspaces.Split(':');
            var match = Regex.Match(filteredspaces, @"(?i)(?:(\d+)h)?(?:(\d+)m)?(?:(\d+)s)?");
            float.TryParse(match.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out HRegEx);
            float.TryParse(match.Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out MRegEx);
            float.TryParse(match.Groups[3].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out SRegEx);
            if (Times.Length > 1)
                Multiple = true;
            if (Multiple)
            {
                TCS.DefaultTime = (float.Parse(Times[0]) * 60f) + float.Parse(Times[1]) + float.Parse(Times[2]) / 60f;
            }
            else if (match.Length > 0)
            {
                TCS.DefaultTime = HRegEx * 60f + MRegEx + SRegEx / 60f;

            }
            else if (!Multiple)
            {
                TCS.DefaultTime = float.Parse(TMP.text, CultureInfo.InvariantCulture.NumberFormat);

            }
        }
        catch { TMP.text = "INVALID"; }
    }
    public void UpdateDefaultTime()
    {
        try
        {
            float HRegEx = 0;
            float MRegEx = 0;
            float SRegEx = 0;
            bool Multiple = false;
            string filteredspaces = Regex.Replace(TMP.text, @"\s+", "");
            string[] Times = filteredspaces.Split(':');
            var match = Regex.Match(filteredspaces, @"(?i)(?:(\d+)h)?(?:(\d+)m)?(?:(\d+)s)?");
            float.TryParse(match.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out HRegEx);
            float.TryParse(match.Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out MRegEx);
            float.TryParse(match.Groups[3].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out SRegEx);
            if (Times.Length > 1)
                Multiple = true;
            if (Multiple)
            {
                TCS.DefaultTime = (float.Parse(Times[0]) * 60f) + float.Parse(Times[1]) + float.Parse(Times[2]) / 60f;
            }
            else if (match.Length > 0)
            {
                TCS.DefaultTime = HRegEx * 60f + MRegEx + SRegEx / 60f;

            }
            else if (!Multiple)
            {
                TCS.DefaultTime = float.Parse(TMP.text, CultureInfo.InvariantCulture.NumberFormat);

            }
            TCS.TotalTime = TCS.DefaultTime;
            TCS.primesave = true;
        }
        catch { TMP.text = "INVALID"; }
    }
    public int CheckTimeState()
    {
        int CurrentTime = 1;
        try
        {
            float HRegEx = 0;
            float MRegEx = 0;
            float SRegEx = 0;
            bool Multiple = false;
            string filteredspaces = Regex.Replace(TMP.text, @"\s+", "");
            string[] Times = filteredspaces.Split(':');
            var match = Regex.Match(filteredspaces, @"(?i)(?:(\d+)h)?(?:(\d+)m)?(?:(\d+)s)?");
            float.TryParse(match.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out HRegEx);
            float.TryParse(match.Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out MRegEx);
            float.TryParse(match.Groups[3].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out SRegEx);
            if (Times.Length > 1)
                Multiple = true;
            if (Multiple)
            {
                float Parsed = (float.Parse(Times[0]) * 60f) + float.Parse(Times[1]) + float.Parse(Times[2]) / 60f;
                if (TildeEqual(Parsed, TCS.TotalTime)) { CurrentTime = 0; }
            }
            else if (match.Length > 0)
            {
                float Parsed = HRegEx * 60f + MRegEx + SRegEx / 60f;
                if (TildeEqual(Parsed, TCS.TotalTime)) { CurrentTime = 0; }
            }
            else if (!Multiple)
            {
                float Parsed = float.Parse(TMP.text, CultureInfo.InvariantCulture.NumberFormat);
                if (TildeEqual(Parsed, TCS.TotalTime)) { CurrentTime = 0; }
            }
        }
        catch { TMP.text = "INVALID"; CurrentTime = -1; }
        //0 = Same
        //1 = Different
        //-1 = Invalid
        return CurrentTime;
    }
    public bool TildeEqual(float f1, float f2)
    {
        return TildeEqual(f1, f2,0.05f);
    }
    public bool TildeEqual(float f1, float f2, float percent)
    {

        float GPerf1 = f1 + (f1 * percent);
        float LPerf1 = f1 - (f1 * percent);

        if ((f2 < GPerf1 && f2 > LPerf1) || f1 == f2) {  return true; }

        return false;
    }
}
