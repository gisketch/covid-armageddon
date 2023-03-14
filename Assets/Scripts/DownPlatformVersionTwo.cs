using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPlatformVersionTwo : MonoBehaviour
{
    BoxCollider2D col;

    public bool isPlayerTouching;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();    
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerTouching = true;
        }
    }

      void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerTouching = false;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if(isPlayerTouching)
        {
            if(Input.GetKeyDown(KeyCode.S) || (Input.GetKeyDown(KeyCode.DownArrow)))
            {
                Physics2D.IgnoreCollision(col, FindObjectOfType<PlayerController>().gameObject.GetComponent<CapsuleCollider2D>());
                Invoke("ReEnableCol", 1f);
            }
        }
    }

    
    void ReEnableCol()
    {
        
        Physics2D.IgnoreCollision(col, FindObjectOfType<PlayerController>().gameObject.GetComponent<CapsuleCollider2D>(), false);
    }
}
