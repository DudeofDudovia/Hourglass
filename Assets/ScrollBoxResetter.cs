using UnityEngine;
using UnityEngine.UIElements;

public class ScrollBoxResetter : MonoBehaviour
{
    public RectTransform RT;
    public Vector3 SetPos;
    public Vector2 SizeD;
    
    void Start()
    {
        //RT.position = new Vector3(RT.position.x,SetPos.y);
    }
    private void Update()
    {
//RT.sizeDelta = new Vector2(SizeD.x,RT.sizeDelta.y);
    }
}