using UnityEngine;
using UnityEngine.UI;

public class UpdateTotalTimePreviewAnimScript : MonoBehaviour
{
    public RectTransform RT;
    public ChangeTotalTimeScript CTTS;
    public Slider CTTSlider;
    public int animframes = 0;
    public int frame = 0;
    public float animscalefactor = 0;

    public float value = 0;
    public float horizontal = 1;
    public float scale = 1;
    public bool FirstTick = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!FirstTick)
        {
            frame = 0;
            FirstTick = true;
        }
        animframes = (int)CTTSlider.value;
        RT.localRotation = Quaternion.Euler(0, 0, 360 * animscalefactor);
        frame--;
        if (frame >= 0)
        {
            animscalefactor = (float)(frame) / (float)animframes;
        }


    }
    public void PreviewAnimation()
    {
        animframes = (int)CTTSlider.value;
        frame = animframes;
        CTTS.animframes = animframes;
    }
}
