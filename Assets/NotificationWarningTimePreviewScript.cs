using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationWarningTimePreviewScript : MonoBehaviour
{
    public TMP_InputField TMP;
    public Slider NWTPS;
    public void SetSlider()
    {
        try
        {
            if (int.Parse(TMP.text) > NWTPS.maxValue) { NWTPS.maxValue = int.Parse(TMP.text); }
            else { NWTPS.maxValue = 1800; }
            NWTPS.value = int.Parse(TMP.text);
        }
        catch { }
    }
    public void UpdateSlider()
    {
        try
        {
            if (NWTPS.maxValue < 1800) { NWTPS.maxValue = 1800; }
            TMP.text = ((int)NWTPS.value).ToString();
            if (int.Parse(TMP.text) > NWTPS.maxValue) { NWTPS.maxValue = int.Parse(TMP.text); }
            else { NWTPS.maxValue = 1800; }
            TMP.text = ((int)NWTPS.value).ToString();
        }
        catch { }
    }
    public void Start()
    {
        UpdateSlider();
    }
}
 