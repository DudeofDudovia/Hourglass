using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlatformOptionGrayOutScript : MonoBehaviour
{
    public bool DisableWindows;
    public void Awake()
    {
#if UNITY_EDITOR && PLATFORM_STANDALONE_WIN
        if (DisableWindows)
            DisableChildren();
#endif
        if (DisableWindows && Application.platform == (RuntimePlatform.WindowsPlayer))
        {
            DisableChildren();
        }
    }
    public void DisableChildren()
    {
        ColorChildren(Color.gray2);
        ColorChildrenText(Color.gray4);
        DisableChildrenToggles();
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
    public void ColorChildrenText(Color color)
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
        {
            text.color = color;
        }
    }
    public void DisableChildrenToggles()
    {
        Toggle[] Options = GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in Options)
        {
            toggle.enabled = false;
        }
    }
}
