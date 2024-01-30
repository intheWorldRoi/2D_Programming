using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    Queue<string> sentences = new Queue<string>();
    public TypeEffect text;


    public string[] endingTxt;
    // Start is called before the first frame update
    void Start()
    {
        Begin(endingTxt);
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


    }

    public void End()
    {
    }


}
