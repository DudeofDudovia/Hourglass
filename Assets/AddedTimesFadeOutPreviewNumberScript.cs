using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AddedTimesFadeOutPreviewNumberScript : MonoBehaviour
{
    public TMP_InputField TMP;
    public Slider ATFOS;
    public TimeControllerScript TCS;
    public void SetSlider()
    {
        try
        {
            if (int.Parse(TMP.text) > 150) { ATFOS.maxValue = int.Parse(TMP.text); }
            else { ATFOS.maxValue = 150; }
            ATFOS.value = int.Parse(TMP.text);
            TCS.DeletedProfLifeTime = (int)ATFOS.value;
        }
        catch { }
    }
    public void UpdateSlider()
    {
        try
        {
            TMP.text = ((int)ATFOS.value).ToString();
            if (int.Parse(TMP.text) > 150) { ATFOS.maxValue = int.Parse(TMP.text); }
            else { ATFOS.maxValue = 150; }
            TMP.text = ((int)ATFOS.value).ToString();
            TCS.DeletedProfLifeTime = (int)ATFOS.value;
        }
        catch { }
    }
}
