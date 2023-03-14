using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp;
    public Rigidbody2D rb;
    public GameObject blobBlood;
    public GameObject dieFx;
    float flameTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(hp <= 0)
        {
            FindObjectOfType<AudioManager>().PlayOneShotRandom("enemydie");
            Instantiate(dieFx, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }        

        if(flameTimer > 0)
        {
            flameTimer -= Time.deltaTime;
        }
    }

    void Knockback()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if(this.transform.position.x > player.transform.position.x)
        {
            Debug.Log("Knocked Back!!");
            rb.velocity = new Vector2(15,0);
        } else
        if(this.transform.position.x <= player.transform.position.x)
        {
            rb.velocity = new Vector2(-15,0);
        }
    }

    void Hurt(int dmg)
    {
        FindObjectOfType<CameraShaker>().ShakeScreen(false, 0.2f);
        FindObjectOfType<AudioManager>().PlayOneShotRandom("enemyhit");
        Knockback();
        Instantiate(blobBlood, transform.position, transform.rotation);
        this.hp -= dmg;
    }

    
    void DpsHurt(int dmg)
    {
        FindObjectOfType<CameraShaker>().ShakeScreen(false, 0.2f);
        Knockback();
        Instantiate(blobBlood, transform.position, transform.rotation);
        this.hp -= dmg;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("MeleeHitbox"))
        {
            Hurt(25);
        }

        if(col.CompareTag("Bullet"))
        {
            Hurt(20);
        }

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Flamethrower"))
        {
            if(flameTimer <= 0)
            {
                flameTimer = 0.2f;
                Hurt(12);
            }
        }
    }
}
