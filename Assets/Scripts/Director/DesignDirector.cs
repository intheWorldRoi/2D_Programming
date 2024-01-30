using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DesignDirector : MonoBehaviour, DirectorInterface
{
    public GameObject Enemy;


    public TypeEffect EnemyEffect;
    public TypeEffect PlayerEffect;

    Queue<string> sentences = new Queue<string>();

    public GameObject EnemyTextBox;
    public GameObject PlayerTextBox;
    public Image img;

    public string[] StartDialogueByEnemy;

    public bool clear;
    public bool EndingSwitch;

    public int lifes;
    public GameObject[] hearts;
    public string[] Dead;
    public string[] GameOver;
    public string[] PlayerLast;

    public AudioSource backMusic;

    // Start is called before the first frame update
    void Start()
    {
        Begin(StartDialogueByEnemy, EnemyEffect); //������ �������� ��Ʈ�� ����
        Invoke("EnemyComponentOn", 20f); // ��Ʈ�ΰ� ������ ������ ���۵�
        lifes = 3; // ������

    }

    
  
    public void Begin(string[] info, TypeEffect effect)
    {

        sentences.Clear(); // ���� ���̾�α� ������ ����

        foreach (var sentence in info)
        {
            sentences.Enqueue(sentence); // string queue�� ��ȭ������ �ֱ�
        }
        if (effect.gameObject.transform.parent.gameObject.activeSelf == false)
        {
            effect.gameObject.transform.parent.gameObject.SetActive(true); //�ؽ�Ʈ�ڽ��� ���������� Ų��.
        }
        Next(effect);

    }

    public void Next(TypeEffect effect)
    {

        if (sentences.Count == 0)
        {
            
            if (effect.name == "PlayerText" && effect.gameObject.activeSelf == true)
            {

                Invoke("BoxFalsePlayer", 1f); // ���̾�α� �����͸� �� ��������� �ؽ�Ʈ�ڽ� ����
            }
            else if (effect.name == "EnemyText" && effect.gameObject.activeSelf == true)
            {
                Invoke("BoxFalseEnemy", 1f);
                
            }


            End();
            
            return;
        }
        if (sentences.Peek().Contains("��¥ �����ΰž�?")) // ���� �б⸦ ������ ���ؼ� �� ���������� ����ϴ� string�� �˻��ؼ� ���� ���� ����
        {
            Invoke("realEnding", 7f); //7�ʵ� �ٸ� �� �θ���
            EndingSwitch = true;
            StartCoroutine(fadeOut()); // �ٸ� �� �θ��� ���� fadeOut ����
        }
        else if (sentences.Peek().Contains("ġŲ��"))
        {
            
            Invoke("gameOver", 5f);

        }
        effect.SetMsg(sentences.Dequeue()); // �ؽ�Ʈ �ѱ��ھ� �Է�
        StartCoroutine(Dialogue(effect)); 
    }

    public void End()
    {
        
    }

    public void realEnding()
    {
        if (clear && EndingSwitch)
        {
            SceneManager.LoadScene(4); 
        }
    }

    public void BoxFalsePlayer()
    {
        PlayerTextBox.SetActive(false); //�ؽ�Ʈ�ڽ�����
    }

    public void BoxFalseEnemy()
    {
        EnemyTextBox.SetActive(false); //�ؽ�Ʈ�ڽ�����22
    }
    IEnumerator Dialogue(TypeEffect effect)
    {
        yield return new WaitForSeconds(3f);
        Next(effect); //��� �ڵ��ѱ��
    }

    void EnemyComponentOn()
    {
        Enemy.GetComponent<DesignEnemy>().enabled = true; //���ݽ���
    }

    public void clearDialogue()
    {
        if (!clear)
        {
            StopCoroutine(Dialogue(PlayerEffect));
            Begin(Dead, EnemyEffect);//������ Ŭ���� ��ȭ ����
            StartCoroutine(goEnding());
        }

    }

    public void attacked()
    {
        hearts[lifes-- - 1].SetActive(false); // ��� ���̰� ��Ʈ ���ӿ�����Ʈ�� ����
        GetComponent<CameraShake>().Shake(); // ī�޶� ��������

        if (lifes <= 0)
        {
            StopCoroutine(Dialogue(PlayerEffect));
            StopCoroutine(Dialogue(EnemyEffect));
            Begin(GameOver, PlayerEffect);
            Enemy.GetComponent<DesignEnemy>().enabled = false;
        }

    }

    public void gameOver()
    {
        SceneManager.LoadScene(5);
    }

    IEnumerator goEnding()
    {
        for(float i =1f; i >-0.2f; i-= 0.1f)
        {
            backMusic.volume = i; //���� ������ ���̱�
            yield return new WaitForSeconds(0.2f);
        }
        
        yield return new WaitForSeconds(17f);
        Begin(PlayerLast, PlayerEffect); //�÷��̾� ���� ����
        yield return new WaitForSeconds(0.3f);
        

    }

    IEnumerator fadeOut() //���״�� ���̵�ƿ� ȿ��
    {
        yield return new WaitForSeconds(3f);
        for(float i = 0; i <1.2f; i += 0.1f)
        {
            Color c = new Color(0, 0, 0, i);
            img.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;

    }
}
