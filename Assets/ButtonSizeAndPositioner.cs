using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class ButtonSizeAndPositioner : MonoBehaviour
{
    public RectTransform RT;
    public float value = 0;
    public float scale = 1f;
    public TimeControllerScript TCS;
    public Image IMG;
    public bool fortimer;
    public bool ForTimeUsed = false;
    void Start()
    {
     if (fortimer && !TCS.RunTimer) { scale = 0; }
     RT.localScale = new Vector3(Screen.height / 481.6f, Screen.height / 481.6f, Screen.height / 481.6f) * scale;
    }
    void Update()
    {
        RT.anchoredPosition = new Vector3(0, (-Screen.height) + Screen.height * value, 0);
        RT.localScale = new Vector3(Screen.height / 481.6f, Screen.height / 481.6f, Screen.height / 481.6f) * scale;
        if (!ForTimeUsed)
        {
            if (!fortimer)
            {
                if (TCS.pendingreset)
                {
                    IMG.color = Color.red;
                }
                else
                {
                    IMG.color = Color.white;
                }
            }
            if (fortimer)
            {
                if (TCS.pendingtimerreset)
                {
                    IMG.color = Color.olive;
                }
                else if (!TCS.pendingtimerreset)
                {
                    IMG.color = Color.aliceBlue;
                }
                if (!TCS.RunTimer)
                {
                    scale = 0;
                }
                else if (TCS.RunTimer)
                {
                    scale = .9f;
                }
            }
        }
    }
}
