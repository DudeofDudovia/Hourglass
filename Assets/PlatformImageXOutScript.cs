using UnityEngine;
using UnityEngine.UI;

public class PlatformImageXOutScript : MonoBehaviour
{
    public bool DisableWindows;
    public Image ImageToReplace;
    public Sprite XImage;
    public SavableValueScript SVS;
    public void Awake()
    {
#if UNITY_EDITOR && PLATFORM_STANDALONE_WIN
        if (DisableWindows)
            ReplaceImage();
#endif
        if (DisableWindows && Application.platform == (RuntimePlatform.WindowsPlayer))
        {
            ReplaceImage();
        }
    }
    public void ReplaceImage()
    {
        if (ImageToReplace != null)
        {
            ImageToReplace.sprite = XImage;
            ImageToReplace.color = Color.white;
        }
        if (SVS != null)
        {
            SVS.VarToToggle = null;
            SVS.VarToFloat = null;
            SVS.VarToInt = null;
        }
    }
}