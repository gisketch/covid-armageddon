using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Animator animator;
    public bool dieForever = true;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            animator.SetTrigger("Show");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            animator.SetTrigger("Hide");
            if(dieForever)
            {
                Invoke("DestroySelf", 1.1f);
            }
        }
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
