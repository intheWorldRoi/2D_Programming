
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackable : MonoBehaviour
{
    public GameObject directorObject;
    DirectorInterface director; //프로그래밍 씬과 디자인씬 모두에서 쓸 수 있도록 인터페이스로 업캐스팅 준비


    // Start is called before the first frame update
    void Start()
    {
        
        directorObject = GameObject.Find("Director"); //옵저버 직접연결
        director = directorObject.GetComponent<DirectorInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) // 충돌한 게임오브젝트가 플레이어면
        {
            if (collision.gameObject.GetComponent<PlayerInterface>().isUnBeatable()) // 현재 무적상태인지 체크
            {
                return; 
            }
            collision.gameObject.GetComponent<PlayerInterface>().invincible(); // 무적 연출(깜빡깜빡)시작
            director.attacked();
            if (gameObject.layer != 16) // 충돌해도 사라지면 안되는 공격체인지 체크
            {
                Destroy(gameObject); // 아니라면 투사체 개념이므로 사라지기
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 3) // onTrigger랑 같음
        {
            if (collision.gameObject.GetComponent<PlayerInterface>().isUnBeatable())
            {
                return;
            }
            collision.gameObject.GetComponent<PlayerInterface>().invincible();
            director.attacked();
            if(gameObject.layer != 16)
            {
                Destroy(gameObject);
            }
            
            
            
            
        }
    }

    
}
