using UnityEngine;
using UnityEngine.UI;

public class UpOrDownPreviewAnimScript : MonoBehaviour
{
    public RectTransform RT;
    public int animframes = 0;
    public int frame = 0;
    public float animscalefactor = 0;
    public bool WasLast;

    public float value = 0;
    public float horizontal = 1;
    public float scale = 1;
    public Slider UpDownSlider;
    public bool Direction;
    public bool FirstTick = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!FirstTick)
        {
            frame = 0;
            FirstTick = true;
        }
        animframes = (int)UpDownSlider.value;
       /* if (Direction)
        {
            RT.localScale = new Vector3(1, -1 + animscalefactor , 1) * scale;
        }
        else
        {
            RT.localScale = new Vector3(1, 1 - animscalefactor, 1) * scale;
        }
        */
        if (frame >= 0 && frame != animframes)
        {
            if (Direction)
            {
                RT.localScale = new Vector3(1, 1 - animscalefactor, 1) * scale;
            }
            else
            {
                RT.localScale = new Vector3(1, -1 + animscalefactor, 1) * scale;

            }
        }
        else if (frame >= 0)
        {
            if (Direction)
            {
                RT.localScale = new Vector3(1, -1 + animscalefactor, 1) * scale;
            }
            else
            {
                RT.localScale = new Vector3(1, 1 - animscalefactor, 1) * scale;

            }
        }





        if (Direction != WasLast)
        {
            frame = animframes;
        }

        frame--;
        if (frame >= 0 && animframes != 0)
        {
            animscalefactor = (float)(animframes - frame) / (float)animframes;
            animscalefactor *= 2 * 1;
        }
        if (animframes == 0)
        {
            animscalefactor = 0;
        }

        WasLast = Direction;
    }
    public void PreviewAnim()
    {
            Direction = !Direction;
    }
}
