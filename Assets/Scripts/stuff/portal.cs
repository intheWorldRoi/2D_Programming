using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    public string portalName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (portalName) // ��Ż �̸��� ���� ȣ���� �� �ٸ���(��ũ��Ʈ ��Ȱ�� ����)
        {
            case "Programming":
            if (Input.GetKeyDown(KeyCode.S))
            {
                    SceneManager.LoadScene(2);
            }
                break;
            case "Scene2":
                
                    SceneManager.LoadScene(1);
                
                break;
            case "Design":
                if (Input.GetKeyDown(KeyCode.S))
                {
                    SceneManager.LoadScene(3);
                }
                break;

        }
            
        
        
    }
}
