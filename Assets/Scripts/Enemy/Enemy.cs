using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    Animator anim;
    Animator cannonAnim;
    public GameObject assignment;
    public GameObject BeamObject;
    public GameObject NullCircle;
    public GameObject Seed;

    public int HP;
    public Slider HPbar;

    public float taskCall;



    //����ġ��
    bool SkillRainSwitch;
    bool NullAttackSwitch;
    bool SeedSwitch;


    bool RainAnim;
    bool ShootAnim;

    float SeedWait;
    float waiting;
    float NullWait;
    float RainWait;

    int skillnum;

    ProgrammingDirector Director;

    // Start is called before the first frame update
    void Start()
    {
        HPbar.maxValue = HP; // hp�� ui�� ���� ü�� ����

        anim = GetComponent<Animator>(); //������Ʈ�� �޾ƿ���
        cannonAnim = GameObject.Find("Cannon").GetComponent<Animator>(); // ���ݿ�����Ʈ ����
        Director = GameObject.Find("Director").GetComponent<ProgrammingDirector>(); // ������ ����
        
    }

    // Update is called once per frame
    void Update()
    {
        HPbar.value = HP;
        if (HP <= 0) // �ǰ� �� ���̸�
        {
            Director.clearDialogue(); // ��� ���
            Director.clear = true; 
            return; // ���� �ߴ�
        }
         

        waiting += Time.deltaTime; // Ÿ�̸��۵�
 
        anim.SetBool("RainSkill", RainAnim); // �ִϸ����� ���� �����Ӹ��� �����ϰ� �����״� ����ġó�� bool�� ����..
        cannonAnim.SetBool("ShootStart", ShootAnim);
        //Debug.Log(anim.GetBool("SowingSeeds"));

        //���� ���� ����
        if (waiting > 8f)
        {
            
            NullAttackSwitch = false; // �������� �ߴ�
            skillnum =Random.Range(0,4); // �������� 4���� �� �ϳ� ����
            switch (skillnum)
            {
                case 0:
                    RainAnim = true;
                    
                    SkillRainSwitch = true; // �ִϸ����� bool ����ġ on
                    StartCoroutine(RainAnimation()); // ���� �ڷ�ƾ ����
                    
                    
                    break;
                case 1:
                    Beam();
                    Director.Begin(Director.beam, Director.EnemyEffect);
                    Invoke("BeamOff", 3.5f); // 3.5�ʵ� �� ������Ʈ ����
                    break;
                case 2:
                    Debug.Log("NullAttack()");
                    
                    StartCoroutine(ShootingAnimation());
                    break;
                case 3:
                    StartCoroutine(SeedAnimation());
                    
                    
                    break;
            }
            waiting = 0; // Ÿ�̸� �ʱ�ȭ

        }
        
         assignmentRain(); // ��׵� �� ��� ȣ���ϴٰ� ����ġ Ű�� ������ �۵��ϴ� ���
         NullAttack();
         BowingSeed();
    }


    void assignmentRain()
    {
        
        if (!SkillRainSwitch)   // ����ġ�� �������������� return��
        {
            return;
        }
        RainWait += Time.deltaTime; // �󸶳� ���� �����Ұ��ΰ�
        taskCall += Time.deltaTime; // ���� ���� �󵵼�
        if (taskCall > 0.1f)
        {
            GameObject g = Instantiate(assignment); // ������ ����
            g.transform.position = new Vector3(Random.Range(-9, 9), Random.Range(5.5f, 10), 0); // ���� ��ġ ��������

            taskCall = 0;
        }
        if(RainWait > 5f) 
        {
            SkillRainSwitch = false; // ���� �� ���� �ʱ�ȭ
            RainWait = 0;
        }
        
        
        
        
    }

    void Beam()
    {
        BeamObject.SetActive(true); // ��������Ʈ Ű��
    }

    void BeamOff()
    {
        BeamObject.SetActive(false); // �� ������Ʈ ����
    }

    void NullAttack()
    {
        if (!NullAttackSwitch) // ����ġ�� �������������� return��
        {
            return;
        }
        
        NullWait += Time.deltaTime; // Ÿ�̸� �۵� 
        if(NullWait > 1f) // 1�ʸ��� �߻�
        {
             Debug.Log("Instantiate()");
             GameObject c = Instantiate(NullCircle); // ������ ����
             c.transform.position = new Vector3(-3.42f, -2.62f, 0); // ��ġ�� ������ġ
             NullWait = 0; // Ÿ�̸� �ʱ�ȭ
        }
        
        
    }

    void BowingSeed()
    {
        if (!SeedSwitch) // ����ġ�� �������������� return��
        {
            return;
        }

        SeedWait += Time.deltaTime; // Ÿ�̸� ����
        if(SeedWait > 1) // 1�ʸ��� ����ü �߻�
        {
            GameObject g = Instantiate(Seed); // ������ ����
            g.transform.position = new Vector3(-5.4f, -1.66f, 0); // ��ġ�� ����ġ
            SeedWait = 0; // Ÿ�̸� �ʱ�ȭ
        }
    }

    IEnumerator RainAnimation()
    {
        
        
        yield return new WaitForSeconds(1f);
        RainAnim = false; // �� ���� �ִϸ��̼� ����
    }

    IEnumerator ShootingAnimation()
    {
        cannonAnim.SetBool("CannonAppear", true); // ���� ����
        yield return new WaitForSeconds(1f);
        cannonAnim.SetBool("CannonAppear", false);
        yield return new WaitForSeconds(0.2f);
        NullAttackSwitch = true; // ������ ����ü �߻�� �̶����� ����
        ShootAnim = true; // ���� �ִϸ��̼ǵ� ����
        yield return new WaitForSeconds(6f); // 6�ʵ��� ����
        ShootAnim = false; // �ִϸ��̼ǵ� �� ����
        NullAttackSwitch = false;

    }

    IEnumerator SeedAnimation()
    {
        waiting = 0;
        anim.SetBool("MouseFadein", true); // ���� �߻��ϴ� �� ����
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("MouseFadein", false);
        yield return new WaitForSeconds(1f);
        SeedSwitch = true;
        yield return new WaitForSeconds(1f);
        anim.SetBool("SowingSeeds", true); // ���� �߻� �ִϸ��̼�
        waiting = 0;
        yield return new WaitForSeconds(5f);
        waiting = 0;
        anim.SetBool("SowingSeeds", false);
        yield return new WaitForSeconds(1f);
        SeedSwitch = false;
        
    }
}
