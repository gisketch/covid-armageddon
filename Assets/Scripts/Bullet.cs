using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    private PlayerController player;
    public float bulletSpeed;
    private float flipper;
    public float damage;
    public LayerMask groundMask;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        flipper = player.transform.localScale.x;
        Destroy(gameObject, 1.1f);
    }
    // Update is called once per frame
    void Update()
    {   
        if(flipper > 0) { //If facing front
            transform.position += transform.right * Time.deltaTime * bulletSpeed;
        } else
        {
            transform.position -= transform.right * Time.deltaTime * bulletSpeed;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Ground Check
        if(col.gameObject.CompareTag("Ground"))
        {
            BulletGone();
        }
        
        if(col.gameObject.CompareTag("Enemy"))
        {
            BulletGone();
        }

        if(col.gameObject.CompareTag("Crate"))
        {
            BulletGone();
        }
    }

    public void BulletGone()
    {
        Destroy(this.gameObject);//TODO: change to destroy function with animation
    }
}
