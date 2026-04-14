using UnityEngine;

public class GenericPositioner : MonoBehaviour
{
    public RectTransform RT;
    public float value = 0;
    public float horizontal = 1;
    public float scale = 1;
    void Update()
    {
        RT.anchoredPosition = new Vector3((-Screen.width) + Screen.width * horizontal, (-Screen.height) + Screen.height * value, 0);
        RT.localScale = new Vector3(Screen.height / 481.6f, Screen.height / 481.6f, Screen.height / 481.6f) * scale;
        if (Screen.width > Screen.height)
        {
            float fr = 0.35f;
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
    }
}
