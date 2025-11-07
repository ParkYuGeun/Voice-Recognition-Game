using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mic : MonoBehaviour
{
    AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void PlaySnd()
    {
        aud.Play();
    }

    public void RecSnd()
    {
        aud.clip = Microphone.Start(Microphone.devices[0].ToString(),false,3,44100);
        //매개변수설정//1.입력받을 기기 2.루프여부 3.녹음시간
    }
}
