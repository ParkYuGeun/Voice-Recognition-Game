using UnityEngine;
using UnityEngine.UI;


public class Hud : MonoBehaviour
{
    public enum infotype {slide, name, stage, time}
    public infotype type;

    Text myText;
    Slider mySlider;

     void Awake()
    {
        mySlider = GetComponent<Slider>();
        myText = GetComponent<Text>();
    }

     void LateUpdate()
    {
        switch (type)
        {
            case infotype.slide:
                float max = GameManager.instance.GameTime;
                float min = GameManager.instance.time;
                mySlider.value = min / max;
                break;

            case infotype.stage:
                myText.text = "Stage: " + GameManager.instance.stage;
                break;



        }
    }
}
