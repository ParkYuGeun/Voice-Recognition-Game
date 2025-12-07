using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float time;
    public float GameTime = 5f;
    public string[] stageName;

    // ▼ [수정 1] stage를 내부 변수와 프로퍼티로 분리
    private int _stage;
    public int Stage // 대문자 S로 시작
    {
        get { return _stage; }
        set
        {
            // 값이 바뀔 때만 실행
            if (_stage != value && isalive)
            {
                _stage = value;
                OnStageChanged(); // 변경 감지 함수 호출
            }
        }
    }

    public bool isalive;

    [SerializeField]
    public Player player;
    public Mic2 micManager;
    public useOpenAi api;
    public WordData myData;
    public ShowWord ShowWord; // ShowWord 스크립트 연결 변수
    public PoolManager pool;
    public GameObject EndBanner;

    void Awake()
    {
        // 1. 싱글톤 설정 먼저
        instance = this;

        LoadWord();
    }

    void Start()
    {
        Time.timeScale = 1;
    }

    public void Init() {
        ShowWord.transform.localScale = new Vector3(1,1,1);
        isalive = true;
        Stage = 1; // [수정 2] 시작할 때 스테이지 1로 설정 (자동으로 OnStageChanged 실행됨)
    }

    void Update()
    {
        if (!isalive)
            return;

        time += Time.deltaTime;

        if (time > GameTime)
        {
            time = 0;
            Stage += 1; // [수정 3] 소문자 stage 대신 대문자 Stage 사용
        }
    }

    // ▼ [수정 4] FixedUpdate 제거하고 이 함수로 대체
    // 스테이지가 바뀔 때 딱 한 번만 실행되는 함수
    void OnStageChanged()
    {
        
        // 1. 게임 오버 체크
        if (_stage > 10)
        {
            Time.timeScale = 0;
            isalive = false;
            return;
        }

        // 2. 홀수/짝수 로직 처리
        if (_stage % 2 == 1) // 홀수 (마이크 켜기)
        {
            //player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            player.getDown();
            micManager.gameObject.SetActive(true);
            api.gameObject.SetActive(false);
            Spawn(0);

            if (ShowWord != null)
            {
                ShowWord.showNextWord();
            }
        }
        else // 짝수 (AI 켜기 + 단어 보여주기)
        {
            //player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            player.getDown();
            micManager.gameObject.SetActive(false);
            api.gameObject.SetActive(true);
            Spawn(1);
            //다른 벽 생성

            if (ShowWord != null)
            {
                ShowWord.showNextWord();
            }
        }
    }

    // FixedUpdate는 더 이상 필요 없어서 삭제했습니다. (기능을 OnStageChanged로 옮김)

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
        EndBanner.SetActive(true);
        Text endText = EndBanner.GetComponentsInChildren<Text>()[0];
        endText.text = string.Format("내점수 : {0}",Stage-1);
    }

    public void LoadWord()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Word");
        if (jsonFile != null)
        {
            myData = JsonUtility.FromJson<WordData>(jsonFile.text);

            // 데이터 확인용 로그 (Words에 데이터가 있을 때만 출력)
            if (myData != null && myData.Words != null && myData.Words.Count >= 3)
            {
                Debug.Log(myData.Words[0]);
                Debug.Log(myData.Words[1]);
                Debug.Log(myData.Words[2]);
            }
        }
        else
        {
            Debug.LogError("단어파일을 찾을 수 없습니다");
        }
    }

    void Spawn(int index) {
        GameObject wall = pool.Get(index);
       
    }
}

[System.Serializable]
public class WordData
{
    public List<string> Words;
}