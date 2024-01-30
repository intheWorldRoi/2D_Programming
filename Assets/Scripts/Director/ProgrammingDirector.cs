using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgrammingDirector : MonoBehaviour, DirectorInterface
{

    public GameObject Enemy;


    public TypeEffect EnemyEffect;
    public TypeEffect PlayerEffect;

    Queue<string> sentences = new Queue<string>();

    public GameObject EnemyTextBox;
    public GameObject PlayerTextBox;

    public string[] rain;
    public string[] StartDialogueByEnemy;

    public string[] beam;

    public string[] seedPlayer;
    public string[] Dead;
    public string[] GameOver;


    public bool clear;
    public bool over;

    public int lifes;

    public GameObject[] hearts;
    // Start is called before the first frame update
    void Start()
    {
        Begin(StartDialogueByEnemy, EnemyEffect);
        Invoke("EnemyComponentOn", 15f);
        lifes = 3;
        
    }

    public void Begin(string[] info, TypeEffect effect)
    {
        
        sentences.Clear(); //디자인디렉터와 전반적으로 비슷함

        foreach (var sentence in info)
        {
            sentences.Enqueue(sentence);
        }
        if (effect.gameObject.transform.parent.gameObject.activeSelf == false)
        {
            effect.gameObject.transform.parent.gameObject.SetActive(true);
        }
        Next(effect);
        
    }

    public void Next(TypeEffect effect)
    {
        
        if (sentences.Count == 0)
        {
            if(effect.name== "PlayerText" && effect.gameObject.activeSelf == true)
            {
                
                Invoke("BoxFalsePlayer", 1f);
            }
            if(effect.name == "EnemyText" && effect.gameObject.activeSelf == true)
            {
                Invoke("BoxFalseEnemy", 1f);
;            }

            End();
            return;
        }

        if (sentences.Peek().Contains("그만하자.."))
        {
            over = true;
            Invoke("gameOver", 5f);
            
        }
        effect.SetMsg(sentences.Dequeue());
        if(!clear && !over) {
            StartCoroutine(Dialogue(effect));
        }
        else if (clear)
        {
            StartCoroutine(Dialogue(EnemyEffect));
        }
        else if (over)
        {
            StartCoroutine(Dialogue(PlayerEffect));
        }
        
    }

    public void End()
    {
        if (clear)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void BoxFalsePlayer()
    {
        PlayerTextBox.SetActive(false);
    }

    public void BoxFalseEnemy()
    {
        EnemyTextBox.SetActive(false);
    }
    IEnumerator Dialogue(TypeEffect effect)
    {
        yield return new WaitForSeconds(3f);
        Next(effect);   
    }

    void EnemyComponentOn()
    {
        Enemy.GetComponent<Enemy>().enabled = true;
    }

    public void clearDialogue()
    {
        if (!clear)
        {
            BoxFalsePlayer();
            BoxFalseEnemy();
            StopCoroutine(Dialogue(PlayerEffect));
            StopCoroutine(Dialogue(EnemyEffect));
            Begin(Dead, EnemyEffect);
        }
        
    }

    public void attacked()
    {
        hearts[lifes-- - 1].SetActive(false);
        GetComponent<CameraShake>().Shake();

        if(lifes <= 0)
        {
            BoxFalsePlayer();
            BoxFalseEnemy();
            StopCoroutine(Dialogue(PlayerEffect));
            StopCoroutine(Dialogue(EnemyEffect));
            Begin(GameOver, PlayerEffect);
            Enemy.GetComponent<Enemy>().enabled = false;
        }
        
    }

    public void gameOver()
    {
        SceneManager.LoadScene(5);
    }
}
