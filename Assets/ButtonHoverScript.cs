using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScript : MonoBehaviour
{
    public bool Hovering = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hovering = false;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Hovering = true;
        }
    }
}
