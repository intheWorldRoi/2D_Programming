using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour

{
    public TypeEffect effect; //��µ� �ؽ�Ʈ ����ٳ���
    Queue<string> sentences = new Queue<string>(); // ��µ� ����� �־���� �迭
    public string[] intro; //�������1
    public string[] intro2; //�������2

    public GameObject TextBox; //�ؽ�Ʈ������Ʈ(�ؽ�Ʈ�ڽ������ִ�)

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
        if(sentences.Peek().Contains("������ �����;� ..")) // ���� �б� ������
        {
            nowNumber = 1;
        }
        else if(sentences.Peek().Contains("�ϴ��� ����!"))
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
                Invoke("BoxMove", 3f); // �ڽ��ִϸ��̼�
                break;
            case 2:
                for(int i = 0; i < obstacle.Length; i++)
                {
                    obstacle[i].SetActive(false); // �̵����� ���ϰ��س��� �庮�� ����
                }
                TextBox.SetActive(false); // �عڵ� ����
                break;
        }
        
    }

    void BoxMove()
    {
        TextBox.SetActive(true);
        if (BoxSwitch)
        {
            TextBox.transform.position = new Vector3(400, 250, 0);
            Begin(intro2); // ��ȭ����
            BoxSwitch = false;
        }
    }
}
