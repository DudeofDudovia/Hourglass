using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class AppQuitter : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public Image IMG;
    public bool pendingquit = false;
    public float quitresettime = 0.4167f;
    void Update()
    {
        if (pendingquit)
        {
            quitresettime -= Time.deltaTime / 5;
            if (quitresettime <= 0) { pendingquit = false; }
        }
        if (pendingquit)
        {
            IMG.color = Color.red;
        }
        else
        {
            IMG.color = Color.white;
        }
        if (pendingquit)
        {
            TMP.text = "Are you sure?";
            TMP.color = Color.white;
        }
        else
        {
            TMP.text = "Exit App";
            TMP.color = Color.red;
        }
    }
    public void QuitApp()
    {
        if (pendingquit)
        {
            Application.Quit();
        }
        else
        {
            pendingquit = true;
            quitresettime = 0.4167f;
        }
    }
}
