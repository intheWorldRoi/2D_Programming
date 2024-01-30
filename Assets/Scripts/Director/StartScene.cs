using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour

{
    public TypeEffect effect; //출력될 텍스트 끌어다놓기
    Queue<string> sentences = new Queue<string>(); // 출력될 문장들 넣어놓는 배열
    public string[] intro; //문장더미1
    public string[] intro2; //문장더미2

    public GameObject TextBox; //텍스트오브젝트(텍스트박스까지있는)

    public GameObject[] obstacle;
    
    bool BoxSwitch;

    int nowNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        Begin(intro);
        
    }

    public void Begin(string[] info)
    {
        sentences.Clear();

        foreach(var sentence in info)
        {
            sentences.Enqueue(sentence);
        }

        Next();
    }

    public void Next()
    {
        if(sentences.Count == 0)
        {
            End();
            return;
        }
        if(sentences.Peek().Contains("게임을 만들고싶어 ..")) // 연출 분기 나누기
        {
            nowNumber = 1;
        }
        else if(sentences.Peek().Contains("일단은 가자!"))
        {
            nowNumber = 2;
        }
        effect.SetMsg(sentences.Dequeue());
    }

    public void End()
    {
        switch (nowNumber)
        {
            case 1:
                Camera.main.GetComponent<Animator>().SetBool("cameraMove", true);
                TextBox.SetActive(false);
                BoxSwitch = true; 
                Invoke("BoxMove", 3f); // 박스애니메이션
                break;
            case 2:
                for(int i = 0; i < obstacle.Length; i++)
                {
                    obstacle[i].SetActive(false); // 이동하지 못하게해놓은 장벽들 끄기
                }
                TextBox.SetActive(false); // 텍박도 끄기
                break;
        }
        
    }

    void BoxMove()
    {
        TextBox.SetActive(true);
        if (BoxSwitch)
        {
            TextBox.transform.position = new Vector3(400, 250, 0);
            Begin(intro2); // 대화시작
            BoxSwitch = false;
        }
    }
}
