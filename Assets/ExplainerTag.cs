using UnityEditor;
using UnityEngine;

public class ExplainerTag : MonoBehaviour
{
    public string TagName;
    [TextArea]
    public string TagText;
    public Color NameColor = Color.white;
    public Color TextColor = Color.white;
    public Color BackgroundColor = Color.black;

    public bool InMenu;
    public bool Child;
    // Update is called once per frame
    void Awake()
    {
        //if (Child)
        {
            Debug.Log("?");
            int Recursion = 0;
            bool FoundParent = false;
            GameObject ParentCandidate = gameObject;
            Debug.Log(FoundParent);
            Debug.Log(Recursion);
            while (!FoundParent && Recursion < 10)
            {
                Debug.Log(ParentCandidate.name);
                Recursion++;
                if (ParentCandidate.transform.parent != null)
                {
                    ParentCandidate = gameObject.transform.parent.gameObject;
                }
                else if (ParentCandidate.GetComponent<RectTransform>().parent != null)
                {

                    ParentCandidate = gameObject.GetComponent<RectTransform>().parent.gameObject;
                    Debug.Log(ParentCandidate.name);
                }
                else
                {
                    Debug.Log(" :<> ");
                    break;
                }
                if (ParentCandidate.GetComponent<ExplainerTag>() != null)
                {
                    if (!ParentCandidate.GetComponent<ExplainerTag>().Child)
                    {
                        ExplainerTag ParentTag = gameObject.transform.parent.GetComponent<ExplainerTag>();
                        TagName = ParentTag.TagName;
                        TagText = ParentTag.TagText;
                        NameColor = ParentTag.NameColor;
                        TextColor = ParentTag.TextColor;
                        BackgroundColor = ParentTag.BackgroundColor;
                        InMenu = ParentTag.InMenu;
                        FoundParent = true;
                        Debug.Log(ParentCandidate.name);
                    }
                }
            }

            /*
            if (gameObject.transform.parent.GetComponent<ExplainerTag>() != null)
            {

                if (gameObject.transform.parent.GetComponent<ExplainerTag>().Child)
                {

                }
                else
                {
                    ExplainerTag ParentTag = gameObject.transform.parent.GetComponent<ExplainerTag>();
                    TagName = ParentTag.TagName;
                    TagText = ParentTag.TagText;
                    NameColor = ParentTag.NameColor;
                    TextColor = ParentTag.TextColor;
                    BackgroundColor = ParentTag.BackgroundColor;
                }
            }*/
        }
    }
}
