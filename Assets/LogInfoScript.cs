using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class LogInfoScript : MonoBehaviour
{
    public GameObject InfoDisplay;
    public TextMeshProUGUI TMP;
    public long DisplayTicks;
    public int HideDelay;
    public float MouseY;
    void Start()
    {
        
    }

    public ButtonSizeAndPositioner BSAP;
    public bool MouseDown;
    void Update()
    {
        TMP.text = System.DateTime.FromBinary(DisplayTicks).ToString();
        if (HideDelay < 0)
        {
            
            if (MouseDown != Input.GetMouseButton(0))
            {
                HideLog();
            }
        }
        HideDelay -= 1;
        MouseDown = false;
        if (Input.GetMouseButton(0)) { MouseDown = true; }

        float HeightPercent = 0;
        HeightPercent = MouseY/Screen.height;
        BSAP.value = HeightPercent + .5f;
    }
    public void HideLog()
    {
        InfoDisplay.SetActive(false);
    }
    public void ShowLog()
    {
        InfoDisplay.SetActive(true);
        TMP.text = System.DateTime.FromBinary(DisplayTicks).ToString();
        MouseY = Input.mousePosition.y;
        HideDelay = 30;
    }
    public void ShowLog(long L)
    {
        DisplayTicks = L;
        ShowLog();
    }
}
