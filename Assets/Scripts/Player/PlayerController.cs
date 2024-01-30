using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, PlayerInterface
{
    public GameObject bullet;
    public float speed;
    public int jumpPower;

    float moveInput;
    int jumpCount;

    bool isGrounded; // 땅에 닿아있는가
    bool facingLeft; // 왼쪽을 보고있는가
    bool unbeatable; // 무적인가

    float fireReady; // 총알 발사 대기시간

    Rigidbody2D rb;
    Vector2 moveVelocity;
    Animator anim;

    
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0; // 점프 두번 제한
        isGrounded = true; // 땅에 닿은 상태로 시작하므로 true로
        unbeatable = false;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
       
    }

    public void Move()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); // horizontal 입력 받기
        if(moveInput != 0)
        {
            anim.SetBool("isMoving", true); // 이동 애니메이션 on
        }
        else
        {
            anim.SetBool("isMoving", false); // 이동 애니메이션 off
        }
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); // 이동

        
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // 땅에 닿은 상태에서 스페이스바를 누르면
        {

            anim.SetTrigger("Jump"); // 점프 애니메이션 on
            anim.SetBool("isGrounded", false); 
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // 위쪽 방향으로 힘 가하기
            jumpCount = 1; // 점프 카운트 증가
            isGrounded = false; // 땅에 닿지 않음
        }
        else if(Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            //rb.freezeRotation = false;
            anim.SetTrigger("Jump"); // 점프 애니메이션 on
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // 위쪽 방향으로 힘 한번 더 가하기
            
            jumpCount = 2; // 점프 카운트 증가
        }
    }

    public void facing()
    {
        
        if((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !facingLeft ) // 오른쪽을 바라볼 때 왼쪽 키를 누르면
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y); // 오브젝트 반전
            facingLeft = true; // 왼쪽을 보고있음
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))&& facingLeft){ // 왼쪽을 바라볼 때 오른쪽을 누르면 
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y); // 오브젝트 반전
            facingLeft = false; // 오른쪽을 보고있음
        }
    }

    public void Fire()
    {
        if(SceneManager.GetActiveScene().name == "Scene1" || SceneManager.GetActiveScene().name == "Scene2")
        {
            return; // 시작씬과 스테이지 선택 씬은 공격 불가
        }
        fireReady += Time.deltaTime; // 공격 타이머 작동
        if (fireReady < 0.1f) // 0.1초가 되기전엔 return 
        {
            return;
        }

        if (Input.GetButton("Fire1")) // 0.1초가 되면 fire1 버튼을 누르고 있는지 검사
        {
            GameObject b = Instantiate(bullet); // 총알 복제
            b.transform.position = transform.GetChild(2).GetChild(0).transform.position; // 미리 지정해둔 총구 위치
            if (facingLeft)
            {
                
                b.GetComponent<bullet>().Vector = 1; // 오른쪽으로 쏠지 왼쪽으로 쏠지 지정함
            }
            else
            {
                
                b.GetComponent<bullet>().Vector = 2;
            }
            fireReady = 0f; // 타이머 초기화
        }
    }


    public void bowingDown()
    {
        if (SceneManager.GetActiveScene().name == "Scene1")
        {
            return;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // 아래 키를 누르면
        {
            if (facingLeft) // 왼쪽을 보고있을 때
            {
                rb.SetRotation(90); // 앞으로 엎드린다
                if (isGrounded)
                {
                    rb.MovePosition(new Vector2(rb.position.x, -3.75f)); // 땅에 닿아있을 때 엎드리면 떠있는듯이 보여서 높이를 낮춰준다.
                }
            }
            else
            {
                rb.SetRotation(-90); // 앞으로 엎드린다.
                if (isGrounded)
                {
                    rb.MovePosition(new Vector2(rb.position.x, -3.75f));// 땅에 닿아있을 때 엎드리면 떠있는듯이 보여서 높이를 낮춰준다.
                }
            }
        }
        else
        {
            rb.SetRotation(0); // 키 누르지 않으면 정상적으로 서있는 상태로 돌아옴
        }
    }

    public void invincible() // 무적상태 돌입
    {
        unbeatable = true; // 무적 스위치 on
        StartCoroutine(blink()); // 캐릭터 깜빡깜빡 애니메이션 코루틴 실행
    }

    public bool isUnBeatable()
    {
        return unbeatable; // 옵저버(디렉터)가 캐릭터가 무적상태인지 확인하기 위해 무적 스위치를 반환하는 함수
    }

    IEnumerator blink()
    {
        yield return null;
        Color c = gameObject.GetComponent<SpriteRenderer>().color; // 플레이어 스프라이트 렌더러 받아옴
        for(int i =0; i < 4; i++)
        {
            for (float j = 1; j > 0.6f; j -= 0.1f)
            {
                c.a = j;
                gameObject.GetComponent<SpriteRenderer>().color = c; // 반투명해졌다가
                yield return new WaitForSeconds(0.12f);
            }
            for(float f = 0.6f; f < 1f; f += 0.1f)
            {
                c.a = f;
                gameObject.GetComponent<SpriteRenderer>().color = c; // 불투명해졌다가 를 4번 반복
                yield return new WaitForSeconds(0.12f);
            }
        }
        unbeatable = false; // 무적 끝
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.layer == 6) // 땅에 닿으면
        {
            
            jumpCount = 0; // 점프 설정 초기화
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6) // 땅에 닿으면
        {
            rb.MoveRotation(0); // 서있기
            rb.freezeRotation = true;  // 넉백 방지
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("exit호출");
            rb.freezeRotation = false; // bowingDown()이 방해받지 않도록 꺼줌
        }
    }

    
}
