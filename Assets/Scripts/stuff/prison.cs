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
        target = GameObject.Find("Player"); // �÷��̾� Ÿ�� ����
        boxCollider = new BoxCollider2D[3]; //�÷��̾�� ��� ������ ��Ȱ��ȭ���ٰ� ������ Ȱ��ȭ�ǵ��� �̸� �ݶ��̴� �迭 ����
        boxCollider = GetComponents<BoxCollider2D>(); // �ݶ��̴� ������Ʈ �޾ƿͼ� �迭 �ȿ� �ֱ�
    }

    // Update is called once per frame
    void Update()
    {
        if (!onSwitch) // �÷��̾�� ��� ����
        {
            float x = target.transform.position.x; // �÷��̾ ��� �����ؼ� �Ѿư�
            vec = new Vector3(x, transform.position.y); // x���� �����ؼ� �Ѿư�(y���� ������ٵ� �������� �Ʒ��� ������)
            transform.position = vec;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                num++; // ������ ������ Ű ��Ÿ�ؼ� num ���� �ø�
            }

        }

        if(num > 15) // ���� Ƚ�� �̻� ��Ÿ�ϸ� 
        {
            Destroy(gameObject); // ���� �����
            target.GetComponent<PlayerController2>().DontJump = false; // ���� �ȵǰ� �س��� �� ���
        }

        if(gameObject.transform.position.y < -5f) // ���� ���׷� ȭ�� �Ʒ��� ��������
        {
            Destroy(gameObject); // �׳� ������Ʈ �ı�
            target.GetComponent<PlayerController2>().DontJump = false; // ���� �ȵǰ� �س��� �� ���
        }
        
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) // �÷��̾�� ������
        {
            for(int i =0;i <3; i++)
            {
                boxCollider[i].enabled = true; // �ݶ��̴� Ȱ��ȭ
                onSwitch = true; // �׶����ʹ� �÷��̾��� �������� �������� ����
            }
            transform.GetChild(0).gameObject.SetActive(true); // ��Ÿ Ű �̹��� ����
            transform.GetChild(1).gameObject.SetActive(true); // ��Ÿ Ű �̹��� ����
        }
    }
}
