using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWord : MonoBehaviour
{

    public List <string> words;
    public string[] selectedWord;
    int[] index;
    Text textLine;

    private void Awake ()
    {
        index = new int[5];
        selectedWord = new string[5];
        textLine = GetComponent<Text>();
    }
    void Start()
    {
        words = GameManager.instance.myData.Words;
        
        while (true) {
            for (int i = 0; i < 5; i++)
            {
                index[i] = Random.Range(0, 5);
            }
            if (index[0] != index[1] && index[1] != index[2] && index[2] != index[3] && index[3] == index[4])
                break;
        }

        for (int i = 0; i < index.Length; i++) {
            selectedWord[i] = words[index[i]];
        }

    }

    private void Update()
    {
        
    }

    public void showNextWord() {
        if (GameManager.instance.Stage % 2 == 1)
        {
            textLine.text = "소리를 질러 조작하세요";
        }
        else if (GameManager.instance.Stage % 2 == 0) {
            textLine.text = words[(GameManager.instance.Stage/2) - 1];
        }

    }
}
