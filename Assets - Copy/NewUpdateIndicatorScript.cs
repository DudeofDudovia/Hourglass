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
    public void Awake()
    {
        Version = Application.version;
         if (PlayerPrefs.HasKey("Version"))
        {
            if (PlayerPrefs.GetString("Version") != Version) { }
            if (PlayerPrefs.GetString("Version") == Version) { gameObject.SetActive(false); }
        }
        else { gameObject.SetActive(false); }
        
    }
    public void HideIndicator()
    {
        gameObject.SetActive(false);
        PlayerPrefs.SetString("Version", Version);
        PlayerPrefs.Save();
    }
    public void Update()
    {
        float AppScale = Scale;
        SineGenerator++;
        float Sine = Mathf.Sin(SineGenerator/DivideSineSpeed);

        AppScale += Sine /DivideSine;
        RT.transform.localScale = new Vector3(AppScale*TotalScale, AppScale * TotalScale, AppScale * TotalScale);
        RT.anchoredPosition = new Vector3((-Screen.width) + Screen.width * horizontal, (-Screen.height) + Screen.height * value, 0);
    
}
}
