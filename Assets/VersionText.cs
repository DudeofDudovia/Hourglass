using TMPro;
using UnityEngine;

public class VersionText : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    void Awake()
    {
        TMP.text = Application.version;
    }
}
 