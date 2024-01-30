using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TypeEffect : MonoBehaviour
{
    string targetMsg;
    public int CPS; //CharPerSecond = 속도
    TextMeshProUGUI msgtext;
    int index;

    float interval;

   
    

    private void Awake()
    { 
        msgtext = GetComponent<TextMeshProUGUI>();
    }



    public void SetMsg(string msg)
    {
        msg = msg.Replace("\\n", "\n"); //줄바꾸기 인식
        targetMsg = msg; // 전체 한 문장 확인
        EffectStart();
        
    }

    private void EffectStart()
    {
        msgtext.text = "";
        index = 0; //글자 인덱스 0으로 시작
        
        interval = 1.0f / CPS; // 출력속도조절
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
        msgtext.text += targetMsg[index]; //출력
        index++; // 다음 글자 출력 위해 인덱스 값 올리기
        Invoke("Effecting", interval); //문장 안 끝났으면 재귀호출
    }

    private void EffectEnd()
    {
        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            SoundManager.instance.Stop(); // 클리어씬에서만 재생중인 효과음 정지시킴
        }
        
    }
}
