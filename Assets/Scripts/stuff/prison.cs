using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prison : MonoBehaviour
{

    
    GameObject target;
    Vector3 vec;
    BoxCollider2D[] boxCollider;
    int num;

    bool onSwitch;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player"); // 플레이어 타겟 설정
        boxCollider = new BoxCollider2D[3]; //플레이어와 닿기 전에는 비활성화였다가 닿으면 활성화되도록 미리 콜라이더 배열 생성
        boxCollider = GetComponents<BoxCollider2D>(); // 콜라이더 컴포넌트 받아와서 배열 안에 넣기
    }

    // Update is called once per frame
    void Update()
    {
        if (!onSwitch) // 플레이어와 닿기 전에
        {
            float x = target.transform.position.x; // 플레이어를 계속 추적해서 쫓아감
            vec = new Vector3(x, transform.position.y); // x값만 추적해서 쫓아감(y값은 리지드바디 영향으로 아래로 떨어짐)
            transform.position = vec;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                num++; // 감옥에 갇히면 키 연타해서 num 값을 올림
            }

        }

        if(num > 15) // 일정 횟수 이상 연타하면 
        {
            Destroy(gameObject); // 감옥 사라짐
            target.GetComponent<PlayerController2>().DontJump = false; // 점프 안되게 해놨던 거 취소
        }

        if(gameObject.transform.position.y < -5f) // 가끔 버그로 화면 아래로 떨어지면
        {
            Destroy(gameObject); // 그냥 오브젝트 파괴
            target.GetComponent<PlayerController2>().DontJump = false; // 점프 안되게 해놨던 거 취소
        }
        
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) // 플레이어와 닿으면
        {
            for(int i =0;i <3; i++)
            {
                boxCollider[i].enabled = true; // 콜라이더 활성화
                onSwitch = true; // 그때부터는 플레이어의 움직임을 추적하지 않음
            }
            transform.GetChild(0).gameObject.SetActive(true); // 연타 키 이미지 생성
            transform.GetChild(1).gameObject.SetActive(true); // 연타 키 이미지 생성
        }
    }
}
