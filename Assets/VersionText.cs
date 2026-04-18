using TMPro;
using UnityEngine;

public class VersionText : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    // Update is called once per frame
    void Update()
    {
        TMP.text = Application.version;
    }
}
 