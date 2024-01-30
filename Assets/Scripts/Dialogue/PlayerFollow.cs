using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFollow : MonoBehaviour
{
    public GameObject Target; // µû¶ó´Ù´Ò Å¸°Ù ¼³Á¤
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Camera.main.WorldToScreenPoint(Target.transform.position // Å¸°Ù µû¶ó´Ù´Ï±â(ui¿ë)
            + new Vector3(1.4f,1.1f,0));
    }
}
