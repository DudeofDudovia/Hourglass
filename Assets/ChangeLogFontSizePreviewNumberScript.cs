using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLogFontSizePreviewNumberScript : MonoBehaviour
{
    public TMP_InputField TMP;
    public Slider CLFSS;
    public TextMeshProUGUI ExplainerTMP;
    public TextMeshProUGUI CLTMP;
    public void SetSlider()
    {
        if (float.Parse(TMP.text) > 18) { CLFSS.maxValue = float.Parse(TMP.text); }
        else { CLFSS.maxValue = 18; }
        CLFSS.value = float.Parse(TMP.text);
        CLTMP.fontSize = CLFSS.value;
        TMP.text = Truncate(float.Parse(TMP.text),2).ToString();
    }
    public void UpdateSlider()
    {
        try
        {
            TMP.text = ((float)CLFSS.value).ToString();
            TMP.text = Truncate(float.Parse(TMP.text), 2).ToString();
            CLTMP.fontSize = (float)CLFSS.value;
            ExplainerTMP.fontSize = (float)CLFSS.value;
        }
        catch { }
    }

    public float Truncate(float number, int digits)
    {
        number *= Mathf.Pow(10, digits);
        number = (long)number;
        number /= Mathf.Pow(10, digits);
        return number;
    }
    public void Start()
    {

    }

}
