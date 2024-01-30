using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DesignDirector : MonoBehaviour, DirectorInterface
{
    public GameObject Enemy;


    public TypeEffect EnemyEffect;
    public TypeEffect PlayerEffect;

    Queue<string> sentences = new Queue<string>();

    public GameObject EnemyTextBox;
    public GameObject PlayerTextBox;
    public Image img;

    public string[] StartDialogueByEnemy;

    public bool clear;
    public bool EndingSwitch;

    public int lifes;
    public GameObject[] hearts;
    public string[] Dead;
    public string[] GameOver;
    public string[] PlayerLast;

    public AudioSource backMusic;

    // Start is called before the first frame update
    void Start()
    {
        Begin(StartDialogueByEnemy, EnemyEffect); //디자인 스테이지 인트로 시작
        Invoke("EnemyComponentOn", 20f); // 인트로가 끝나면 공격이 시작됨
        lifes = 3; // 생명설정

    }

    
  
    public void Begin(string[] info, TypeEffect effect)
    {

        sentences.Clear(); // 이전 다이얼로그 데이터 삭제

        foreach (var sentence in info)
        {
            sentences.Enqueue(sentence); // string queue에 대화데이터 넣기
        }
        if (effect.gameObject.transform.parent.gameObject.activeSelf == false)
        {
            effect.gameObject.transform.parent.gameObject.SetActive(true); //텍스트박스가 꺼져있으면 킨다.
        }
        Next(effect);

    }

    public void Next(TypeEffect effect)
    {

        if (sentences.Count == 0)
        {
            
            if (effect.name == "PlayerText" && effect.gameObject.activeSelf == true)
            {

                Invoke("BoxFalsePlayer", 1f); // 다이얼로그 데이터를 다 출력했으면 텍스트박스 끄기
            }
            else if (effect.name == "EnemyText" && effect.gameObject.activeSelf == true)
            {
                Invoke("BoxFalseEnemy", 1f);
                
            }


            End();
            
            return;
        }
        if (sentences.Peek().Contains("진짜 졸업인거야?")) // 연출 분기를 나누기 위해서 좀 조잡하지만 출력하는 string을 검사해서 엔딩 연출 시작
        {
            Invoke("realEnding", 7f); //7초뒤 다른 씬 부르기
            EndingSwitch = true;
            StartCoroutine(fadeOut()); // 다른 씬 부르기 전에 fadeOut 연출
        }
        else if (sentences.Peek().Contains("치킨집"))
        {
            
            Invoke("gameOver", 5f);

        }
        effect.SetMsg(sentences.Dequeue()); // 텍스트 한글자씩 입력
        StartCoroutine(Dialogue(effect)); 
    }

    public void End()
    {
        
    }

    public void realEnding()
    {
        if (clear && EndingSwitch)
        {
            SceneManager.LoadScene(4); 
        }
    }

    public void BoxFalsePlayer()
    {
        PlayerTextBox.SetActive(false); //텍스트박스끄기
    }

    public void BoxFalseEnemy()
    {
        EnemyTextBox.SetActive(false); //텍스트박스끄기22
    }
    IEnumerator Dialogue(TypeEffect effect)
    {
        yield return new WaitForSeconds(3f);
        Next(effect); //대사 자동넘기기
    }

    void EnemyComponentOn()
    {
        Enemy.GetComponent<DesignEnemy>().enabled = true; //공격시작
    }

    public void clearDialogue()
    {
        if (!clear)
        {
            StopCoroutine(Dialogue(PlayerEffect));
            Begin(Dead, EnemyEffect);//디자인 클리어 대화 시작
            StartCoroutine(goEnding());
        }

    }

    public void attacked()
    {
        hearts[lifes-- - 1].SetActive(false); // 목숨 줄이고 하트 게임오브젝트도 끄기
        GetComponent<CameraShake>().Shake(); // 카메라 지진연출

        if (lifes <= 0)
        {
            StopCoroutine(Dialogue(PlayerEffect));
            StopCoroutine(Dialogue(EnemyEffect));
            Begin(GameOver, PlayerEffect);
            Enemy.GetComponent<DesignEnemy>().enabled = false;
        }

    }

    public void gameOver()
    {
        SceneManager.LoadScene(5);
    }

    IEnumerator goEnding()
    {
        for(float i =1f; i >-0.2f; i-= 0.1f)
        {
            backMusic.volume = i; //볼륨 서서히 줄이기
            yield return new WaitForSeconds(0.2f);
        }
        
        yield return new WaitForSeconds(17f);
        Begin(PlayerLast, PlayerEffect); //플레이어 독백 시작
        yield return new WaitForSeconds(0.3f);
        

    }

    IEnumerator fadeOut() //말그대로 페이드아웃 효과
    {
        yield return new WaitForSeconds(3f);
        for(float i = 0; i <1.2f; i += 0.1f)
        {
            Color c = new Color(0, 0, 0, i);
            img.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;

    }
}
