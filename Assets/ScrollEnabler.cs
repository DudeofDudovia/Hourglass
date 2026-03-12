using UnityEngine;
using UnityEngine.UI;

public class ScrollEnabler : MonoBehaviour
{
    public ScrollRect SR;
    void Awake()
    {
        SR.enabled = true;
    }
}
