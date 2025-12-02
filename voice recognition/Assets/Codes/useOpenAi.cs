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



    int sampleRate = 44100;

    void OnEnable()
    {
        isProcessing = false;
        clip = Microphone.Start(Microphone.devices[0].ToString(), false, 1, sampleRate);
        time = 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isProcessing)
            return;
        time += Time.deltaTime;
        if (time >= GameManager.instance.GameTime - 1f)
        {
            isProcessing = true;
            Microphone.End(null);
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


    }


}
