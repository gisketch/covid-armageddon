using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockBreak : MonoBehaviour
{

    public int damage;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(SceneManager.GetActiveScene().buildIndex == 9)
            {
                if(FindObjectOfType<BigBoss>().hp <= 250)
                {
                    return;
                }
            }

            if(SceneManager.GetActiveScene().buildIndex == 11)
            {
                if(FindObjectOfType<BigBossTwo>().hp <= 0)
                {
                    return;
                }
            }
            FindObjectOfType<PlayerController>().TakeDamage(damage);
        }
    }


}
