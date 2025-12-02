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

    void Awake()
    {
        instance = this;
    }

     void Start()
    {
        Time.timeScale = 1;
        isalive = true;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > GameTime) {
            time = 0;
            stage += 1;
        }

        if (stage > 10) {
            Time.timeScale = 0;
            stage = 999;
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
}
