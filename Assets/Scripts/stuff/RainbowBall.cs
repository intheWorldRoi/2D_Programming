using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowBall : MonoBehaviour
{
    public GameObject[] balls;

    Animator anim;
    public static int destroyTime = 8;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        Destroy(this.gameObject, destroyTime); // �ı� ���ð� ���� �ı�
        StartCoroutine(ballAttack()); // �ִϸ��̼� ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator ballAttack()
    {
        yield return null;
        anim.SetBool("start", true); // ���� �� �ϰ� ����
        yield return new WaitForSeconds(1f);
        anim.SetBool("start", false);
        anim.SetBool("rotate", true); // ȸ�� �ִϸ��̼�
        
    }
}
