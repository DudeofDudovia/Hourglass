using TMPro;
using UnityEngine;

public class LogInfoScript : MonoBehaviour
{
    public GameObject InfoDisplay;
    public TextMeshProUGUI TMP;
    public long DisplayTicks;
    public int HideDelay;
    public float MouseY;
    public ButtonSizeAndPositioner BSAP;
    public bool MouseDown;
    public GameObject Menu;
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
        
        MouseDown = false;
        if (Input.GetMouseButton(0)) { MouseDown = true; }

        float HeightPercent = 0;
        HeightPercent = MouseY/Screen.height;
        BSAP.value = HeightPercent + .5f;

        if (Menu.activeSelf) {   HideLog();  }
    }
    private void FixedUpdate()
    {
        HideDelay -= 1;
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
        HideDelay = 20;
    }
    public void ShowLog(long L)
    {
        DisplayTicks = L;
        ShowLog();
    }
}
