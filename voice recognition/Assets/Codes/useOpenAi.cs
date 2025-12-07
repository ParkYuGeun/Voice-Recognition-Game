using UnityEngine;
using OpenAI;
using Samples.Whisper;
using UnityEngine.UI;

public class useOpenAi : MonoBehaviour
{
    public AudioClip clip;
    float time;
    private readonly string fileName = "output.wav";
    private OpenAIApi openai = new OpenAIApi();
    public Text textline;
    private bool isProcessing = false;
    bool jumpBool;

    string speak;


    int sampleRate = 44100;

    void OnEnable()
    {
        clean();
        isProcessing = false;
        clip = Microphone.Start(Microphone.devices[0].ToString(), false, (int)GameManager.instance.GameTime, sampleRate);
        time = 0;
        jumpBool = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //맞으면점프
        if (speak != null && jumpBool)
        {
            if (speak.ToLower().Contains(GameManager.instance.ShowWord.words[(GameManager.instance.Stage / 2) - 1]))
            {
                GameManager.instance.pool.BroadcastMessage("delete", SendMessageOptions.DontRequireReceiver);
                Debug.Log("일치");
                jumpBool = false;
            }
        }

        if (isProcessing)
            return;
        time += Time.deltaTime;
        if (time >= GameManager.instance.GameTime/3)
        {
            isProcessing = true;
            Microphone.End(Microphone.devices[0].ToString());
            EndRecording();
        }
       
    }

    async void EndRecording()
    {
        byte[] data = SaveWav.Save(fileName, clip);
        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() { Data = data, Name = "audio.wav" },
            // File = Application.persistentDataPath + "/" + fileName,
            Model = "whisper-1",
            Language = "en"
        };
        var res = await openai.CreateAudioTranscription(req);
        textline.text = res.Text;
        
        speak = res.Text;

    }

    void clean() {
        // 1. [핵심] 혹시라도 마이크가 켜져 있다면 강제로 끕니다.
        // (이전 스테이지에서 녹음하다가 중간에 넘어왔을 경우 방지)
        if (Microphone.IsRecording(null))
        {
            Microphone.End(null);
        }

        // 2. [핵심] 이전에 쓰던 오디오 클립이 남아있다면 메모리에서 완전히 삭제합니다.
        // Destroy를 쓰지 않으면 변수에 옛날 데이터가 남아있을 수 있습니다.
        if (clip != null)
        {
            Destroy(clip); // 기존 클립 파괴
            clip = null;   // 변수 연결 끊기
        }
    }

}
