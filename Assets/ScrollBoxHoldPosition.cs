using UnityEngine;

public class ScrollBoxHoldPosition : MonoBehaviour
{
    public float ScrollPos;
    public float HeightOffset;
    public float SizeD;
    public RectTransform RT;
    public TimeControllerScript TCS;
    public bool FixScrollPos;
    public bool ReturnScrollPos;
    public int ReturnScrollPosTimer;
    public void Awake()
    {

        if (TCS == null) { TCS = Object.FindFirstObjectByType<TimeControllerScript>().gameObject.GetComponent<TimeControllerScript>(); }
        if (RT == null) { RT = gameObject.GetComponent<RectTransform>(); }
        int Prof = int.Parse(TCS.RDfile(0, 0, true));
    }
    // Update is called once per frame
    public void PrepReturn()
    {
        ScrollPos = RT.anchoredPosition.y;

        SizeD = RT.sizeDelta.y;
        ReturnScrollPos = true;
        ReturnScrollPosTimer = 1;
        
    }
    void Update()
    {
        if (ReturnScrollPosTimer >= 0)
        {
            ReturnScrollPos = false;
            if (ScrollPos > SizeD - HeightOffset)
            {
                RT.anchoredPosition = new Vector3(RT.anchoredPosition.x, SizeD - HeightOffset, 0);
            }
            else
            {
                RT.anchoredPosition = new Vector3(RT.anchoredPosition.x, ScrollPos, 0);
            }
        }
        ReturnScrollPosTimer -= 1;
    }
}
