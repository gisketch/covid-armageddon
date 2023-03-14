using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongMinion : MonoBehaviour
{
    public enum State {Searching,Breathing}
    public State state = State.Searching;
    private Animator animator;
    public LayerMask playerMask;
    public LayerMask flameMask;
    public Transform searchTrans;
    public Transform attackTrans;

    public bool hasClosedPlayer; 
    public float timer;
    public float lineOfSight;
    public float breathOfSight;
    public float speed;
    public int damage;
    public bool canDamage;
    public float dmgTimer;

    
    public ParticleSystem gasFx;

    void Start()
    {
        animator = GetComponent<Animator>();
        var em = gasFx.emission;
        em.enabled = false;
    }

    bool breathing = false;

    void Update()
    {

        var em = gasFx.emission;
        RaycastHit2D hasFoundPlayer;
        RaycastHit2D hasBreathedPlayer;
        RaycastHit2D hasTouchedFlame;

        if(transform.localScale.x < 0)
        {
            hasFoundPlayer = Physics2D.Raycast(searchTrans.position, Vector2.right, lineOfSight, playerMask);
            Debug.DrawRay(searchTrans.position, Vector2.right * lineOfSight, Color.green);
            hasBreathedPlayer = Physics2D.Raycast(attackTrans.position, Vector2.right, lineOfSight, playerMask);
            Debug.DrawRay(attackTrans.position, Vector2.right * breathOfSight, Color.red);
            hasTouchedFlame = Physics2D.Raycast(searchTrans.position, Vector2.right, lineOfSight, flameMask);
            gasFx.transform.localScale = new Vector3(1, 1, 1);
        } else
        {
            hasFoundPlayer = Physics2D.Raycast(searchTrans.position, Vector2.left, lineOfSight, playerMask);
            Debug.DrawRay(searchTrans.position, Vector2.left * lineOfSight, Color.green);
            hasBreathedPlayer = Physics2D.Raycast(attackTrans.position, Vector2.left, lineOfSight, playerMask);
            Debug.DrawRay(attackTrans.position, Vector2.left * breathOfSight, Color.red);
            hasTouchedFlame = Physics2D.Raycast(searchTrans.position, Vector2.right, lineOfSight, flameMask);
            gasFx.transform.localScale = new Vector3(-1, 1, 1);
        }



        if(state == State.Searching)
        {

            breathing = false;

            if(!DialogueManager.isActive)
            {
                em.enabled = false;

                if(hasFoundPlayer) 
                {
                    state = State.Breathing;
                    Invoke("GoBreathe", 0.4f);
                }
            }
                
            //
            if(timer <= 0f)
            {
                timer = Random.Range(1f,2.5f);
                speed *= -1;
                float newScale = transform.localScale.x * -1;
                transform.localScale = new Vector3(newScale, transform.localScale.y, transform.localScale.z);
            }

            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }

            transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if(state == State.Breathing)
        {
            

            if(breathing) 
            {
                if(!hasTouchedFlame)
                {
                    if(hasBreathedPlayer)
                    {
                        em.enabled = true;
                        if(canDamage)
                        {
                            canDamage = false;
                            FindObjectOfType<PlayerController>().TakeDamage(damage);
                        }
                    }
                } else
                {
                    em.enabled = false;
                }

                

                if(!canDamage)
                {
                    if(dmgTimer <= 0f)
                    {
                        dmgTimer = 0.25f;
                        canDamage = true;
                    }        

                    if(dmgTimer > 0f)
                    {
                        dmgTimer -= Time.deltaTime;
                    }
                }
            }
            if(!hasFoundPlayer)
            {
                state = State.Searching;
            }
        }

        Animate();
    }

    void GoBreathe()
    {
        breathing = true;
    }


    void Animate()
    {
        animator.SetBool("searching", state == State.Searching);
        animator.SetBool("breathing", state == State.Breathing);
    }
}
