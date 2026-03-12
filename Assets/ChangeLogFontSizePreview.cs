using UnityEngine;
using UnityEngine.UI;

public class ChangeLogFontSizePreview : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TMP;
    public Slider CLFSS;
    public void Update()
    {
        TMP.fontSize = CLFSS.value*1.6f;
        TMP.text = Application.version.ToString();
    }
}
