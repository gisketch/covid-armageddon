using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    Animator animator;
    public GameObject grassParticles;
    
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            animator.SetTrigger("Touch");
        }

        if(col.CompareTag("MeleeHitbox"))
        {
            Instantiate(grassParticles, transform.position, transform.rotation).transform.localScale = Vector3.one * 1.3f;
            FindObjectOfType<AudioManager>().PlayOneShotRandom("grasscut");
            Destroy(gameObject);
        }
    }

}
