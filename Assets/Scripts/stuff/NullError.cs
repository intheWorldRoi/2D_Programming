using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullErrorBall : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    float power;

    float x;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(30, 80);
        x = 0.1f;
        power = Random.Range(5, 15);
        rb.AddForce(new Vector2(1, 1) * power, ForceMode2D.Impulse);
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(x, 0) * speed * Time.deltaTime;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            rb.AddForce(Vector2.up * power, ForceMode2D.Impulse); ;
        }
        else if(collision.gameObject.layer == 11)
        {
            
            x = -x;

        }
        else if(collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
