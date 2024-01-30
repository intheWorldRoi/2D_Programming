
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackable : MonoBehaviour
{
    public GameObject directorObject;
    DirectorInterface director; //���α׷��� ���� �����ξ� ��ο��� �� �� �ֵ��� �������̽��� ��ĳ���� �غ�


    // Start is called before the first frame update
    void Start()
    {
        
        directorObject = GameObject.Find("Director"); //������ ��������
        director = directorObject.GetComponent<DirectorInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) // �浹�� ���ӿ�����Ʈ�� �÷��̾��
        {
            if (collision.gameObject.GetComponent<PlayerInterface>().isUnBeatable()) // ���� ������������ üũ
            {
                return; 
            }
            collision.gameObject.GetComponent<PlayerInterface>().invincible(); // ���� ����(��������)����
            director.attacked();
            if (gameObject.layer != 16) // �浹�ص� ������� �ȵǴ� ����ü���� üũ
            {
                Destroy(gameObject); // �ƴ϶�� ����ü �����̹Ƿ� �������
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 3) // onTrigger�� ����
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
