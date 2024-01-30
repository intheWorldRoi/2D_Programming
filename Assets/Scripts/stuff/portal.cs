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
        switch (portalName) // 포탈 이름에 따라서 호출할 씬 다르게(스크립트 재활용 목적)
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
