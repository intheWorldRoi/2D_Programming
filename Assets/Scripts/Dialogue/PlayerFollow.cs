using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFollow : MonoBehaviour
{
    public GameObject Target; // ����ٴ� Ÿ�� ����
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Camera.main.WorldToScreenPoint(Target.transform.position // Ÿ�� ����ٴϱ�(ui��)
            + new Vector3(1.4f,1.1f,0));
    }
}
