using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RichFormatToggling : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_InputField tmp;
    public TMP_InputField tmp3;
    public Toggle togg;
    public TextMeshProUGUI tmp2;
    public bool wastogg;
    void Awake()
    {
        if (PlayerPrefs.GetInt("AdvancedFormatting", 0) == 1) { togg.isOn = true; }
    }

    // Update is called once per frame
    void Update()
    {
        if (togg.isOn)
        {
            tmp.contentType = TMP_InputField.ContentType.Standard;
            tmp3.contentType = TMP_InputField.ContentType.Standard;
            tmp2.text = "Add Time: x:y:z / xHyMzS";
            tmp2.fontSize = 10.5f;
            //togg.targetGraphic.color = new Color(.5f, .5f, .5f);
        }
        else
          {
            tmp.contentType = TMP_InputField.ContentType.DecimalNumber;
            tmp3.contentType = TMP_InputField.ContentType.DecimalNumber;
            tmp2.text = "Add Time (Minutes)";
            tmp2.fontSize = 14f;
            togg.targetGraphic.color = new Color(1, 1, 1);
        }
        if (togg.isOn != wastogg)
        {
            SaveTogg();
        }
        wastogg = togg.isOn;
    }
    public void SaveTogg()
    {
        int ADVFRMT = 0;
        if (togg.isOn) { ADVFRMT = 1; }
        PlayerPrefs.SetInt("AdvancedFormatting", ADVFRMT);
        PlayerPrefs.Save();
    }
}
