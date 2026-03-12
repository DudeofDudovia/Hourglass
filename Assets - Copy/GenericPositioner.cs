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
    }
}
