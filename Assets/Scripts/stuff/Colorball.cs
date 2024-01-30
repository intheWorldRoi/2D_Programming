using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorball : MonoBehaviour
{
    public Vector2 vec;
    Vector3 EnemyVec;
    public bool isCatched;
    public bool isMyWeapon;
    public float speed;

    static string[] color = { "red", "orange", "yellow", "green", "blue", "indigo", "purple" };

    public int thiscolor;

    
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name == "RedBall(Clone)")
        {
            thiscolor = 0;
        }
        else if(gameObject.name == "OrangeBall(Clone)")
        {
            thiscolor = 1;
        }
        else if (gameObject.name == "YellowBall(Clone)")
        {
            thiscolor = 2;
        }
        else if (gameObject.name == "GreenBall(Clone)")
        {
            thiscolor = 3;
        }
        else if (gameObject.name == "BlueBall(Clone)")
        {
            thiscolor = 4;
        }
        else if (gameObject.name == "IndigoBall(Clone)")
        {
            thiscolor = 5;
        }
        else if (gameObject.name == "PurpleBall(Clone)")
        {
            thiscolor = 6;
        }
        EnemyVec = GameObject.Find("DesignProfessor").transform.position.normalized;
        StartCoroutine(catchCheck());
        if(isCatched == false)
        {
            isCatched = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!isCatched)
        {
            transform.Translate(vec * 3 * Time.deltaTime);
        }
        else if (isCatched)
        {
            transform.Rotate(Vector3.back * speed * Time.deltaTime);
        }
        
        
        if(isMyWeapon)
        {   
            transform.Translate((EnemyVec - transform.position).normalized * 5 * Time.deltaTime);
        }
        
        
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    IEnumerator catchCheck()
    {
        yield return new WaitForSeconds(5f);
        if (!isCatched)
        {
            Destroy(gameObject);
        }
    }
}
