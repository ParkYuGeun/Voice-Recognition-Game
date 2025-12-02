using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    public int cut_Range;
    public  int Mic_Range;
    public Mic2 Mic;
    public Sprite[] Images;

    private Image targetImage;

    private void Start()
    {
        targetImage = GetComponent<Image>();
    }

    public void Update()
    {
        Mic_Range = Mic.resultValue;
        if (Mic_Range >= cut_Range)
        {
            targetImage.sprite = Images[1];
        }
        else
        {
            targetImage.sprite = Images[0];
        }
    }
}
