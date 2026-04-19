using UnityEngine;
using UnityEngine.UI;

public class AddedTimesFadeOutPreviewAnimScript : MonoBehaviour
{
    public RectTransform RT;
    public TimeControllerScript TCS;
    public Slider ATFOSlider;
    public int animframes = 0;
    public int frame = 0;
    public float animscalefactor = 0;

    public float value = 0;
    public float horizontal = 1;
    public float scale = 1;
    public bool FirstTick = false;

    public int Life;
    public int LifeTime;
    public Image IMG;
    void FixedUpdate()
    {
        if (!FirstTick)
        {
            Life = 0;
            LifeTime = 0;
            FirstTick = true;
        }
        animframes = (int)ATFOSlider.value;
        transform.localScale = Vector3.one;
        IMG.color = Color.white;
        if (Life > 0)
        {
            Life--;
            transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), Vector3.one, (float)Life / (float)LifeTime);
            IMG.color = Color.Lerp(Color.gray1, Color.red, (float)Life / (float)LifeTime);
        }
    }
    public void PreviewAnimation()
    {
        animframes = (int)ATFOSlider.value;
        Life = animframes;
        LifeTime = animframes;
        TCS.DeletedProfLifeTime = animframes;
    }
}
