using UnityEngine;
public class BackgroundScaler : MonoBehaviour
{
    public RectTransform RT;



    void Update()
    {
        RT.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
