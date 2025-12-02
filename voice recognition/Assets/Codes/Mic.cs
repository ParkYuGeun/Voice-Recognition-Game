using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mic : MonoBehaviour
{
    public int micNum;
    AudioSource aud;
    bool isRecording = false;


    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void PlaySnd()
    {
        if (!isRecording)
        {
            // 녹음 시작 없이 바로 PlaySnd()가 호출되는 경우를 대비
            aud.Play();
            return;
        }
        // 1. 녹음 중지
        Microphone.End(Microphone.devices[micNum].ToString());

        // 2. 재생 시점 조정 (필요하다면)
        // 3초 미만으로 녹음했을 경우, 실제로 녹음된 부분만 클립으로 자릅니다.
        int position = Microphone.GetPosition(Microphone.devices[micNum].ToString());
        if (position > 0)
        {
            // 녹음된 길이만큼 클립을 자르는 코드가 복잡하면 생략 가능
            // 3초 전체를 재생해도 무방합니다.
        }

        // 3. 녹음된 클립 재생
        aud.Play();
        isRecording = false;
        Debug.Log("녹음 중지 및 재생!");
    }

    public void RecSnd()
    {

        string micName = Microphone.devices.Length > 0 ? Microphone.devices[micNum] : null;
        if (micName != null)
        {
            aud.clip = Microphone.Start(micName, false, 3, 44100);
            isRecording = true;
            Debug.Log($"녹음시작,{micName}");
        }
        else
        {
            Debug.LogError("마이크 장치가 감지되지 않았습니다.");
        }

        if (isRecording)
            return;
        //매개변수설정//1.입력받을 기기 2.루프여부 3.녹음시간

    }

    public void testClick()
    {
        Debug.Log("클릭됨");
    }
}
