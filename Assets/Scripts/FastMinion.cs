using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMinion : MonoBehaviour
{
    public enum State {Searching,Running,Attacking}
    public State state = State.Searching;
    private Animator animator;
    public LayerMask playerMask;
    public Collider2D searchCol;
    public Transform searchTrans;
    public Collider2D attackCol;

    public bool hasClosedPlayer; 
    public float timer;
    public float lineOfSight;
    public float speed;
    public bool running;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        RaycastHit2D hasFoundPlayer;

        if(transform.localScale.x == - 1)
        {
            hasFoundPlayer = Physics2D.Raycast(searchTrans.position, Vector2.right, lineOfSight, playerMask);
            Debug.DrawRay(searchTrans.position, Vector2.right * lineOfSight, Color.green);
        } else
        {
            hasFoundPlayer = Physics2D.Raycast(searchTrans.position, Vector2.left, lineOfSight, playerMask);
            Debug.DrawRay(searchTrans.position, Vector2.left * lineOfSight, Color.green);
        }

        hasClosedPlayer = Physics2D.IsTouchingLayers(attackCol, playerMask);

        if(state == State.Searching)
        {
            if(!DialogueManager.isActive)
            {
                if(hasFoundPlayer) 
                {
                    state = State.Running;
                    Invoke("GoRun", 0.8f);
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

        if(state == State.Running)
        {
            if(hasClosedPlayer) 
            {
                timer = 0f;
                state = State.Attacking;
            }

            if(running) transform.position = transform.position + new Vector3(speed*2f * Time.deltaTime, 0, 0);

            if(!hasFoundPlayer)
            {
                state = State.Searching;
                running = false;
            }
        }

        if(state == State.Attacking)
        {
            if(hasClosedPlayer && hasFoundPlayer)
            {
                if(timer <= 0f)
                {
                    timer = 3f;
                    animator.SetTrigger("attack");
                }

                if(timer > 0)
                {
                    timer -= Time.deltaTime;
                }
            }
                

            if(!hasClosedPlayer && hasFoundPlayer)
            {
                state = State.Running;
                running = true;
            }

            if(!hasClosedPlayer && !hasFoundPlayer)
            {
                state = State.Searching;
                running = false;
            }
        }

        Animate();
    }

    void GoRun()
    {
        running = true;
    }

    void Animate()
    {
        animator.SetBool("searching", state == State.Searching);
        animator.SetBool("chasing", state == State.Running);
    }
}
