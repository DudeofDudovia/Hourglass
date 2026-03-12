using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TimesAdded : MonoBehaviour
{
    public int TimesAppeneded = 0;
    public RectTransform RT;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RT.sizeDelta = new Vector2(RT.sizeDelta.x,10 + (TimesAppeneded * 33));
    }
}
