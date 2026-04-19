using UnityEngine;

public class RectTransformer : MonoBehaviour
{
    public RectTransform RT;
    void Update()
    {
        RT.localScale = Vector3.one;
    }
}
