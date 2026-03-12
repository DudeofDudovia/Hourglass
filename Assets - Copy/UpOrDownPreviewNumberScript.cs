using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UpOrDownPreviewNumberScript : MonoBehaviour
{

    public TMP_InputField TMP;
    public Slider UDPNS;
    public UpDownScript UDS;
    public void SetSlider()
    {
        if (int.Parse(TMP.text) > 150) { UDPNS.maxValue = int.Parse(TMP.text); }
        else { UDPNS.maxValue = 150; }
        UDPNS.value = int.Parse(TMP.text);
        UDS.animframes = (int)UDPNS.value;
    }
    public void UpdateSlider()
    {
        try
        {
            TMP.text = ((int)UDPNS.value).ToString();
            if (int.Parse(TMP.text) > 150) { UDPNS.maxValue = int.Parse(TMP.text); }
            else { UDPNS.maxValue = 150; }
            TMP.text = ((int)UDPNS.value).ToString();
            UDS.animframes = (int)UDPNS.value;
        }
        catch { }
    }
}
