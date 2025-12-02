using UnityEngine;

public class VoiceHeightTest : MonoBehaviour
{
    [Header("높이 설정")]
    public float minHeight = -3f;  // 최소 높이 (바닥 근처)
    public float maxHeight = 5f;   // 최대 높이 (하늘)
    public float smoothSpeed = 5f; // 높이 변화 부드러움 정도
    
    [Header("디버그 정보")]
    public bool showDebugInfo = true;
    public float currentVoiceValue = 0f;
    public float currentHeight = 0f;
    public float targetHeight = 0f;
    
    private float groundLevel = -4f; // 바닥 레벨 (절대 넘지 않음)

    void Start()
    {
        // 시작 위치를 중간 높이로 설정
        Vector3 startPos = transform.position;
        startPos.y = (minHeight + maxHeight) / 2f;
        transform.position = startPos;
    }

    void Update()
    {
        if (MicManager.instance == null)
        {
            if (showDebugInfo)
            {
                Debug.LogWarning("MicManager.instance가 없습니다!");
            }
            return;
        }

        
        // 디버그 정보 출력
        if (showDebugInfo)
        {
            currentVoiceValue = MicManager.instance.resultValue;
            currentHeight = transform.position.y;
        }
    }

    private void FixedUpdate()
    { 
        // 목소리 크기를 기반으로 높이 계산
        UpdateHeightByVoice();
    }

    void UpdateHeightByVoice()
    {
        // 목소리 크기가 0이면 중간 높이로 이동
        if (MicManager.instance.resultValue == 0)
        {
            targetHeight = (minHeight + maxHeight) / 2f;
        }
        else
        {
            // 목소리 크기를 minVoiceValue~maxVoiceValue 범위에서 0~1로 정규화
            float normalizedVoice = Mathf.InverseLerp(
                MicManager.instance.minVoiceValue,
                MicManager.instance.maxVoiceValue,
                MicManager.instance.resultValue
            );
            
            // 정규화된 값을 minHeight~maxHeight로 매핑
            targetHeight = Mathf.Lerp(minHeight, maxHeight, normalizedVoice);
        }
        
        // 바닥 레벨보다 아래로 가지 않도록 제한
        targetHeight = Mathf.Max(targetHeight, groundLevel);
        
        // 현재 위치에서 목표 높이로 부드럽게 이동
        Vector3 currentPos = transform.position;
        float newY = Mathf.Lerp(currentPos.y, targetHeight, Time.deltaTime * smoothSpeed);
        
        // 바닥 레벨 체크 (이중 안전장치)
        newY = Mathf.Max(newY, groundLevel);
        
        transform.position = new Vector3(currentPos.x, newY, currentPos.z);
    }

    // Unity Editor에서 실시간으로 값 확인용
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        GUI.color = Color.black;
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;
        
        int yOffset = 10;
        GUI.Label(new Rect(10, yOffset, 400, 30), $"목소리 값: {currentVoiceValue}", style);
        yOffset += 30;
        GUI.Label(new Rect(10, yOffset, 400, 30), $"현재 높이: {currentHeight:F2}", style);
        yOffset += 30;
        GUI.Label(new Rect(10, yOffset, 400, 30), $"목표 높이: {targetHeight:F2}", style);
        yOffset += 30;
        
        if (MicManager.instance != null)
        {
            GUI.Label(new Rect(10, yOffset, 400, 30), 
                $"목소리 범위: {MicManager.instance.minVoiceValue} ~ {MicManager.instance.maxVoiceValue}", style);
            yOffset += 30;
            GUI.Label(new Rect(10, yOffset, 400, 30), 
                $"높이 범위: {minHeight:F1} ~ {maxHeight:F1}", style);
        }
    }
}

