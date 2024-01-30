using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesignEnemy : MonoBehaviour
{
    public float patternTime; // ���� ���� ���ð�
    float Timer;

    bool patternSwitch; // ���� ���� ����ġ(�����ʿ��ҰŰ��Ƽ� �ϴ� ����)
    bool stopTimer;

    public GameObject target;
    //���� stuff(stuff�� �ѱ���� ���� �� �𸣰���)
    public GameObject blackhole;
    public GameObject bowBall;
    public GameObject spear;
    public GameObject prison;

    public DesignDirector Director;
    static Vector2[] vectors = {new Vector2(1,0), new Vector2(0.88f, 3.25f).normalized, new Vector2(-1.61f, 3.53f).normalized, new Vector2(-1, 0), new Vector2(-1.77f, -0.9f).normalized,
    new Vector2(0,-1), new Vector2(2.19f, -0.66f).normalized};

    public int HP;
    public Slider HPbar;
    // Start is called before the first frame update
    void Start()
    {
        HPbar.maxValue = HP;
        stopTimer = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        HPbar.value = HP;
        if (HP <= 0) // �ǰ� �� ������ ��ȭ �����
        {
            Director.clearDialogue();
            Director.clear = true;            return;
        }
        

            
        Waiting();
        patternYield();
        
    }


    void Waiting() // Ÿ�̸� �۵��κ��� �׳� �޼���� ���� update()�� ���������.
    {
        if (stopTimer) // Ÿ�̸� �۵��� ������ ���� return
        {
            return;
        }
        Timer += Time.deltaTime; //Ÿ�̸��۵�
        if (Timer > patternTime) // ���� ���� ���ð��� ������
        {
            patternSwitch = true; // ������ �����Ѵ�
        }
        
    }

    void patternYield()
    {
        if (!patternSwitch) // ���� ���� ���ð��� ������ true�� ��, �� ������ return
        {
            return;
        }

        int skillnum = Random.Range(0,4); // ���� ���� ��������
        //int skillnum = 3;
        switch (skillnum)
        {
            case 0:
                Debug.Log("��Ȧ ����");
                StartCoroutine(blackholeStart());          
                break;

            case 1:
                Debug.Log("Colorball ����");
                StartCoroutine(ColorBallShoot());                
                break;

            case 2:
                Debug.Log("������ â");
                StartCoroutine(SpearAttack());               
                break;

            case 3:
                Debug.Log("����");
                StartCoroutine(Prison());                
                break;

        }
        patternSwitch = false; // ���� ���� ����
        stopTimer = true; // ���� �� Ÿ�̸� ����
        Timer = 0; // Ÿ�̸� �ʱ�ȭ
    }


    IEnumerator blackholeStart()
    {
        yield return new WaitForSeconds(1f);
        GameObject b = Instantiate(blackhole); // ��Ȧ ����
        b.transform.position = new Vector3(5.7f, 7f, 0); //��ġ�� �����ʻ�� ����

        yield return new WaitForSeconds(Blackhole.destroyTime); // ��Ȧ�� �ı��ɶ����� ��ٷȴٰ�
        stopTimer = false; // �ٽ� Ÿ�̸� �۵�
        
    }


    IEnumerator ColorBallShoot()
    {
        GameObject b = Instantiate(bowBall); // ������������
        b.transform.position = new Vector3(-0.09f, 1.71f, 0);
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < bowBall.GetComponent<RainbowBall>().balls.Length; i++) // 
        {
            GameObject g =Instantiate(bowBall.GetComponent<RainbowBall>().balls[i]); // �÷��� ����
            g.transform.position = bowBall.transform.position; // �������� ��ġ�� ����
            g.GetComponent<Colorball>().vec = vectors[i]; // �߻�� ���� ���⼭ ������
            yield return new WaitForSeconds(0.5f);
        }

        stopTimer = false; // Ÿ�̸� �ٽ� �۵�

    }

    IEnumerator SpearAttack()
    {
        for(int i =0; i < 4; i++)
        {
            GameObject b = Instantiate(spear); // â ����
            b.transform.position = new Vector3(Random.Range(-7.7f, 8f), Random.Range(-4, 2.6f)); // ���� ��ġ ��������
            Vector3 vec = target.transform.position - b.transform.position; // �̵� ���� ���(���ӿ�����Ʈ -> Ÿ��(�÷��̾�) ����)
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg; // �̵� ���� ���� ���
            b.transform.rotation = Quaternion.AngleAxis(angle - 40 + 360, Vector3.forward); // �̵��ϴ� �������� ������Ʈ�� ���� ������ ȸ��
            yield return new WaitForSeconds(2f);
        }

        stopTimer = false; // �ٽ� Ÿ�̸� �۵�
        
    }

    IEnumerator Prison()
    {
        yield return null;
        GameObject b = Instantiate(prison); // ���� ����
        target.gameObject.GetComponent<PlayerController2>().DontJump = true; // ������ �����Ǹ� ���� �Ұ�(�����ϸ� �ڲ� ���� �հ� ������ ���׻���)
        yield return new WaitForSeconds(3f);
        stopTimer = false; // �ٽ� Ÿ�̸� �۵�
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "colorBall") // ������ ���ʹ̰� �÷����� ������ 
        {
            if(collision.GetComponent<Colorball>().isMyWeapon == true) // �÷��̾ �߻��� �÷������� üũ
            {
                Destroy(collision.gameObject); // �÷����� �������
                HP -= 100; // ���ݹ޴´�
            }
        }
    }
}
