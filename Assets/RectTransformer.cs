using UnityEngine;

public class RectTransformer : MonoBehaviour
{
    public RectTransform RT;

    // Update is called once per frame
    void Update()
    {
        RT.localScale = Vector3.one;
    }
}
