using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject Menu;
    public void ToggleMenu()
    {
        Menu.SetActive(!Menu.activeSelf);
    }
}
