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

        Destroy(this.gameObject, destroyTime); // 파괴 대기시간 이후 파괴
        StartCoroutine(ballAttack()); // 애니메이션 연출
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator ballAttack()
    {
        yield return null;
        anim.SetBool("start", true); // 위로 뿅 하고 등장
        yield return new WaitForSeconds(1f);
        anim.SetBool("start", false);
        anim.SetBool("rotate", true); // 회전 애니메이션
        
    }
}
