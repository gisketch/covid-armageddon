using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{

    private Rigidbody2D rb;
    public LayerMask playerMask;
    private PlayerController player;
    public CircleCollider2D circleCollider2D;
    public bool isTouchingPlayer;
    public bool isTouchingPlayerTwo;
    public float speed;

    

    bool AbleToGet = false;

    public enum LootType {
        Health,
        Armor,
        GunAmmo,
        FlameAmmo
    }

    public LootType lootType; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-20,20), 60);
        Invoke("AbleToGetLoot", 0.4f);
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if(AbleToGet)
        {
            isTouchingPlayer = Physics2D.IsTouchingLayers(circleCollider2D, playerMask);

            isTouchingPlayerTwo = Physics2D.IsTouchingLayers(GetComponent<BoxCollider2D>(), playerMask);

            if(isTouchingPlayer)
            {
                rb.isKinematic = true;

                Vector2 newPos = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);

                GetComponent<BoxCollider2D>().isTrigger = true;

                rb.MovePosition(newPos);
            }

            if(isTouchingPlayerTwo)
            {
                Gain();
            }
        }
    }
    

    public void Gain()
    {
        
        if(!AbleToGet) return;
        
        switch(lootType)
        {
            case LootType.Health:
                player.GainHp(40);
                break;
            case LootType.Armor:
                player.GainArmor(100);
                break;
            case LootType.GunAmmo:
                player.GainAmmo(100);
                break;
            case LootType.FlameAmmo:
                player.GainFlameAmmo(100);
                break;
        }

        Destroy(gameObject);
    }

    public void AbleToGetLoot()
    {
        AbleToGet = true;
    }

}
