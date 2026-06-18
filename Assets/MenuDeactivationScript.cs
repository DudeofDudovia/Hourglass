using UnityEngine;

public class MenuDeactivationScript : MonoBehaviour
{
    public Canvas canv;
    public bool FirstTick = false;
    void Start()
    {
        canv.enabled = true;
        gameObject.SetActive(false);
    }
    public void Update()
    {
        /*
        if (!FirstTick)
        {
            canv.enabled = true;
            gameObject.SetActive(false);
            FirstTick = true;

        }*/
    }
}
