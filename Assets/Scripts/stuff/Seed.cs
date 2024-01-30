using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    Rigidbody2D rb;
    float power;
    GameObject Enemy;
    public GameObject branch;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        power = Random.Range(1,9);
        rb.AddForce(new Vector2(1, 0.7f) * power , ForceMode2D.Impulse);
        Enemy = GameObject.Find("Enemy");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
            
            GameObject g = Instantiate(branch);

            g.transform.position = new Vector3(this.transform.position.x,
                this.transform.position.y -2, 1);
            
        }
    }
}
