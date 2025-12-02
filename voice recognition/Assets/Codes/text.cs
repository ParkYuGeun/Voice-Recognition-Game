using TMPro;
using UnityEngine;

public class text : MonoBehaviour
{
    public ImageChange mic;
    public TextMeshProUGUI textMesh;
    private void Start()
    {
        mic = GetComponentInParent<ImageChange>();
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        int value = mic.cut_Range;
        if (textMesh != null)
        {
            textMesh.text = value.ToString();
        }
    }

}
