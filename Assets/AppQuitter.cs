using System.Collections;
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
    public LoggingConsoleControllerScript LCCS;
    private void FixedUpdate()
    {
        if (pendingquit)
        {
            //quitresettime -= Time.deltaTime / 5;
            quitresettime -= 1;
            if (quitresettime <= 0) { pendingquit = false; }
            else
            {
                IMG.color = Color.red;
                TMP.text = "Are you sure?";
                TMP.color = Color.white;
            }
        }
        else
        {
            IMG.color = Color.white;
            TMP.text = "Exit App";
            TMP.color = Color.red;
        }
    }
    public void QuitApp()
    {
        if (pendingquit)
        {
            StartCoroutine(ExitApp());
            Application.Quit();
        }
        else
        {
            pendingquit = true;
            quitresettime = 100;
        }
    }
    IEnumerator ExitApp()
    {
        LCCS.WriteConsole();
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}
