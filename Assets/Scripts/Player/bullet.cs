using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    
    public float speed;

    public int Vector;
    public GameObject fx;
    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector == 1)
        {
            leftshooting();
        }
        else if(Vector == 2)
        {
            Rightshooting();
        }
    }

    public void leftshooting()
    {
        transform.position -= new Vector3(1, 0) * speed * Time.deltaTime;
    }
    
    public void Rightshooting()
    {
        transform.position += new Vector3(1, 0) * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().HP -= 3;
            StartCoroutine(Attack(collision.gameObject));

            GameObject g = Instantiate(fx);
            g.transform.position = transform.position;
            Destroy(g, 0.5f);
            Destroy(gameObject);
        }
        
        
    }

    IEnumerator Attack(GameObject enemy)
    {
        enemy.GetComponent<Animator>().SetBool("Attacked", true);
        yield return new WaitForSeconds(0.2f);
        enemy.GetComponent<Animator>().SetBool("Attacked", false);
    }
}
