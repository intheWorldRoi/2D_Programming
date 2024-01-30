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

    bool isGrounded; // ���� ����ִ°�
    bool facingLeft; // ������ �����ִ°�
    bool unbeatable; // �����ΰ�

    float fireReady; // �Ѿ� �߻� ���ð�

    Rigidbody2D rb;
    Vector2 moveVelocity;
    Animator anim;

    
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0; // ���� �ι� ����
        isGrounded = true; // ���� ���� ���·� �����ϹǷ� true��
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
        moveInput = Input.GetAxisRaw("Horizontal"); // horizontal �Է� �ޱ�
        if(moveInput != 0)
        {
            anim.SetBool("isMoving", true); // �̵� �ִϸ��̼� on
        }
        else
        {
            anim.SetBool("isMoving", false); // �̵� �ִϸ��̼� off
        }
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); // �̵�

        
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // ���� ���� ���¿��� �����̽��ٸ� ������
        {

            anim.SetTrigger("Jump"); // ���� �ִϸ��̼� on
            anim.SetBool("isGrounded", false); 
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // ���� �������� �� ���ϱ�
            jumpCount = 1; // ���� ī��Ʈ ����
            isGrounded = false; // ���� ���� ����
        }
        else if(Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            //rb.freezeRotation = false;
            anim.SetTrigger("Jump"); // ���� �ִϸ��̼� on
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // ���� �������� �� �ѹ� �� ���ϱ�
            
            jumpCount = 2; // ���� ī��Ʈ ����
        }
    }

    public void facing()
    {
        
        if((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !facingLeft ) // �������� �ٶ� �� ���� Ű�� ������
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y); // ������Ʈ ����
            facingLeft = true; // ������ ��������
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))&& facingLeft){ // ������ �ٶ� �� �������� ������ 
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y); // ������Ʈ ����
            facingLeft = false; // �������� ��������
        }
    }

    public void Fire()
    {
        if(SceneManager.GetActiveScene().name == "Scene1" || SceneManager.GetActiveScene().name == "Scene2")
        {
            return; // ���۾��� �������� ���� ���� ���� �Ұ�
        }
        fireReady += Time.deltaTime; // ���� Ÿ�̸� �۵�
        if (fireReady < 0.1f) // 0.1�ʰ� �Ǳ����� return 
        {
            return;
        }

        if (Input.GetButton("Fire1")) // 0.1�ʰ� �Ǹ� fire1 ��ư�� ������ �ִ��� �˻�
        {
            GameObject b = Instantiate(bullet); // �Ѿ� ����
            b.transform.position = transform.GetChild(2).GetChild(0).transform.position; // �̸� �����ص� �ѱ� ��ġ
            if (facingLeft)
            {
                
                b.GetComponent<bullet>().Vector = 1; // ���������� ���� �������� ���� ������
            }
            else
            {
                
                b.GetComponent<bullet>().Vector = 2;
            }
            fireReady = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }


    public void bowingDown()
    {
        if (SceneManager.GetActiveScene().name == "Scene1")
        {
            return;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // �Ʒ� Ű�� ������
        {
            if (facingLeft) // ������ �������� ��
            {
                rb.SetRotation(90); // ������ ���帰��
                if (isGrounded)
                {
                    rb.MovePosition(new Vector2(rb.position.x, -3.75f)); // ���� ������� �� ���帮�� ���ִµ��� ������ ���̸� �����ش�.
                }
            }
            else
            {
                rb.SetRotation(-90); // ������ ���帰��.
                if (isGrounded)
                {
                    rb.MovePosition(new Vector2(rb.position.x, -3.75f));// ���� ������� �� ���帮�� ���ִµ��� ������ ���̸� �����ش�.
                }
            }
        }
        else
        {
            rb.SetRotation(0); // Ű ������ ������ ���������� ���ִ� ���·� ���ƿ�
        }
    }

    public void invincible() // �������� ����
    {
        unbeatable = true; // ���� ����ġ on
        StartCoroutine(blink()); // ĳ���� �������� �ִϸ��̼� �ڷ�ƾ ����
    }

    public bool isUnBeatable()
    {
        return unbeatable; // ������(����)�� ĳ���Ͱ� ������������ Ȯ���ϱ� ���� ���� ����ġ�� ��ȯ�ϴ� �Լ�
    }

    IEnumerator blink()
    {
        yield return null;
        Color c = gameObject.GetComponent<SpriteRenderer>().color; // �÷��̾� ��������Ʈ ������ �޾ƿ�
        for(int i =0; i < 4; i++)
        {
            for (float j = 1; j > 0.6f; j -= 0.1f)
            {
                c.a = j;
                gameObject.GetComponent<SpriteRenderer>().color = c; // �����������ٰ�
                yield return new WaitForSeconds(0.12f);
            }
            for(float f = 0.6f; f < 1f; f += 0.1f)
            {
                c.a = f;
                gameObject.GetComponent<SpriteRenderer>().color = c; // �����������ٰ� �� 4�� �ݺ�
                yield return new WaitForSeconds(0.12f);
            }
        }
        unbeatable = false; // ���� ��
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.layer == 6) // ���� ������
        {
            
            jumpCount = 0; // ���� ���� �ʱ�ȭ
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6) // ���� ������
        {
            rb.MoveRotation(0); // ���ֱ�
            rb.freezeRotation = true;  // �˹� ����
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("exitȣ��");
            rb.freezeRotation = false; // bowingDown()�� ���ع��� �ʵ��� ����
        }
    }

    
}
