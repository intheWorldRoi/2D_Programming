using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour , PlayerInterface
{
    public float speed;
    public int jumpPower;

    float moveInput;
    int jumpCount;
    bool isGrounded;

    bool facingLeft;
    float fireReady;

    Rigidbody2D rb;
    Vector2 moveVelocity;

    Animator anim;

    public bool[] balls;
    public GameObject[] ballObjects;

    bool unbeatable;
    public bool DontJump;
    // Start is called before the first frame update
    void Start()
    {
        unbeatable = false;
        balls = new bool[7];
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0;
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        Rolling(); // 구르기 체킹
        
    }

    public void Rolling()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("버튼눌림인식완료");
           
            anim.SetTrigger("Rolling");
            
            
        }
    }
    public void Move()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    public void Jump()
    {
        if (DontJump)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            anim.SetTrigger("Jump");
            anim.SetBool("isGrounded", false);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount = 1;
            isGrounded = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            //rb.freezeRotation = false;
            anim.SetTrigger("Jump");
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            jumpCount = 2;
        }
    }

    public void facing()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !facingLeft)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
            facingLeft = true;
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && facingLeft)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
            facingLeft = false;
        }
    }

    public void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!ReadyToAttack())
            {
                return;
            }// 컬러볼(공격) 7가지 다 모았는지 체크

            StartCoroutine(ballAttack());
        }
        
        
    }



    bool ReadyToAttack()
    {
        for(int i = 0; i < balls.Length; i++)
        {
            if(balls[i] == false)
            {
                return false;
            }
        }

        return true;
    }

    public void bowingDown()
    {

    }

    public void invincible()
    {
        unbeatable = true;
        StartCoroutine(blink());
    }
    IEnumerator blink()
    {
        yield return null;
        Color c = gameObject.GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < 4; i++)
        {
            for (float j = 1; j > 0.6f; j -= 0.1f)
            {
                c.a = j;
                gameObject.GetComponent<SpriteRenderer>().color = c;
                yield return new WaitForSeconds(0.12f);
            }
            for (float f = 0.6f; f < 1f; f += 0.1f)
            {
                c.a = f;
                gameObject.GetComponent<SpriteRenderer>().color = c;
                yield return new WaitForSeconds(0.12f);
            }
        }
        unbeatable = false;
    }
    public bool isUnBeatable()
    {
        return unbeatable;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 6)
        {

            jumpCount = 0;
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            rb.MoveRotation(0);
            rb.freezeRotation = true;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("exit호출");
            rb.freezeRotation = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "colorBall")
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("인식은했음");
                int num = collision.gameObject.GetComponent<Colorball>().thiscolor;

                transform.GetChild(0).GetChild(num).gameObject.SetActive(true);
                balls[num] = true;

                Destroy(collision.gameObject);


            }
        }
    }

    IEnumerator ballAttack()
    {
        yield return null;
        for(int i = 0; i < balls.Length; i++)
        {
            balls[i] = false;
            transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            GameObject g = Instantiate(ballObjects[i]);
            g.transform.position = transform.position;
            yield return new WaitForSeconds(1f);
        }

        
    }
}
