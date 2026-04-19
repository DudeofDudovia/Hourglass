using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScript : MonoBehaviour
{
    public bool Hovering = false;
    void Update()
    {
        Hovering = false;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Hovering = true;
        }
    }
}
