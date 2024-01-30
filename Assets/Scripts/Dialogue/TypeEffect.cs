using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TypeEffect : MonoBehaviour
{
    string targetMsg;
    public int CPS; //CharPerSecond = �ӵ�
    TextMeshProUGUI msgtext;
    int index;

    float interval;

   
    

    private void Awake()
    { 
        msgtext = GetComponent<TextMeshProUGUI>();
    }



    public void SetMsg(string msg)
    {
        msg = msg.Replace("\\n", "\n"); //�ٹٲٱ� �ν�
        targetMsg = msg; // ��ü �� ���� Ȯ��
        EffectStart();
        
    }

    private void EffectStart()
    {
        msgtext.text = "";
        index = 0; //���� �ε��� 0���� ����
        
        interval = 1.0f / CPS; // ��¼ӵ�����
        //Debug.Log(interval);

        Invoke("Effecting", 1/CPS); 
    }
    private void Effecting()
    {
        if(msgtext.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgtext.text += targetMsg[index]; //���
        index++; // ���� ���� ��� ���� �ε��� �� �ø���
        Invoke("Effecting", interval); //���� �� �������� ���ȣ��
    }

    private void EffectEnd()
    {
        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            SoundManager.instance.Stop(); // Ŭ����������� ������� ȿ���� ������Ŵ
        }
        
    }
}
