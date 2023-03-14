using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{

    [SerializeField] int hp = 60;
    private Animator animator;
    private PlayerController player;
    public GameObject crateFragment;
    public Sprite crate2;
    public Sprite crate3;

    [Header("Droppable Loots")]
    public GameObject hpLoot;
    public GameObject armorLoot;
    public GameObject gunAmmoLoot;
    public GameObject flameAmmoLoot;


    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "MeleeHitbox")
        {
            Damage(20);
        }

        if(col.gameObject.tag == "Bullet")
        {
            Damage(15);
        }
    }

    void Damage(int dmg)
    {
        
        FindObjectOfType<AudioManager>().PlayOneShotRandom("cratehit");
        hp -= dmg;
        animator.SetTrigger("Damage");
        Instantiate(crateFragment, transform.position, transform.rotation);
    }

    void Update()
    {
        if(hp <= 0)
        {
            Break();
        }
        
        if(hp <= 40 && hp > 20)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = crate2;
        } else
        if(hp <= 20)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = crate3;
        }



    }

    void Break()
    {
        FindObjectOfType<AudioManager>().PlayOneShotRandom("cratebreak");

        

        if(player.hp <= 30)
        {
            
            float randomVal = Random.Range(0f,1f);
            if(randomVal < 0.3f) Instantiate(hpLoot, transform.position, Quaternion.identity);
        }

        if(player.armor <= 30)
        {
            float randomVal = Random.Range(0f,1f);
            if(randomVal < 0.3f) Instantiate(armorLoot, transform.position, Quaternion.identity);
        }

        if(player.gunAmmo < player.gunTotal-10)
        {
            if(player.hasGun)
            {
                float randomVal = Random.Range(0f,1f);
                if(randomVal < 0.15f) Instantiate(gunAmmoLoot, transform.position, Quaternion.identity);
            }
        }

        if(player.flameAmmo < player.flameTotal-10)
        {
            if(player.hasFlamethrower)
            {
                float randomVal = Random.Range(0f,1f);
                if(randomVal < 0.15f) Instantiate(flameAmmoLoot, transform.position, transform.rotation);
            }
        }

        Destroy(gameObject);
    }
}
