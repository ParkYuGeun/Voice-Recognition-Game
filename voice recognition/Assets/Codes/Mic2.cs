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
        clean();
        samples = new float[sampleRate];
        aud = Microphone.Start(Microphone.devices[0].ToString(), true, 1, sampleRate);
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

    void clean()
    {
        // 1. [핵심] 혹시라도 마이크가 켜져 있다면 강제로 끕니다.
        // (이전 스테이지에서 녹음하다가 중간에 넘어왔을 경우 방지)
        if (Microphone.IsRecording(null))
        {
            Microphone.End(null);
        }

        // 2. [핵심] 이전에 쓰던 오디오 클립이 남아있다면 메모리에서 완전히 삭제합니다.
        // Destroy를 쓰지 않으면 변수에 옛날 데이터가 남아있을 수 있습니다.
        if (aud != null)
        {
            Destroy(aud); // 기존 클립 파괴
            aud = null;   // 변수 연결 끊기
        }
    }
}
