using UnityEngine;

public class GenericPositioner : MonoBehaviour
{
    public RectTransform RT;
    public float value = 0;
    public float horizontal = 1;
    public float scale = 1;
    public float x = 0.3f;
    void Update()
    {
        RT.anchoredPosition = new Vector3((-Screen.width) + Screen.width * horizontal, (-Screen.height) + Screen.height * value, 0);
        RT.localScale = new Vector3(Screen.height / 481.6f, Screen.height / 481.6f, Screen.height / 481.6f) * scale;
        float Ratio = (float)Screen.width / (float)Screen.height;
        if (Ratio > 0.5 && Ratio <= 1)
        {
            float fr = 0.44f / Ratio;
            float hoz = horizontal - 1;
            float ASPRAT = (float)Screen.width / (float)Screen.height;
            float IASPRAT = 1080f / 2408f;
            float TOPOW = ASPRAT / 2f;
            TOPOW = Mathf.Pow(TOPOW, 1f);
            TOPOW = -TOPOW;
            float IDASPRAT = Mathf.Pow(ASPRAT, IASPRAT);
            IDASPRAT = Mathf.Pow(IDASPRAT, TOPOW);
            RT.anchoredPosition = new Vector3((float)(Screen.width * hoz * fr * 1 * IDASPRAT), (-Screen.height) + Screen.height * value, 0);
        }
        if (Screen.width > Screen.height)
        {
            float hoz = horizontal - 1;
            float ASPRAT = (float)Screen.width / (float)Screen.height;
            float fr = 0.35f * Mathf.Pow((1.777777777777778f / ASPRAT),.45f);
            float IASPRAT = 1080f / 2408f;
            float TOPOW = ASPRAT / 2f;
            TOPOW = Mathf.Pow(TOPOW, 1f);
            TOPOW = -TOPOW;
            float IDASPRAT = Mathf.Pow(ASPRAT, IASPRAT);
            IDASPRAT = Mathf.Pow(IDASPRAT, TOPOW);
            RT.anchoredPosition = new Vector3((float)(Screen.width * hoz * fr * 1 * IDASPRAT), (-Screen.height) + Screen.height * value, 0);
        }
    }
}
