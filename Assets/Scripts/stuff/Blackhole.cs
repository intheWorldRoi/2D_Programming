using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    GameObject target;

    Vector2 targetVec;

    Vector2 velo;

    Rigidbody2D rb;

    public float power;

    public float Timer;
    public static float destroyTime;
    public float delayTime;


    bool Switch;

    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player"); // 플레이어 타겟설정
        targetVec = target.transform.position; // 플레이어 위치값 저장
        velo = Vector2.zero;

        rb = target.GetComponent<Rigidbody2D>(); // 오브젝트에 힘을 가하는 함수가 리지드바디에 붙어있어서 리지드바디 컴포넌트 받아옴
        destroyTime = Timer; //destroyTime을 정적으로 했더니 인스펙터에서 지정이 안 돼서 이렇게 했다.
        Destroy(this.gameObject, destroyTime); // 사라지기

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BringPlayer());
        if (Switch)
        {
            rb.AddForce(transform.position * power); //끌어당기기
        }
        
    }


    IEnumerator BringPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("start", false); // 등장 애니메이션
        yield return new WaitForSeconds(delayTime);
        anim.SetBool("absorbs", true); // 빨아들이는 애니메이션
        Switch = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
