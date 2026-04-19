using UnityEngine;

public class ChangeTotalTimeScript : MonoBehaviour
{
    public RectTransform RT;
    public DefaultTimeSetter DTS;
    public int animframes = 0;
    public int frame = 0;
    public float animscalefactor = 0;

    public float value = 0;
    public float horizontal = 1;
    public float scale = 1;
    public bool Wrong = false;
    public bool Stay = false;
    void FixedUpdate()
    {
        RT.localRotation = Quaternion.Euler(0,0, 360 * animscalefactor);
        frame--;
        if (Wrong && frame >= 0)
        {
            animscalefactor = (float)(frame) / -(float)animframes;
        }
        else if (Stay)
        {
            animscalefactor = 0;
        }
        else if (frame >= 0)
        {
            animscalefactor = (float)(frame) / (float)animframes;
        }
        

    }
    public void ChangeTotalTime() 
    {
        Wrong = false;
        Stay = false;
        if (DTS.CheckTimeState() == -1) { Wrong = true; }
        if (DTS.CheckTimeState() == 0) { Stay = true; }
        frame = animframes;
    }
}
