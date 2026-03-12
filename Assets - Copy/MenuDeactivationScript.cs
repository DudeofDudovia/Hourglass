using UnityEngine;

public class MenuDeactivationScript : MonoBehaviour
{
    public Canvas canv;
    void Start()
    {
        canv.enabled = true;
        gameObject.SetActive(false);
    }
}
