using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2 : MonoBehaviour
{

    public TypeEffect effect;
    Queue<string> sentences = new Queue<string>();

    public GameObject TextBox;
    public GameObject wall;

    
    public string[] script1;
    // Start is called before the first frame update
    void Start()
    {
        Begin(script1);
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
        if (sentences.Count == 0)
        {
            End();
            return;
        }
        
        effect.SetMsg(sentences.Dequeue());
    }

    public void End()
    {
        TextBox.SetActive(false);
        wall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
