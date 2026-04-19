using UnityEngine;

public class TimesAdded : MonoBehaviour
{
    public int TimesAppeneded = 0;
    public RectTransform RT;
    void Update()
    {
        RT.sizeDelta = new Vector2(RT.sizeDelta.x,10 + (TimesAppeneded * 33));
    }
}
