using UnityEngine;

public class UpDownScript : MonoBehaviour
{
    public TimeControllerScript TCS;
    public RectTransform RT;
    public int animframes = 0;
    public int frame = 0;
    public float animscalefactor = 0;
    public float lastanimscalefactor = 0;
    public bool AnimFinished;
    public bool WasLast;

    public float value = 0;
    public float horizontal = 1;
    public float scale = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(frame);
        Debug.Log(animscalefactor);


        RT.anchoredPosition = new Vector3((-Screen.width) + Screen.width * horizontal, (-Screen.height) + Screen.height * value, 0);
        //if (animscalefactor == 0) { animscalefactor = 10; }
        if (frame == animframes) { Debug.Log("B"); }
        if (frame >= 0 && frame != animframes)
        {
            Debug.Log("A");
            if (TCS.CountUp)
            {
                RT.localScale = new Vector3(Screen.height / 481.6f, (Screen.height / 481.6f) - animscalefactor, Screen.height / 481.6f) * scale;
                //RT.localScale = new Vector3(Screen.height / 481.6f, -(Screen.height / 481.6f) + animscalefactor, Screen.height / 481.6f) * scale;
            }
            else
            {
                RT.localScale = new Vector3(Screen.height / 481.6f, -(Screen.height / 481.6f) + animscalefactor, Screen.height / 481.6f) * scale;
                //RT.localScale = new Vector3(Screen.height / 481.6f, Screen.height / 481.6f - animscalefactor, Screen.height / 481.6f) * scale;

            }
        }
        else if (frame >= 0 ) {
            Debug.Log("B2");
            if (TCS.CountUp)
            {
                RT.localScale = new Vector3(Screen.height / 481.6f, (Screen.height / 481.6f) - lastanimscalefactor, Screen.height / 481.6f) * scale;
                //RT.localScale = new Vector3(Screen.height / 481.6f, -(Screen.height / 481.6f) + animscalefactor, Screen.height / 481.6f) * scale;
            }
            else
            {
                RT.localScale = new Vector3(Screen.height / 481.6f, -(Screen.height / 481.6f) + lastanimscalefactor, Screen.height / 481.6f) * scale;
                //RT.localScale = new Vector3(Screen.height / 481.6f, Screen.height / 481.6f - animscalefactor, Screen.height / 481.6f) * scale;

            }
        }


        /*if (TCS.CountUp && animscalefactor == 0)
        {
            RT.localScale = new Vector3(Screen.height / 481.6f, -(Screen.height / 481.6f) + animscalefactor, Screen.height / 481.6f) * scale;
            Debug.Log("Should've");
        }
        else if (!TCS.CountUp && animscalefactor == 0)
        {
            RT.localScale = new Vector3(Screen.height / 481.6f, (Screen.height / 481.6f) - animscalefactor, Screen.height / 481.6f) * scale;
            Debug.Log("Should've3");
        }*/
        lastanimscalefactor = animscalefactor;
        if (frame >= 0)
        {
            animscalefactor = (float)(animframes - frame) / (float)animframes;
            animscalefactor *= 2 * (Screen.height / 481.6f);
        }
        frame--;
        if (TCS.CountUp != WasLast)
        {
            frame = animframes;

        }
        WasLast = TCS.CountUp;
    }
    
    
}
