using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesignEnemy : MonoBehaviour
{
    public float patternTime; // 패턴 산출 대기시간
    float Timer;

    bool patternSwitch; // 패턴 산출 스위치(왠지필요할거같아서 일단 선언)
    bool stopTimer;

    public GameObject target;
    //공격 stuff(stuff가 한국어로 뭔지 잘 모르겠음)
    public GameObject blackhole;
    public GameObject bowBall;
    public GameObject spear;
    public GameObject prison;

    public DesignDirector Director;
    static Vector2[] vectors = {new Vector2(1,0), new Vector2(0.88f, 3.25f).normalized, new Vector2(-1.61f, 3.53f).normalized, new Vector2(-1, 0), new Vector2(-1.77f, -0.9f).normalized,
    new Vector2(0,-1), new Vector2(2.19f, -0.66f).normalized};

    public int HP;
    public Slider HPbar;
    // Start is called before the first frame update
    void Start()
    {
        HPbar.maxValue = HP;
        stopTimer = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        HPbar.value = HP;
        if (HP <= 0) // 피가 다 닳으면 대화 출력함
        {
            Director.clearDialogue();
            Director.clear = true;            return;
        }
        

            
        Waiting();
        patternYield();
        
    }


    void Waiting() // 타이머 작동부분을 그냥 메서드로 만들어서 update()가 깔끔해졌다.
    {
        if (stopTimer) // 타이머 작동을 멈췄을 때는 return
        {
            return;
        }
        Timer += Time.deltaTime; //타이머작동
        if (Timer > patternTime) // 패턴 산출 대기시간이 넘으면
        {
            patternSwitch = true; // 패턴을 산출한다
        }
        
    }

    void patternYield()
    {
        if (!patternSwitch) // 패턴 산출 대기시간이 지나면 true가 됨, 그 전까진 return
        {
            return;
        }

        int skillnum = Random.Range(0,4); // 공격 패턴 랜덤설정
        //int skillnum = 3;
        switch (skillnum)
        {
            case 0:
                Debug.Log("블랙홀 산출");
                StartCoroutine(blackholeStart());          
                break;

            case 1:
                Debug.Log("Colorball 산출");
                StartCoroutine(ColorBallShoot());                
                break;

            case 2:
                Debug.Log("무한의 창");
                StartCoroutine(SpearAttack());               
                break;

            case 3:
                Debug.Log("감옥");
                StartCoroutine(Prison());                
                break;

        }
        patternSwitch = false; // 패턴 산출 종료
        stopTimer = true; // 공격 중 타이머 멈춤
        Timer = 0; // 타이머 초기화
    }


    IEnumerator blackholeStart()
    {
        yield return new WaitForSeconds(1f);
        GameObject b = Instantiate(blackhole); // 블랙홀 생성
        b.transform.position = new Vector3(5.7f, 7f, 0); //위치는 오른쪽상단 고정

        yield return new WaitForSeconds(Blackhole.destroyTime); // 블랙홀이 파괴될때까지 기다렸다가
        stopTimer = false; // 다시 타이머 작동
        
    }


    IEnumerator ColorBallShoot()
    {
        GameObject b = Instantiate(bowBall); // 무지개공생성
        b.transform.position = new Vector3(-0.09f, 1.71f, 0);
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < bowBall.GetComponent<RainbowBall>().balls.Length; i++) // 
        {
            GameObject g =Instantiate(bowBall.GetComponent<RainbowBall>().balls[i]); // 컬러볼 생성
            g.transform.position = bowBall.transform.position; // 무지개볼 위치로 설정
            g.GetComponent<Colorball>().vec = vectors[i]; // 발사될 벡터 여기서 정해줌
            yield return new WaitForSeconds(0.5f);
        }

        stopTimer = false; // 타이머 다시 작동

    }

    IEnumerator SpearAttack()
    {
        for(int i =0; i < 4; i++)
        {
            GameObject b = Instantiate(spear); // 창 생성
            b.transform.position = new Vector3(Random.Range(-7.7f, 8f), Random.Range(-4, 2.6f)); // 생성 위치 랜덤설정
            Vector3 vec = target.transform.position - b.transform.position; // 이동 벡터 계산(게임오브젝트 -> 타겟(플레이어) 방향)
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg; // 이동 벡터 각도 계산
            b.transform.rotation = Quaternion.AngleAxis(angle - 40 + 360, Vector3.forward); // 이동하는 방향으로 오브젝트가 앞을 보도록 회전
            yield return new WaitForSeconds(2f);
        }

        stopTimer = false; // 다시 타이머 작동
        
    }

    IEnumerator Prison()
    {
        yield return null;
        GameObject b = Instantiate(prison); // 감옥 생성
        target.gameObject.GetComponent<PlayerController2>().DontJump = true; // 감옥이 생성되면 점프 불가(점프하면 자꾸 감옥 뚫고 나가서 버그생김)
        yield return new WaitForSeconds(3f);
        stopTimer = false; // 다시 타이머 작동
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "colorBall") // 디자인 에너미가 컬러볼에 닿으면 
        {
            if(collision.GetComponent<Colorball>().isMyWeapon == true) // 플레이어가 발사한 컬러볼인지 체크
            {
                Destroy(collision.gameObject); // 컬러볼이 사라진다
                HP -= 100; // 공격받는다
            }
        }
    }
}
