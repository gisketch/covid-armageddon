using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitter : MonoBehaviour
{
    public int dmg;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().TakeDamage(dmg);
        }
    }
}
