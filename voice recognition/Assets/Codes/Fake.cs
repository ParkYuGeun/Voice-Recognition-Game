using UnityEngine;

public class Fake : MonoBehaviour
{
    [Header("마이크 설정")]
    public AudioClip aud;
    public float moduleate = 5000f; // 감도 (집 기준 5000 권장)
    public int cutValue = 10;       // 최소 인식 임계값 (이것보다 작으면 0 처리)
    
    [Header("실시간 분석 데이터")]
    public float rmsValue;          // 계산된 실수형 볼륨
    public int resultValue;         // 최종 정수형 볼륨 (Player가 가져가는 값)

    private int sampleRate = 44100; // 음질 표준
    private int sampleWindow = 128; // 구간 크기 

    void Awake()
    {
        // 안전장치: GameManager가 없거나 설정이 안 되어 있을 경우 기본값 1
        if (GameManager.instance != null)
        {
            // 사실 1초만 있어도 충분합니다 (계속 덮어쓰기 때문)
            int duration = (int)GameManager.instance.GameTime;
            if (duration < 1) duration = 1;
        }
    }

    void OnEnable()
    {
        // 1. 시작 전 청소 (가장 중요!)
        clean();

        // 마이크 녹음 시작 
        // 계속 덮어쓰며 녹음하므로 loop: true, lengthSec: 1
        if (Microphone.devices.Length > 0)
        {
            aud = Microphone.Start(Microphone.devices[0].ToString(), true, 1, sampleRate);
        }
        else
        {
            Debug.LogError("마이크 장치를 찾을 수 없습니다!");
        }
    }

    void OnDisable()
    {
        // 오브젝트가 꺼질 때 마이크도 확실하게 끔
        clean();
    }

    void Update()
    {
        if (aud == null) return;

        //현재 마이크가 녹음하고 있는 헤드 위치(커서) 파악
        int micPosition = Microphone.GetPosition(null);

        //데이터가 샘플링 윈도우(128개)보다 적게 모였으면 계산 생략
        if (micPosition < sampleWindow) return;

        //가장 '최근'에 녹음된 짧은 구간(128개)만 가져오기
        // (micPosition - sampleWindow) 위치부터 sampleWindow 개수만큼
        float[] waveData = new float[sampleWindow];
        aud.GetData(waveData, micPosition - sampleWindow);

        //RMS(Root Mean Square) 에너지 계산
        float sum = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            // 파형의 진폭을 제곱해서 더함 (음수 제거 및 에너지 증폭)
            sum += waveData[i] * waveData[i];
        }

        // 평균(Mean) -> 제곱근(Root) -> 감도 조절(Modulate)
        rmsValue = Mathf.Sqrt(sum / sampleWindow) * moduleate;
        
        // 값 보정 및 결과 출력
        rmsValue = Mathf.Clamp(rmsValue, 0, 100); // 0~100 사이로 제한
        resultValue = Mathf.RoundToInt(rmsValue);

        // 잡음 제거 (컷오프)
        if (resultValue < cutValue)
        {
            resultValue = 0;
        }
    }

    // 마이크 초기화 함수
    void clean()
    {
        // 녹음 중이라면 중지
        if (Microphone.IsRecording(null))
        {
            Microphone.End(null);
        }

        // 메모리에 남은 오디오 클립 삭제 (이전 소리 잔상 제거)
        if (aud != null)
        {
            Destroy(aud);
            aud = null;
        }
    }
}