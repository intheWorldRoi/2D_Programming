using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    Animator anim;
    Animator cannonAnim;
    public GameObject assignment;
    public GameObject BeamObject;
    public GameObject NullCircle;
    public GameObject Seed;

    public int HP;
    public Slider HPbar;

    public float taskCall;



    //스위치들
    bool SkillRainSwitch;
    bool NullAttackSwitch;
    bool SeedSwitch;


    bool RainAnim;
    bool ShootAnim;

    float SeedWait;
    float waiting;
    float NullWait;
    float RainWait;

    int skillnum;

    ProgrammingDirector Director;

    // Start is called before the first frame update
    void Start()
    {
        HPbar.maxValue = HP; // hp바 ui와 실제 체력 연결

        anim = GetComponent<Animator>(); //컴포넌트들 받아오기
        cannonAnim = GameObject.Find("Cannon").GetComponent<Animator>(); // 공격오브젝트 연결
        Director = GameObject.Find("Director").GetComponent<ProgrammingDirector>(); // 옵저버 연결
        
    }

    // Update is called once per frame
    void Update()
    {
        HPbar.value = HP;
        if (HP <= 0) // 피가 다 깎이면
        {
            Director.clearDialogue(); // 대사 출력
            Director.clear = true; 
            return; // 공격 중단
        }
         

        waiting += Time.deltaTime; // 타이머작동
 
        anim.SetBool("RainSkill", RainAnim); // 애니메이터 불을 프레임마다 설정하고 껐다켰다 스위치처럼 bool을 조작..
        cannonAnim.SetBool("ShootStart", ShootAnim);
        //Debug.Log(anim.GetBool("SowingSeeds"));

        //패턴 산출 시작
        if (waiting > 8f)
        {
            
            NullAttackSwitch = false; // 이전공격 중단
            skillnum =Random.Range(0,4); // 공격패턴 4가지 중 하나 산출
            switch (skillnum)
            {
                case 0:
                    RainAnim = true;
                    
                    SkillRainSwitch = true; // 애니메이터 bool 스위치 on
                    StartCoroutine(RainAnimation()); // 공격 코루틴 시작
                    
                    
                    break;
                case 1:
                    Beam();
                    Director.Begin(Director.beam, Director.EnemyEffect);
                    Invoke("BeamOff", 3.5f); // 3.5초뒤 빔 오브젝트 끄기
                    break;
                case 2:
                    Debug.Log("NullAttack()");
                    
                    StartCoroutine(ShootingAnimation());
                    break;
                case 3:
                    StartCoroutine(SeedAnimation());
                    
                    
                    break;
            }
            waiting = 0; // 타이머 초기화

        }
        
         assignmentRain(); // 얘네도 다 계속 호출하다가 스위치 키면 실제로 작동하는 방식
         NullAttack();
         BowingSeed();
    }


    void assignmentRain()
    {
        
        if (!SkillRainSwitch)   // 스위치가 켜져있지않으면 return함
        {
            return;
        }
        RainWait += Time.deltaTime; // 얼마나 오래 생성할것인가
        taskCall += Time.deltaTime; // 과제 생성 빈도수
        if (taskCall > 0.1f)
        {
            GameObject g = Instantiate(assignment); // 과제비 생성
            g.transform.position = new Vector3(Random.Range(-9, 9), Random.Range(5.5f, 10), 0); // 과제 위치 랜덤설정

            taskCall = 0;
        }
        if(RainWait > 5f) 
        {
            SkillRainSwitch = false; // 패턴 끝 설정 초기화
            RainWait = 0;
        }
        
        
        
        
    }

    void Beam()
    {
        BeamObject.SetActive(true); // 빔오브젝트 키기
    }

    void BeamOff()
    {
        BeamObject.SetActive(false); // 빔 오브젝트 끄기
    }

    void NullAttack()
    {
        if (!NullAttackSwitch) // 스위치가 켜져있지않으면 return함
        {
            return;
        }
        
        NullWait += Time.deltaTime; // 타이머 작동 
        if(NullWait > 1f) // 1초마다 발사
        {
             Debug.Log("Instantiate()");
             GameObject c = Instantiate(NullCircle); // 프리펩 생성
             c.transform.position = new Vector3(-3.42f, -2.62f, 0); // 위치는 대포위치
             NullWait = 0; // 타이머 초기화
        }
        
        
    }

    void BowingSeed()
    {
        if (!SeedSwitch) // 스위치가 켜져있지않으면 return함
        {
            return;
        }

        SeedWait += Time.deltaTime; // 타이머 시작
        if(SeedWait > 1) // 1초마다 투사체 발사
        {
            GameObject g = Instantiate(Seed); // 프리펩 생성
            g.transform.position = new Vector3(-5.4f, -1.66f, 0); // 위치는 입위치
            SeedWait = 0; // 타이머 초기화
        }
    }

    IEnumerator RainAnimation()
    {
        
        
        yield return new WaitForSeconds(1f);
        RainAnim = false; // 팔 만세 애니메이션 끄기
    }

    IEnumerator ShootingAnimation()
    {
        cannonAnim.SetBool("CannonAppear", true); // 대포 등장
        yield return new WaitForSeconds(1f);
        cannonAnim.SetBool("CannonAppear", false);
        yield return new WaitForSeconds(0.2f);
        NullAttackSwitch = true; // 실제로 투사체 발사는 이때부터 시작
        ShootAnim = true; // 대포 애니메이션도 시작
        yield return new WaitForSeconds(6f); // 6초동안 지속
        ShootAnim = false; // 애니메이션들 다 끄기
        NullAttackSwitch = false;

    }

    IEnumerator SeedAnimation()
    {
        waiting = 0;
        anim.SetBool("MouseFadein", true); // 씨앗 발사하는 입 등장
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("MouseFadein", false);
        yield return new WaitForSeconds(1f);
        SeedSwitch = true;
        yield return new WaitForSeconds(1f);
        anim.SetBool("SowingSeeds", true); // 씨앗 발사 애니메이션
        waiting = 0;
        yield return new WaitForSeconds(5f);
        waiting = 0;
        anim.SetBool("SowingSeeds", false);
        yield return new WaitForSeconds(1f);
        SeedSwitch = false;
        
    }
}
