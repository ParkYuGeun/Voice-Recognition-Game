using UnityEngine;

public class MicManager : MonoBehaviour
{
    public static MicManager instance;

    public AudioClip aud;
    public float moduleate;
    public float rmsValue;
    public int resultValue;
    public int cutValue;
    public int minVoiceValue = 10;  // 최소 목소리 크기
    public int maxVoiceValue = 50;  // 최대 목소리 크기


    int sampleRate = 44100;
    private float[] samples;
    

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
            
        samples = new float[sampleRate];
        aud = Microphone.Start(Microphone.devices[0].ToString(),true, 1, sampleRate);
    }


    void Update()
    {
        aud.GetData(samples, 0);

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
        else
        {
            // 목소리 크기를 설정한 범위로 제한
            resultValue = Mathf.Clamp(resultValue, minVoiceValue, maxVoiceValue);
        }
    }
}
