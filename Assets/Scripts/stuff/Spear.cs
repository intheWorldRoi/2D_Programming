using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public GameObject target;
    Vector2 v1;
    Vector2 v2;

    Vector2 velo;

    Vector2 vec;
    

    float Timer;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        v2 = target.transform.position;
        velo = Vector2.zero;
        Destroy(gameObject, 7f);
        
    }

    // Update is called once per frame
    void Update() 
    {
        Timer += Time.deltaTime;
        if(Timer < 1.5f)
        {
            
            transform.position = Vector2.SmoothDamp (transform.position, v2, ref velo,  0.4f);
            
        }
        else if(Timer > 3f && Timer < 3.1f)
        {
            Debug.Log("start");
            StartCoroutine(rotateRoutine());


        }
        if(Timer > 5f)
        {
            rotate();
            v2 = target.transform.position;

            Timer = 0;
            
        }


        
    }

    void rotate()
    {
        
        vec = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 40 + 360, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ºÎµúÈû");
    }

    IEnumerator rotateRoutine()
    {
        yield return null;
        float z = 0;
        float Timer = 0;
        vec = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        while (Timer < 2f)
        {
            
            Timer += Time.deltaTime;
            z += 1000 * Time.deltaTime;
            if (Timer > 1.8f)
            {

                rotate();
                
                
                

            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, z);
            }
            
            
            yield return null;
        }
        //transform.rotation = Quaternion.AngleAxis(360, Vector3.forward);
    }
}
