using Unity.VisualScripting;
using UnityEngine;

public class Mic2 : MonoBehaviour
{

    public AudioClip aud;
    public float moduleate; //집 기준 5000권장
    public float rmsValue;
    public int resultValue;
    public int cutValue;
    public int duration;


    int sampleRate = 44100;
    private float[] samples;
    

    void Awake()
    {
        duration = (int)GameManager.instance.GameTime;
    }

     void OnEnable()
    {
        samples = new float[sampleRate];
        aud = Microphone.Start(Microphone.devices[0].ToString(), true, duration, sampleRate);
        //aud = GetComponent<AudioClip>();
    }

    void Update()
    {
        aud.GetData(samples, 0);

        //평균 구하기
        //원래대로 구하면 0에 수렴하기 때문에 
        //배열의 값을 전부 제곱하여 더해준 후 배열의 크기만큼 나눈 뒤 제곱근을 구함
        float sum = 0;
        for (int i = 0; i < samples.Length; i++) { 
        sum += Mathf.Pow(samples[i],2);
        }
        rmsValue =  Mathf.Sqrt(sum / samples.Length)*moduleate;
        rmsValue = Mathf.Clamp(rmsValue,0,100);
        resultValue = Mathf.RoundToInt(rmsValue);
        if (resultValue < cutValue)
        {
            resultValue = 0;
        }

    }
}
