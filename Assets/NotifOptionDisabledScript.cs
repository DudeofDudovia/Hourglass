using UnityEngine;
using UnityEngine.UI;
public class NotifOptionDisabledScript : MonoBehaviour
{
    public TimeControllerScript TCS;
    public bool NotifPermsOld;
    public Toggle Mastertogg;
    //public bool Mastertoggold;
    //public bool toggold;
    //public bool testmastertoggon;
    public Color CompletelyDisabledColor = Color.gray1;
    public Color SuperDisabledColor = Color.darkGray;
    public Color DisabledColor = Color.gray;
    public Color EnabledColor = Color.white;
    public GameObject[] MasterToggs;
    public GameObject[] SecondLevelToggs;
    public GameObject[] ThirdLevelToggs;

    public GameObject XGameObj;
    public GameObject CheckGameObj;
    /*
    public Image ImageToReplace;
    public Sprite XImage;
    public Sprite OldImage;
    
    public void Awake()
    {
            OldImage = ImageToReplace.sprite;
    }*/
    public void EnableX()
    {
        /*
        if (ImageToReplace != null)
        {
            ImageToReplace.sprite = XImage;
            ImageToReplace.color = Color.white;
        }*/
        XGameObj.SetActive(true);
        XGameObj.GetComponent<Image>().color = Color.white;
        CheckGameObj.SetActive(false);
    }
    public void DisableX()
    {
        XGameObj.SetActive(false);
        CheckGameObj.SetActive(true);
    }
    public void ChangeCol()
    {
        bool MasterEnabled = true;
        if (!TCS.NotificationPerms) { 
            MasterEnabled = false;

        }
        //UnreplaceImage();
        bool MasterOn = true;
        if (!Mastertogg.isOn)
        {
            MasterOn = false;
        }
        foreach (GameObject Togg in MasterToggs)
        {
            if (MasterEnabled)
            {
                ColorChildren(EnabledColor, Togg);
            }
            else
            {
                ColorChildren(DisabledColor, Togg);
            }
        }
        foreach (GameObject Togg in SecondLevelToggs)
        {
            if (MasterEnabled)
            {
                if (MasterOn)
                {
                    ColorChildren(EnabledColor, Togg);
                }
                else
                {
                    ColorChildren(DisabledColor, Togg);
                }
            }
            else
            {
                if (MasterOn)
                {
                    ColorChildren(DisabledColor, Togg);
                }
                else
                {
                    ColorChildren(SuperDisabledColor, Togg);
                }
            }

        }
        foreach (GameObject Togg in ThirdLevelToggs)
        {
            if (Togg.GetComponent<NotifOptionInfo>() != null)
            {
                if (MasterEnabled)
                {
                    if (MasterOn)
                    {
                        if (Togg.GetComponent<NotifOptionInfo>().OptionTogg.isOn)
                        {
                            ColorChildren(EnabledColor, Togg);
                        }
                        else
                        {
                            ColorChildren(DisabledColor, Togg);
                        }
                    }
                    else
                    {
                        if (Togg.GetComponent<NotifOptionInfo>().OptionTogg.isOn)
                        {
                            ColorChildren(DisabledColor, Togg);
                        }
                        else
                        {
                            ColorChildren(SuperDisabledColor, Togg);
                        }
                    }
                }
                else
                {
                    if (MasterOn)
                    {
                        if (Togg.GetComponent<NotifOptionInfo>().OptionTogg.isOn)
                        {
                            ColorChildren(DisabledColor, Togg);
                        }
                        else
                        {
                            ColorChildren(SuperDisabledColor, Togg);
                        }
                    }
                    else
                    {
                        if (Togg.GetComponent<NotifOptionInfo>().OptionTogg.isOn)
                        {
                            ColorChildren(SuperDisabledColor, Togg);
                        }
                        else
                        {
                            ColorChildren(CompletelyDisabledColor, Togg);
                        }
                    }
                }
                
            }
        }
        if (!MasterEnabled)
        {
            EnableX();
        }
        else
        {
            DisableX();
        }
    }
    public bool FirstTicked;
    void Update()
    {
        if (!FirstTicked)
        {
            if (!TCS.NotificationPerms)
            {
                ChangeCol();
                FirstTicked = true;
            }
        }
        if (NotifPermsOld != TCS.NotificationPerms)
        {
            ChangeCol();
        }
        NotifPermsOld = TCS.NotificationPerms;
    }
    public void ColorChildren(Color color, GameObject Target)
    {
        SpriteRenderer[] renderers = Target.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.color = color;
        }
        Image[] renderers3 = Target.GetComponentsInChildren<Image>();
        foreach (Image renderer3 in renderers3)
        {
            renderer3.color = color;
        }
    }
}
