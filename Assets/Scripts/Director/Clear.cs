using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Clear : MonoBehaviour
{
    public AudioClip clip;
    public GameObject TextDummys;
    public GameObject cText;
    [TextArea]
    public string[] EndingTxt;
    Queue<string> sentences = new Queue<string>();
    public TypeEffect text;
    // Start is called before the first frame update
    void Start()
    {
        Begin(EndingTxt);
    }



    public void Begin(string[] info)
    {
        
        sentences.Clear();

        foreach (var sentence in info)
        {
            sentences.Enqueue(sentence);
        }
        
        Next();

    }

    public void Next()
    {
        print("¿€µø");
        if (sentences.Count == 0)
        {
            
            End();
            return;
        }

       text.SetMsg(sentences.Dequeue());

        SoundManager.instance.Stop();
        SoundManager.instance.SFXPlay("typing", clip);
        
    }

    public void End()
    {
        TextDummys.SetActive(false);
        StartCoroutine(clear());
    }

    IEnumerator clear()
    {
        yield return null;
        Camera.main.GetComponent<Animator>().SetBool("Move", true);
        yield return new WaitForSeconds(3f);
        for(float i = 0f; i < 1.2f; i+= 0.1f)
        {
            cText.GetComponent<TextMeshProUGUI>().alpha = i;
            yield return new WaitForSeconds(0.2f);
        }
        
    }
}
