using UnityEngine;

public class GenericNormalTransform : MonoBehaviour
{
    public Transform TF;
    public float value = 0;
    public float scale = 1;
    void Update()
    {

        TF.localPosition = new Vector3(0, (-Screen.height) + Screen.height * value, 0);
        TF.localScale = new Vector3(Screen.height / 481.6f, Screen.height / 481.6f, Screen.height / 481.6f) * scale;
    }
}
