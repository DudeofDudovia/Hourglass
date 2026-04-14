using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddedTimesScript : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public float MinutesAdded;
    public float MinutesAddedDisplay;
    public int AddedId = 0;
    public bool BClicked1 = false;
    public bool BClicked2 = false;
    public bool MouseDown = false;
    public bool MouseD2 = false;
    public bool MouseUp = false;
    public bool WasMouseUp = false;
    public float initpos = 0;
    public float initposY = 0;
    public float diff = 0;
    public float exitdiff = 300;
    public bool Ishovering = false;
    public float OriginalPosX = 0;
    public float OriginalPosY = 0;
    public float diffY = 0;
    public float exitdiffY = 30;
    public Image ButtonToColorChange;
    public bool BeingDeleted;
    public int Life;
    public int LifeTime;
    public Canvas CanV;

    public bool WasRainbow;
    public Color NormalColor;
    public Color FadeoutColor;
    public void RainbowColors()
    {
        WasRainbow = true;
        NormalColor = Color.HSVToRGB(Random.Range(1, 100) / 100f, Random.Range(50, 100) / 100f, Random.Range(50, 100) / 100f);
        FadeoutColor = Color.HSVToRGB(Random.Range(1, 100) / 100f, Random.Range(50, 100) / 100f, Random.Range(50, 100) / 100f);
    }
    private void Start()
    {
        GameObject[] TCS = GameObject.FindGameObjectsWithTag("TimeController");
        if (TCS[0].GetComponent<TimeControllerScript>().AddedTimesRainbow) { RainbowColors();}
        else { 
        
            NormalColor = Color.HSVToRGB(TCS[0].GetComponent<TimeControllerScript>().AddedTimesHue, TCS[0].GetComponent<TimeControllerScript>().AddedTimesSat, TCS[0].GetComponent<TimeControllerScript>().AddedTimesVal);
            FadeoutColor = Color.HSVToRGB(TCS[0].GetComponent<TimeControllerScript>().AddedTimesFadeOutHue, TCS[0].GetComponent<TimeControllerScript>().AddedTimesFadeOutSat, TCS[0].GetComponent<TimeControllerScript>().AddedTimesFadeOutVal);
        }
        transform.parent.GetComponent<TimesAdded>().TimesAppeneded += 1;
        AddedId = transform.parent.GetComponent<TimesAdded>().TimesAppeneded;
        MinutesAdded = transform.position.x;
        transform.localPosition = new Vector3(0, AddedId * -40 + 10, 0);

        transform.localPosition = new Vector3(98, AddedId * -40 + 10, 0);
        float OGMinutesAdded = MinutesAdded;
        MinutesAddedDisplay = RoundFloat(MinutesAdded,5);
        //if (OGMinutesAdded > MinutesAdded)
    }
    public float Truncate(float number, int digits)
    {
        number *= Mathf.Pow(10, digits);
        number = (long)number;
        //number = (float)number;
        number /= Mathf.Pow(10, digits);
        return number;
    }
    public bool IsPointerOver(List<RaycastResult> eventSystemRaycastResults)
    {
        bool ReturnVal = false;
        for (int i = 0; i < eventSystemRaycastResults.Count; i++)
        {
            RaycastResult result = eventSystemRaycastResults[i];
            if (result.gameObject.layer == LayerMask.NameToLayer("UI")) { return false; }
            if (result.gameObject.layer == LayerMask.NameToLayer("AddedTime") && result.gameObject.transform.position == transform.position)
                ReturnVal = true;
            
        }
        return ReturnVal;
    }
    public float RoundFloat(float f, int digits)
    {
        f = f*Mathf.Pow(10, digits) + 5;
        long I = (long)f;
        f = (float)I;
        f /= Mathf.Pow(10, digits);
        return f;
    }
    public float RoundFloat(float f)
    {
        f = f * 1000 + 5;
        long I = (long)f;
        f = (float)I;
        f /= 1000;
        return f;
    }
    public string TruncateFS(float number, int digits)
    {
        string Numstring = number.ToString();
        number *= Mathf.Pow(10, digits);
        number = (long)number;
        //number = (float)number;
        number /= Mathf.Pow(10, digits);
        Numstring = number.ToString();
        if (number < 10 && number >= 0) { Numstring = "0" + Numstring; }
        return Numstring;
    }
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults;
    }
    void Update()
    {
        
        if (BeingDeleted)
        {
            return;
        }

        CanV.overrideSorting = false;
        transform.localPosition = new Vector3(92f, AddedId * -33 + 10, 0);

        float seconds = MinutesAddedDisplay - Truncate((MinutesAddedDisplay - (Truncate(((MinutesAddedDisplay / 60f) % 60), 0)) * 60), 0);
        seconds *= 60;
        seconds -= 3600 * Truncate(((MinutesAddedDisplay / 60f) % 60), 0);
        GameObject[] TCS = GameObject.FindGameObjectsWithTag("TimeController");
        if (!TCS[0].GetComponent<TimeControllerScript>().AddedTimesRainbow) {WasRainbow = false; }
        if (TCS[0].GetComponent<TimeControllerScript>().AddedTimesRainbow && !WasRainbow) {
            RainbowColors();
        }
        else if (!TCS[0].GetComponent<TimeControllerScript>().AddedTimesRainbow)
        {
            NormalColor = Color.HSVToRGB(TCS[0].GetComponent<TimeControllerScript>().AddedTimesHue, TCS[0].GetComponent<TimeControllerScript>().AddedTimesSat, TCS[0].GetComponent<TimeControllerScript>().AddedTimesVal);
            FadeoutColor = Color.HSVToRGB(TCS[0].GetComponent<TimeControllerScript>().AddedTimesFadeOutHue, TCS[0].GetComponent<TimeControllerScript>().AddedTimesFadeOutSat, TCS[0].GetComponent<TimeControllerScript>().AddedTimesFadeOutVal);
        }
        ButtonToColorChange.color = NormalColor;

        if (TCS[0].gameObject.GetComponent<TimeControllerScript>() != null)
        {
            if (TCS[0].gameObject.GetComponent<TimeControllerScript>().MSAddeds)
            {
                if (Mathf.Abs(MinutesAddedDisplay) >= 60)
                {
                    TMP.text = (((long)(Truncate((MinutesAddedDisplay / 60f), 0))).ToString() + "H : " + Truncate(((float)MinutesAddedDisplay - ((long)(Truncate((MinutesAddedDisplay / 60f), 0))) * 60), 0).ToString() + "M : " + TCS[0].gameObject.GetComponent<TimeControllerScript>().TruncateForSeconds(seconds, 2) + "S");
                }
                else if (Mathf.Abs(MinutesAdded) >= 1)
                {
                    TMP.text = (Truncate(((float)MinutesAddedDisplay - ((long)(Truncate((MinutesAddedDisplay / 60f), 0))) * 60), 0).ToString() + "M : " + TCS[0].gameObject.GetComponent<TimeControllerScript>().TruncateForSeconds(seconds, 2) + "S");
                }
                else if (Mathf.Abs(MinutesAdded) < 1)
                {
                    TMP.text = (TCS[0].gameObject.GetComponent<TimeControllerScript>().TruncateForSeconds(seconds, 2) + "S");
                }
            }
            else
            {
                if (Mathf.Abs(MinutesAdded) >= 60)
                {
                    //TMP.text = (((TCS.Truncate(((TCS.RemainingTime / 60f)), 0))).ToString() + "H : " + ((float)TCS.Truncate((TCS.RemainingTime - (int)(TCS.Truncate(((TCS.RemainingTime / 60f)), 0)) * 60), 0)).ToString() + "M : " + (TCS.Truncate(seconds, 0) + "S"));
                    TMP.text = (((long)(Truncate((MinutesAddedDisplay / 60f), 0))).ToString() + "H : " + Truncate(((float)MinutesAddedDisplay - ((long)(Truncate((MinutesAddedDisplay / 60f), 0))) * 60), 0).ToString() + "M : " + TruncateFS(seconds, 0) + "S");
                }
                else if (Mathf.Abs(MinutesAdded) >= 1)
                {
                    //TMP.text = (Truncate((MinutesAdded - (Truncate(((MinutesAdded / 60f) % 60), 0)) * 60), 0).ToString() + "M : " + TCS[0].gameObject.GetComponent<TimeControllerScript>().TruncateForSeconds(seconds, 2) + "S");
                    TMP.text = (Truncate(((float)MinutesAddedDisplay - ((long)(Truncate((MinutesAddedDisplay / 60f), 0))) * 60), 0).ToString() + "M : " + (TruncateFS(seconds, 0) + "S"));
                }
                else if (Mathf.Abs(MinutesAdded) < 1)
                {
                    //TMP.text = (TCS[0].gameObject.GetComponent<TimeControllerScript>().TruncateForSeconds(seconds, 2) + "S");
                    TMP.text = (TruncateFS(seconds, 0) + "S");
                }
            }
        }
        else
        {
            TMP.text = (Truncate(((MinutesAddedDisplay / 60f) % 60), 0).ToString() + "H : " + Truncate((MinutesAddedDisplay - (Truncate(((MinutesAddedDisplay / 60f) % 60), 0)) * 60), 0).ToString() + "M : " + (TruncateFS(seconds, 0) + "S"));
        }

        //TMP.text = MinutesAdded.ToString();

        MouseDown = false;
        //InputAction click = new InputAction(type: InputActionType.PassThrough, binding: "<Mouse>/leftButton");
        //InputSystem.
        if (Input.GetMouseButton(0)) { MouseDown = true; }
        if (MouseUp) { WasMouseUp = true; }
        if (BClicked1 && !BClicked2 && MouseUp)
        {
            WasMouseUp = true;
            OriginalPosX = transform.position.x;
            OriginalPosY = transform.position.y;
            initpos = Input.mousePosition.x;
            initposY = Input.mousePosition.y;
            //initpos = Mouse.current.position.x.ReadValue();
            BClicked2 = true;
        }
        if (BClicked1 && BClicked2 && WasMouseUp)
        {
           
            diff = Input.mousePosition.x - initpos;
            diffY = Input.mousePosition.y - initposY;
            transform.position += new Vector3(diff, 0, 0);
            CanV.overrideSorting = true;
            CanV.sortingOrder = 1;
            if (Mathf.Abs(diff) > exitdiff) { ButtonToColorChange.color = FadeoutColor; }
            if (Mathf.Abs(diffY) > exitdiffY) 
            {
                BClicked1 = false;
                BClicked2 = false;
            }
        }
        if (!BClicked1 && BClicked2)
        {
            if (Mathf.Abs(diff) > exitdiff) { RemoveTime(); return; }
            BClicked2 = false;
        }
        if (!BClicked2)
        {
            WasMouseUp = false;
        }
        //if (BtoClick.) { }
        if (!MouseDown) { BClicked1 = false; }
        Ishovering = IsPointerOver(GetEventSystemRaycastResults());
        if (MouseDown && Ishovering) { BClicked1 = true; }
        if (MouseDown) { MouseUp = false; }
        if (MouseD2 != MouseDown && Ishovering) { MouseUp = true; }
        MouseD2 = MouseDown;
    }
    public void RemoveTime()
    {
        gameObject.tag = "BeingRemoved";
        GameObject TCS = Object.FindFirstObjectByType<TimeControllerScript>().gameObject;
        TCS.GetComponent<TimeControllerScript>().RemoTime(MinutesAdded);
        
        Life = TCS.GetComponent<TimeControllerScript>().DeletedProfLifeTime;
        LifeTime = TCS.GetComponent<TimeControllerScript>().DeletedProfLifeTime;
        BeingDeleted = true;
        BeingRemoved();
    }
    private void FixedUpdate()
    {
        if (BeingDeleted)
        {
            BeingRemoved();
        }

    }
    public void BeingRemoved()
    {
        GameObject TCS = Object.FindFirstObjectByType<TimeControllerScript>().gameObject;
        if (Life < LifeTime) { gameObject.tag = "Untagged"; }
        Life--;
        CanV.overrideSorting = true;
        CanV.sortingOrder = 0;
        transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), Vector3.one, (float)Life/(float)LifeTime);
        ButtonToColorChange.color = Color.Lerp(new Color(NormalColor.r / 8, NormalColor.g / 8, NormalColor.b/8), FadeoutColor, (float)Life / (float)LifeTime);
        transform.localPosition = new Vector3(92f, AddedId * -33 + 10, 0);
        Color e = new Color(32341,1,1);
        transform.position += new Vector3(diff, 0, 0);
        if (Life < 0) {
            gameObject.SetActive(false); Destroy(gameObject);
        }

    }
}
