using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTotalTimePreviewNumberScript : MonoBehaviour
{
    public TMP_InputField TMP;
    public Slider UTTS;
    public ChangeTotalTimeScript CTTS;
    public RectTransform RT;
    public void SetSlider()
    {
        if (int.Parse(TMP.text) > 150) { UTTS.maxValue = int.Parse(TMP.text); }
        else { UTTS.maxValue = 150; }
        UTTS.value = int.Parse(TMP.text);
        CTTS.animframes = (int)UTTS.value;
        //RT.sizeDelta = new Vector2(6, RT.sizeDelta.y);
    }
    public void UpdateSlider()
    {
        try
        {
            TMP.text = ((int)UTTS.value).ToString();
            if (int.Parse(TMP.text) > 150) { UTTS.maxValue = int.Parse(TMP.text); }
            else { UTTS.maxValue = 150; }
            TMP.text = ((int)UTTS.value).ToString();
            CTTS.animframes = (int)UTTS.value;
        }
        catch { }
    }
}
