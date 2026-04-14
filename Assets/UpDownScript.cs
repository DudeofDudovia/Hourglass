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

    public bool FirstFrame = true;

    // Update is called once per frame
    void Start()
    {
        frame = 0;
        animscalefactor = (float)(animframes - frame) / (float)animframes;
        animscalefactor *= 2 * (Screen.height / 481.6f);
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
    void FixedUpdate()
    {
        RT.anchoredPosition = new Vector3((-Screen.width) + Screen.width * horizontal, (-Screen.height) + Screen.height * value, 0);
        if (Screen.width > Screen.height)
        {
            float fr = 0.35f;
            float hoz = horizontal - 1;
            float ASPRAT = (float)Screen.width / (float)Screen.height;
            float IASPRAT = 1080f / 2408f;
            float TOPOW = ASPRAT / 2f;
            TOPOW = Mathf.Pow(TOPOW, 1f);
            TOPOW = -TOPOW;
            float IDASPRAT = Mathf.Pow(ASPRAT, IASPRAT);
            IDASPRAT = Mathf.Pow(IDASPRAT, TOPOW);
            RT.anchoredPosition = new Vector3((float)(Screen.width * hoz * fr * 1 * IDASPRAT), (-Screen.height) + Screen.height * value, 0);
        }
        //if (animscalefactor == 0) { animscalefactor = 10; }
        if (frame >= 0 && frame != animframes)
        {
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
            if (TCS.CountUp)
            {
                RT.localScale = new Vector3(Screen.height / 481.6f, -(Screen.height / 481.6f) + (2 * (Screen.height / 481.6f)), Screen.height / 481.6f) * scale;
                //RT.localScale = new Vector3(Screen.height / 481.6f, -(Screen.height / 481.6f) + animscalefactor, Screen.height / 481.6f) * scale;
            }
            else
            {
                RT.localScale = new Vector3(Screen.height / 481.6f, (Screen.height / 481.6f) - (2 * (Screen.height / 481.6f)), Screen.height / 481.6f) * scale;
                //RT.localScale = new Vector3(Screen.height / 481.6f, Screen.height / 481.6f - animscalefactor, Screen.height / 481.6f) * scale;

            }
        }
        lastanimscalefactor = animscalefactor;
        if (frame >= 0)
        {
            animscalefactor = (float)(animframes - frame) / (float)animframes;
            animscalefactor *= 2 * (Screen.height / 481.6f);
        }
        if (frame > 0) { frame--; }
        if (frame < 0) { frame = 0; }
        if (TCS.CountUp != WasLast)
        {
            frame = animframes;

        }
        WasLast = TCS.CountUp;
        if (FirstFrame) { frame = 0; FirstFrame = false; }
    }
    
    
}
