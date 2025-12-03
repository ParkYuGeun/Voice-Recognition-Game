using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float time;
    public float GameTime = 2f;
    public string[] stageName;
    public int stage ;
    public bool isalive;
    [SerializeField]


    public Player player;
    public Mic2 micManager;
    public useOpenAi api;
    public WordData myData;

    void Awake()
    {
        instance = this;
    }

     void Start()
    {
        LoadWord();
        Time.timeScale = 1;
        isalive = true;
    }

    void Update()
    {
        if (!isalive)
            return;
        time += Time.deltaTime;
        if (time > GameTime) {
            time = 0;
            stage += 1;
        }

        if (stage > 10) {
            Time.timeScale = 0;
            isalive = false;
        }
    }

     void FixedUpdate()
    {
        if (stage%2 == 1 )
        {
            micManager.gameObject.SetActive(true);
            api.gameObject.SetActive(false);
        }
        else if(stage % 2 == 0) {
            micManager.gameObject.SetActive(false);
            api.gameObject.SetActive(true);
        }
    }

    public void retry()
    {
        SceneManager.LoadScene(0);
    }

    public void dead()
    {
        micManager.gameObject.SetActive(false);
        api.gameObject.SetActive(false);
        isalive = false;
        Time.timeScale = 0;

    }

    public void LoadWord() {
        TextAsset jsonFile = Resources.Load<TextAsset>("Word");
        if (jsonFile != null) {
            myData = JsonUtility.FromJson<WordData>(jsonFile.text);

            Debug.Log(myData.Words[0]);
            Debug.Log(myData.Words[1]);
            Debug.Log(myData.Words[2]);
        }
        else
        {
            Debug.LogError("단어파일을 찾을 수 없습니다");
        }
    }

}

[System.Serializable]
public class WordData
{
    public List<string> Words;
}
