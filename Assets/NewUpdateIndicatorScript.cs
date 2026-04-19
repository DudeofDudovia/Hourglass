using UnityEngine;

public class NewUpdateIndicatorScript : MonoBehaviour
{
    public float TotalScale = 5f;
    public float Scale = 1;
    public RectTransform RT;
    public int SineGenerator;
    public float DivideSineSpeed = 8f;
    public float DivideSine = 1f;
    public float value = 0;
    public float horizontal = 1;
    public float scale = 1;
    public string Version = "0.6A";
    public TimeControllerScript TCS;
    public void Awake()
    {
        Version = Application.version;
        string LastVer = TCS.RDfile(0, 5, true);
        if (LastVer == "-792" || LastVer == "#" || LastVer == "0")
        {
            TCS.MKfile(0, 5, Version, true);
            gameObject.SetActive(false);
        }
        if (TCS.RDfile(0,5,true) != "-792")
        {
            if (TCS.RDfile(0, 5, true) == Version) { gameObject.SetActive(false); }
        }
        else { gameObject.SetActive(false); }
        
    }
    public void HideIndicator()
    {
        TCS.MKfile(0, 5, Version, true);
        gameObject.SetActive(false);
    }
    public void Update()
    {
        float AppScale = Scale;
        SineGenerator++;
        float Sine = Mathf.Sin(SineGenerator/DivideSineSpeed);

        AppScale += Sine /DivideSine;
        RT.localScale = new Vector3(Screen.height / 481.6f, Screen.height / 481.6f, Screen.height / 481.6f) * AppScale * TotalScale;
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

    }
}
