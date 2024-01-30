using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    float height;
    bool reachedTop;
    // Start is called before the first frame update
    void Start()
    {
        height = 0;
        Destroy(gameObject, 3f);    
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(branchAnim());
        if(gameObject.transform.localScale.y > 1f)
        {
            reachedTop = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            Debug.Log("아악! C학점 가지에 맞았다.");
        }
    }

    IEnumerator branchAnim ()
    {
        while (gameObject.transform.localScale.y < 0.5f && !reachedTop)
        {
            yield return new WaitForSeconds(0.2f);
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,
                height, 1);
            height += 0.01f;
        }
        
        while( gameObject.transform.localScale.y > 0f && reachedTop)
        {
            yield return new WaitForSeconds(0.25f);
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,
                height, 1);
            height -= 0.01f;
        }
    }
}
