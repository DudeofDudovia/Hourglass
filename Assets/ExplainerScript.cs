using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class ExplainerScript : MonoBehaviour
{
    public string TagName;
    public string TagType;
    [TextArea]
    public string TagText;
    public Color NameColor = Color.white;
    public Color TypeColor = Color.white;
    public Color TextColor = Color.white;
    public Color BackgroundColor = Color.gray3;

    public TextMeshProUGUI TMPName;
    public TextMeshProUGUI TMPType;
    public TextMeshProUGUI TMPText;

    public float MouseY;
    public ButtonSizeAndPositioner BSAP;
    public bool MouseDown;
    public int MouseClicked;
    public GameObject Menu;
    public GameObject Options;
    public bool OldMenuActive;
    public bool TCSEXPLAINOLD;

    public GameObject ExplainationDisplay;
    public Image Background;

    public int HideDelay;

    public TimeControllerScript TCS;

    public GameObject[] ButtonsToEnable = new GameObject[0];
    public GameObject[] SlidersToEnable = new GameObject[0];
    public GameObject[] TogglesToEnable = new GameObject[0];
    public GameObject[] TMPInputsToEnable = new GameObject[0];
    public int WaitToShow;
    public bool IsShowing;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && TCS.ExplainMode && !ClickedBox(GetEventSystemRaycastResults()))
        {
            MouseDown = true;
            MouseClicked = 2;
            if (!ClickedSame(GetEventSystemRaycastResults()))
            {
                //Debug.Log("???");
                SetTextsFromHighest(GetEventSystemRaycastResults());
            }
        }

        if (HideDelay < 0 & ExplainationDisplay.activeSelf)
        {
            if (MouseClicked == 1 && !Input.GetMouseButton(0))
            {
                //Debug.Log(NamesPointerIsOver(GetEventSystemRaycastResults()));
                if (!ClickedBox(GetEventSystemRaycastResults()))
                {
                        HideExplaination();
                    
                }
            }
        }

        WaitToShow -= 1;
        MouseClicked -= 1;
        MouseDown = false;

        float HeightPercent = 0;
        HeightPercent = MouseY / Screen.height;
        if (HeightPercent > 0.825) { HeightPercent = 0.825f; }
        if (HeightPercent < 0.17) { HeightPercent = 0.17f; }
        BSAP.value = HeightPercent + .5f;

        if (Menu.activeSelf != OldMenuActive) {
            
            HideExplaination(); }
        OldMenuActive = Menu.activeSelf;

        if (TCS.ExplainMode)
        {

            //EnableInteractables();
            //DisableInteractables_Old(GetEventSystemRaycastResults());
            //DisableInteractables(GetEventSystemRaycastResults());
            //FindInteractables();
            
        }
        else
        {
            HideExplaination();
        }
        if (TCS.ExplainMode != TCSEXPLAINOLD)
        {
            if (TCS.ExplainMode)
            {
                FindInteractables();
                DisableInteractables();
            }
            else
            {
                //Debug.Log("EnableT1");
                EnableInteractables();
            }
        }
        TCSEXPLAINOLD = TCS.ExplainMode;


    }
    private void FixedUpdate()
    {
        HideDelay -= 1;
    }
    public void HideExplaination()
    {

        ExplainationDisplay.SetActive(false);
        WaitToShow = 200;



        TagName = "";
        TagText = "";
        NameColor = Color.gray;
        TextColor = Color.gray;
        TypeColor = Color.gray;
        BackgroundColor = Color.gray;
        NameColor = Color.gray;
        TypeColor = Color.gray;
        TextColor = Color.gray;
        
    }
    public void ShowExplaination()
    {
        MouseY = Input.mousePosition.y;
        ExplainationDisplay.SetActive(true);
        TMPName.text = TagName;
        TMPType.text = TagType;
        TMPText.text = TagText;


        HideDelay = 20;
    }

    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults;
    }
    /*
    public string NamePointerIsOver(List<RaycastResult> eventSystemRaycastResults)
    {
        string ret = "Hm1.";
        for (int i = 0; i < eventSystemRaycastResults.Count; i++)
        {
            RaycastResult result = eventSystemRaycastResults[i];
            //if (result.gameObject.layer == LayerMask.NameToLayer("Resetable Slider") && result.gameObject.transform.position == transform.position)
            return ret;
        }
        return ret;
    }*/

    public string NamesPointerIsOver(List<RaycastResult> eventSystemRaycastResults)
    {
        string ret = "Hmm.";
        for (int i = 0; i < eventSystemRaycastResults.Count; i++)
        {
            RaycastResult result = eventSystemRaycastResults[i];
            //if (result.gameObject.layer == LayerMask.NameToLayer("Resetable Slider") && result.gameObject.transform.position == transform.position)
            ret += i + ":" + result.gameObject.name + "\n";
        }
        return ret;
    }
    public bool ClickedBox(List<RaycastResult> eventSystemRaycastResults)
    {
        for (int i = 0; i < eventSystemRaycastResults.Count; i++)
        {
            RaycastResult result = eventSystemRaycastResults[i];
            if (result.gameObject.tag == "ExplainerDisplay")
            {
  
                return true;
            }
        }
        return false;
    }
    public bool ClickedSame(List<RaycastResult> eventSystemRaycastResults)
    {
        for (int i = 0; i < eventSystemRaycastResults.Count; i++)
        {
            RaycastResult result = eventSystemRaycastResults[i];
            if (result.gameObject.GetComponent<ExplainerTag>() != null)
            {
                if (result.gameObject.GetComponent<ExplainerTag>().TagName == TagName) { WaitToShow = 200; Debug.Log("Same!"); return true; }
                if (result.gameObject.GetComponent<ExplainerTag>().TagText == TagText) { WaitToShow = 200; Debug.Log("Same!"); return true; }

            }
        }
        return false;
    }
    public bool IsFamilyOfExplainerTag(GameObject Obj)
    {
       // Debug.Log("?");
        int Recursion = 0;
        bool FoundParent = false;
        GameObject ParentCandidate = Obj;
        while (!FoundParent && Recursion < 10)
        {
            if (ParentCandidate.GetComponent<ExplainerTag>() != null)
            {
                //Debug.Log("3");
                if (!ParentCandidate.GetComponent<ExplainerTag>().Child)
                {
                   // Debug.Log("o");
                    return true;
                }
            }
            //Debug.Log(ParentCandidate.name);
            //Debug.Log(Recursion +":"+ ParentCandidate.name);
            
            Recursion++;
            if (ParentCandidate.transform.parent != null)
            {
                ParentCandidate = ParentCandidate.transform.parent.gameObject;
            }
            /*else if (ParentCandidate.GetComponent<RectTransform>().parent != null)
            {

                Debug.Log("2");
                ParentCandidate = gameObject.GetComponent<RectTransform>().parent.gameObject;
                Debug.Log(ParentCandidate.name);
            }*/
            else
            {
                //Debug.Log(" :<> ");
                //return false;
            }
            //Debug.Log(Recursion + ":" + ParentCandidate.name);

            /*
            if (ParentCandidate.name == "ToggleConsoleLog")
            {
                Debug.Log("!???!?!!?!??!?!");
                Debug.Log(ParentExplainerTag.);
            }
            */

        }
        return false;
    }
    public GameObject FamilyExplainerTag(GameObject Obj)
    {
       // Debug.Log("?");
        int Recursion = 0;
        bool FoundParent = false;
        GameObject ParentCandidate = Obj;
        while (!FoundParent && Recursion < 10)
        {
            if (ParentCandidate.GetComponent<ExplainerTag>() != null)
            {
               // Debug.Log("3");
                if (!ParentCandidate.GetComponent<ExplainerTag>().Child)
                {
                   // Debug.Log("o:" + ParentCandidate);
                    return ParentCandidate;
                }
            }
            //Debug.Log(ParentCandidate.name);
           // Debug.Log(Recursion + ":" + ParentCandidate.name);

            Recursion++;
            if (ParentCandidate.transform.parent != null)
            {
                ParentCandidate = ParentCandidate.transform.parent.gameObject;
               // Debug.Log(ParentCandidate.name);
               // Debug.Log("1");
            }
            /*else if (ParentCandidate.GetComponent<RectTransform>().parent != null)
            {

                Debug.Log("2");
                ParentCandidate = gameObject.GetComponent<RectTransform>().parent.gameObject;
                Debug.Log(ParentCandidate.name);
            }*/
            else
            {
               // Debug.Log(" :<> ");
                //return false;
            }
            //Debug.Log(Recursion + ":" + ParentCandidate.name);

            /*
            if (ParentCandidate.name == "ToggleConsoleLog")
            {
                Debug.Log("!???!?!!?!??!?!");
                Debug.Log(ParentExplainerTag.);
            }
            */

        }
        return gameObject;
    }
    public ExplainerTag ParentExplainerTag()
    {
        ExplainerTag e = new ExplainerTag();
        return e;
    }
    
    public void SetTextsFromHighest(List<RaycastResult> eventSystemRaycastResults)
    {
        //string ret = "Hmm.";
        for (int i = 0; i < eventSystemRaycastResults.Count; i++)
        {
            RaycastResult result = eventSystemRaycastResults[i];
            //if (result.gameObject.layer == LayerMask.NameToLayer("Resetable Slider") && result.gameObject.transform.position == transform.position)
            //ret += i + ":" + result.gameObject.name + "\n";
            if (IsFamilyOfExplainerTag(result.gameObject))
            {

                ExplainerTag ET = FamilyExplainerTag(result.gameObject).GetComponent<ExplainerTag>();


                if (!(!ET.InMenu && Options.activeSelf))
                {
                    //Debug.Log(result.gameObject.name);
                    //Debug.Log(result.gameObject.name + "AhA!");
                    if (ClickedSame(GetEventSystemRaycastResults())) { Debug.Log("Bye!, Same"); return; }


                    

                    TagName = ET.TagName;
                    TagType = ET.TagType;
                    TagText = ET.TagText;
                    TMPName.text = TagName;
                    TMPType.text = TagType;
                    TMPText.text = TagText;
                    BackgroundColor = ET.BackgroundColor;
                    NameColor = ET.NameColor;
                    TypeColor = ET.TypeColor;
                    TextColor = ET.TextColor;


                    Background.color = BackgroundColor;
                    TMPName.color = NameColor;
                    TMPType.color = TypeColor;
                    TMPText.color = TextColor;
                    ShowExplaination();
                    //Debug.Log(ET.TagName);
                }
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt))
            {
                if (result.gameObject.GetComponent<ExplainerTag>() != null)
                {
                    Debug.Log(result.gameObject.name);
                }
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log(result.gameObject.name);
                if (result.gameObject.transform.parent != null && Input.GetKey(KeyCode.LeftControl))
                {
                    Debug.Log("Parent:" + result.gameObject.transform.parent.name);
                    Debug.Log("Parentx2:" + result.gameObject.transform.parent.transform.parent.name);

                }
            }/*else if (result.gameObject.GetComponent<ExplainerTag>() != null)
            {
                Debug.Log("Archeic");
                ExplainerTag ET = result.gameObject.GetComponent<ExplainerTag>();



                Debug.Log(result.gameObject.name);
                //Debug.Log(result.gameObject.name + "AhA!");
                if (ClickedSame(GetEventSystemRaycastResults())) { return; }

                if (!ET.InMenu && Options.activeSelf)
                {
                    
                    return;
                }
                

                TagName = ET.TagName;
                TagText= ET.TagText;
                TMPName.text = TagName;
                TMPText.text = TagText;
                BackgroundColor = ET.BackgroundColor;
                NameColor = ET.NameColor;
                TextColor = ET.TextColor;


                Background.color = BackgroundColor;
                TMPName.color = NameColor;
                TMPText.color = TextColor;
                ShowExplaination();
                //Debug.Log(ET.TagName);
                return;
            }*/

            //Debug.Log(result.gameObject.name);
        }
    }
    public void CheckTogglables(GameObject Obj)
    {
        /*
        if (Obj.GetComponent<Button>() != null)
        {
            if (Obj.GetComponent<Button>().interactable && Obj.tag != "MenuTogg")
            {
                Debug.Log(Obj.tag);
                Array.Resize(ref ButtonsToEnable, ButtonsToEnable.Length + 1);
                ButtonsToEnable[ButtonsToEnable.Length - 1] = Obj;
                Obj.GetComponent<Button>().interactable = false;
                Debug.Log("Button:" + Obj.name);
            }
            return;
        }
        if (Obj.GetComponent<Slider>() != null)
        {
            if (Obj.GetComponent<Slider>().interactable)
            {
                Array.Resize(ref SlidersToEnable, SlidersToEnable.Length + 1);
                ButtonsToEnable[SlidersToEnable.Length - 1] = Obj;
                Obj.GetComponent<Slider>().interactable = false;
                Debug.Log("Slider:" + Obj.name);
            }
            return;
        }
        if (Obj.GetComponent<Toggle>() != null)
        {
            if (Obj.GetComponent<Toggle>().interactable)
            {
                Array.Resize(ref TogglesToEnable, TogglesToEnable.Length + 1);
                TogglesToEnable[TogglesToEnable.Length - 1] = Obj;
                Debug.Log("Toggle:" + Obj.name);
                Obj.GetComponent<Toggle>().interactable = false;
            }
            return;
        }*/
    }
    public void CheckTogglablesChildren(GameObject Obj)
    {
        /*
        if (Obj.GetComponentInChildren<Button>() != null)
        {
            if (Obj.GetComponentInChildren<Button>().interactable)
            {
                Array.Resize(ref ButtonsToEnable, ButtonsToEnable.Length + 1);
                ButtonsToEnable[ButtonsToEnable.Length - 1] = Obj.GetComponentInChildren<Button>().gameObject;
                Obj.GetComponentInChildren<Button>().interactable = false;
            }
            return;
        }
        if (Obj.GetComponentInChildren<Slider>() != null)
        {




            if (Obj.GetComponentInChildren<Slider>().interactable)
            {
                Array.Resize(ref SlidersToEnable, SlidersToEnable.Length + 1);
                ButtonsToEnable[SlidersToEnable.Length - 1] = Obj.GetComponentInChildren<Slider>().gameObject;
                Obj.GetComponentInChildren<Slider>().interactable = false;
            }
            return;
        }
        if (Obj.GetComponentInChildren<Toggle>() != null)
        {
            if (Obj.GetComponentInChildren<Toggle>().interactable)
            {
                Array.Resize(ref TogglesToEnable, TogglesToEnable.Length + 1);
                TogglesToEnable[TogglesToEnable.Length - 1] = Obj.GetComponentInChildren<Toggle>().gameObject;
                Obj.GetComponentInChildren<Toggle>().interactable = false;
            }
            return;
        }*/
    }
    public void FindInteractables()
    {
        //Just find everything once
        //GameObject[] AllToggles = FindObjectOfType(typeof(GameObject)) as GameObject[];

        ButtonsToEnable = new GameObject[0];
        SlidersToEnable = new GameObject[0];
        TogglesToEnable = new GameObject[0];
        TMPInputsToEnable = new GameObject[0];

        foreach (GameObject Obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (Obj.tag == "MenuTogg")
            {
                //Debug.Log(Obj.tag);
                continue;
            }
            if (Obj.tag != "ExplainerDisplay")
            {


                if (Obj.GetComponent<Button>() != null)
                {
                    if (Obj.GetComponent<Button>().interactable)
                    {

                        Array.Resize(ref ButtonsToEnable, ButtonsToEnable.Length + 1);
                        ButtonsToEnable[ButtonsToEnable.Length - 1] = Obj.GetComponentInChildren<Button>().gameObject;
                    }
                }

                if (Obj.GetComponent<Slider>() != null)
                {
                    if (Obj.GetComponent<Slider>().interactable)
                    {
                        Array.Resize(ref SlidersToEnable, SlidersToEnable.Length + 1);
                        SlidersToEnable[SlidersToEnable.Length - 1] = Obj.GetComponentInChildren<Slider>().gameObject;
                    }
                }
                if (Obj.GetComponent<Toggle>() != null)
                {
                    if (Obj.GetComponent<Toggle>().interactable)
                    {
                        Array.Resize(ref TogglesToEnable, TogglesToEnable.Length + 1);
                        TogglesToEnable[TogglesToEnable.Length - 1] = Obj.GetComponentInChildren<Toggle>().gameObject;
                    }
                }
                if (Obj.GetComponent<TMP_InputField>() != null)
                {
                    if (Obj.GetComponent<TMP_InputField>().interactable)
                    {
                        Array.Resize(ref TMPInputsToEnable, TMPInputsToEnable.Length + 1);
                        TMPInputsToEnable[TMPInputsToEnable.Length - 1] = Obj.GetComponentInChildren<TMP_InputField>().gameObject;
                    }
                }
            }
            }

            //FindObjectsOfTypeAll
        }
    public void DisableInteractables()
    {
        foreach (GameObject Obj in TogglesToEnable)
        {
            Obj.GetComponent<Toggle>().interactable = false;
        }
        foreach (GameObject Obj in ButtonsToEnable)
        {
            Obj.GetComponent<Button>().interactable = false;
        }
        foreach (GameObject Obj in SlidersToEnable)
        {
            Obj.GetComponent<Slider>().interactable = false;
        }
        foreach (GameObject Obj in TMPInputsToEnable)
        {
            Obj.GetComponent<TMP_InputField>().interactable = false;
        }
    }
    public void EnableInteractables()
    {
        foreach (GameObject Obj in TogglesToEnable)
        {
            //Debug.Log("Toggle: " + Obj.name);
            Obj.GetComponent<Toggle>().interactable = true;
        }
        foreach (GameObject Obj in ButtonsToEnable)
        {
            //Debug.Log("Button: " + Obj.name);
            Obj.GetComponent<Button>().interactable = true;
        }
        foreach (GameObject Obj in SlidersToEnable)
        {
            //Debug.Log("Slider: " + Obj.name);
            Obj.GetComponent<Slider>().interactable = true;
        }
        foreach (GameObject Obj in TMPInputsToEnable)
        {
            //Debug.Log("TMP: " + Obj.name);
            Obj.GetComponent<TMP_InputField>().interactable = true;
        }
        /*ButtonsToEnable = new GameObject[0];
        SlidersToEnable = new GameObject[0];
        TogglesToEnable = new GameObject[0];*/
    }
    public void DisableInteractables_Old(List<RaycastResult> eventSystemRaycastResults)
    {
        //string ret = "Hmm.";
        for (int i = 0; i < eventSystemRaycastResults.Count; i++)
        {

            RaycastResult result = eventSystemRaycastResults[i];
            //if (result.gameObject.layer == LayerMask.NameToLayer("Resetable Slider") && result.gameObject.transform.position == transform.position)
            // ret += i + ":" + result.gameObject.name + "\n";
            CheckTogglables(result.gameObject);
            CheckTogglablesChildren(result.gameObject);
            /*
                //Debug.Log(result.gameObject.name + "AhA!");
                TagName = result.gameObject.GetComponent<ExplainerTag>().TagName;
                TagText = result.gameObject.GetComponent<ExplainerTag>().TagText;
                TMPName.text = TagName;
                TMPText.text = TagText;
                BackgroundColor = result.gameObject.GetComponent<ExplainerTag>().BackgroundColor;
                NameColor = result.gameObject.GetComponent<ExplainerTag>().NameColor;
                TextColor = result.gameObject.GetComponent<ExplainerTag>().TextColor;


                Background.color = BackgroundColor;
                TMPName.color = NameColor;
                TMPText.color = TextColor;
                ShowExplaination();
            */
            //Debug.Log(result.gameObject.GetComponent<ExplainerTag>().TagName);

            //Debug.Log(result.gameObject.name);
        }
    }

}
