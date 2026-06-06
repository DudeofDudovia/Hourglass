using UnityEngine;
using UnityEngine.UI;

public class NotifOptionGrayOutScript : MonoBehaviour
{
    public Toggle togg;
    public bool forslider;
    public Toggle Mastertogg;
    public bool Mastertoggold;
    public bool toggold;
    public Color SuperDisabledColor = Color.darkGray;
    public Color DisabledColor = Color.gray;
    public Color EnabledColor = Color.white;
    public bool tintstatus;
    public int wait = 0;
    void ChangeCol()
    {
        ColorChildren(EnabledColor);
        if (forslider)
        {
            if (!togg.isOn && !Mastertogg.isOn)
            {
                ColorChildren(SuperDisabledColor);
            }
            else if (!togg.isOn || !Mastertogg.isOn)
            {
                ColorChildren(DisabledColor);
            }

        }
        else
        {
            if (togg.isOn)
            {

            }
            if (!togg.isOn)
            {
                ColorChildren(DisabledColor);
            }
        }


    }
    public bool FirstTicked;
    void Update()
    {
        if (!FirstTicked)
        {
            ChangeCol();
            FirstTicked = true;
        }
        if (togg.isOn != toggold)
        {
            ChangeCol();
        }
        if (forslider)
        {
            if (Mastertogg.isOn != Mastertoggold)
            {
                ChangeCol();
            }
            Mastertoggold = Mastertogg.isOn;
        }

        toggold = togg.isOn;
        
        /*
        if (!togg.isOn) { TintChildren(DisabledColor); }
        if (!Mastertogg.isOn) { TintChildren(DisabledColor); }
        if (!forslider)
        {
            if (!togg.isOn)
            {

                if (!tintstatus) { TintChildren(DisabledColor); }
                tintstatus = true;
            }
            else
            {
                if (forslider && Mastertogg.isOn)
                {
                    if (tintstatus) { TintChildren(EnabledColor); }
                    tintstatus = false;
                }
                else if (!forslider)
                {
                    if (tintstatus) { TintChildren(EnabledColor); }
                    tintstatus = false;
                }

            }
        }
        else
        {
            ColorChildren(EnabledColor);
            if (!togg.isOn) { TintChildren(DisabledColor); }
            if (!Mastertogg.isOn) { TintChildren(DisabledColor); }
            


            if (Mastertogg != Mastertoggold)
            {
                ColorChildren(DisabledColor);




                tintstatus = !Mastertogg.isOn;
            }
            Mastertoggold = Mastertogg.isOn;
            if (wait == 0)
            {
                Debug.Log("?");
                if (!togg.isOn)
                {
                    if (!tintstatus) { TintChildren(DisabledColor); }
                    tintstatus = true;
                }
                else if (Mastertogg.isOn)
                {
                    if (tintstatus) { TintChildren(EnabledColor); }
                    tintstatus = false;
                }
            }
            wait--;


        }
        */
    }
    public void TintChildren(Color color)
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            if (color != EnabledColor)
            {
                Color LerpedCol = Color.Lerp(renderer.color, color, .5f);
                renderer.color *= LerpedCol;
            }
            else
            {
                renderer.color = EnabledColor;
            }
           
        }
        Image[] renderers3 = GetComponentsInChildren<Image>();
        foreach (Image renderer3 in renderers3)
        {
            if (color != EnabledColor)
            {
                Color LerpedCol = Color.Lerp(renderer3.color, color, .5f);
                renderer3.color *= LerpedCol;
            }
            else
            {
                renderer3.color = EnabledColor;
            }
            
        }
    }
    public void ColorChildren(Color color)
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.color = color;
        }
        Image[] renderers3 = GetComponentsInChildren<Image>();
        foreach (Image renderer3 in renderers3)
        {
            renderer3.color = color;
        }
    }
}
