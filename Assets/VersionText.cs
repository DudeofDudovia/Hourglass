using TMPro;
using UnityEngine;

public class VersionText : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    void Update()
    {
        TMP.text = Application.version;
    }
}
 