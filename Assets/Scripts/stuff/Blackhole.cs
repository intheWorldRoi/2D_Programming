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
        target = GameObject.Find("Player"); // �÷��̾� Ÿ�ټ���
        targetVec = target.transform.position; // �÷��̾� ��ġ�� ����
        velo = Vector2.zero;

        rb = target.GetComponent<Rigidbody2D>(); // ������Ʈ�� ���� ���ϴ� �Լ��� ������ٵ� �پ��־ ������ٵ� ������Ʈ �޾ƿ�
        destroyTime = Timer; //destroyTime�� �������� �ߴ��� �ν����Ϳ��� ������ �� �ż� �̷��� �ߴ�.
        Destroy(this.gameObject, destroyTime); // �������

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BringPlayer());
        if (Switch)
        {
            rb.AddForce(transform.position * power); //�������
        }
        
    }


    IEnumerator BringPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("start", false); // ���� �ִϸ��̼�
        yield return new WaitForSeconds(delayTime);
        anim.SetBool("absorbs", true); // ���Ƶ��̴� �ִϸ��̼�
        Switch = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
