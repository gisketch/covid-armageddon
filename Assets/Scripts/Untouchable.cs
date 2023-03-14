using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Untouchable : MonoBehaviour
{
    public int damage; 

    public bool canDamage;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(canDamage)
            {
                canDamage = false;
                FindObjectOfType<PlayerController>().TakeDamage(damage);
            }
        }
    }

    float timer = 0f;

    void Update()
    {
        if(!canDamage)
        {
            if(timer <= 0f)
            {
                timer = 0.9f;
                canDamage = true;
            }        

            if(timer > 0f)
            {
                timer -= Time.deltaTime;
            }
        }
    }
}
