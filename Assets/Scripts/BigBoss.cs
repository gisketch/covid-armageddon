using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigBoss : MonoBehaviour
{
    public enum State{Idle, Jumping, Landing}

    public State state = State.Idle;
    public LayerMask playerMask;

    private Animator anim;
    
    public float playerLoc;

    public float jumpSpeed;

    public GameObject rockBreak;

    public Image hpFill;

    public int hp = 1000;
    public int maxHp = 1000;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        playerLoc = FindObjectOfType<PlayerController>().gameObject.transform.position.x;
    }

    bool readyToFlip = true;

    bool isFacingRight = false;

    bool hasProceeded = false;

    void Update()
    {
        hpFill.fillAmount = Mathf.Lerp(hpFill.fillAmount, (float)hp/maxHp, 5f * Time.deltaTime);
        if(hp < 250 && !hasProceeded)
        {
            hasProceeded = true;
            FindObjectOfType<PlayerController>().changeToSlowMo = true;
            Invoke("NextScene", 0.2f);
            
        }

        //FOR DEBUGGING

        if(Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<PlayerController>().changeToSlowMo = true;
            Invoke("NextScene", 0.2f);
        }
    }

    void NextScene()
    {
        FindObjectOfType<PlayerController>().changeToSlowMo = false;
        FindObjectOfType<LevelManager>().LoadScene(10);
    }

    void FixedUpdate()
    {

        if(DialogueManager.isActive || PauseManager.isPaused || GameOverManager.isActive || CutsceneManager.isActive) return;

        switch(state)
        {
            case State.Idle:
                //FIND player position
                playerLoc = FindObjectOfType<PlayerController>().gameObject.transform.position.x;

                if(!readyToFlip)
                {
                    if(transform.position.x > playerLoc)
                    {
                        transform.localScale = new Vector3(5.3f,5.3f,1);
                        isFacingRight = false;
                    }
                    if(transform.position.x < playerLoc)
                    {
                        transform.localScale = new Vector3(-5.3f,5.3f,1);
                        isFacingRight = true;
                    }
                }
                
                break;

            case State.Jumping:

                transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, playerLoc, jumpSpeed * Time.deltaTime), transform.position.y, transform.position.z);

                readyToFlip = true;
                
                break;

            case State.Landing:
                jumpSpeed = 0f;
                break;
        }

        Animate();
    }

    void SwitchToJumping()
    {
        if(!DialogueManager.isActive)
        {
            state = State.Jumping;
            jumpSpeed = (Mathf.Abs(transform.position.x - playerLoc)/1.5f);
        }

    }

    void SwitchToLanding()
    {
        state = State.Landing;
        FindObjectOfType<CameraShaker>().ShakeScreen(false, 0.69f);

        Instantiate(rockBreak, transform.position, Quaternion.identity);

        GameObject otherRock = Instantiate(rockBreak, transform.position, Quaternion.identity);
        otherRock.transform.localScale = new Vector3(-otherRock.transform.localScale.x, otherRock.transform.localScale.y, otherRock.transform.localScale.z);


    }

    public void SpawnRock()
    {
        
        FindObjectOfType<CameraShaker>().ShakeScreen(false, 0.69f);
        if(!isFacingRight)
        {
            Instantiate(rockBreak, transform.position, Quaternion.identity);
        } else
        {
            GameObject otherRock = Instantiate(rockBreak, transform.position, Quaternion.identity);
        otherRock.transform.localScale = new Vector3(-otherRock.transform.localScale.x, otherRock.transform.localScale.y, otherRock.transform.localScale.z);
        }

        
    }

    void SwitchToIdle()
    {
        state = State.Idle;
        Invoke("ReadyToFlip", 0.5f);
    }

    void ReadyToFlip()
    {
        readyToFlip = false;
    }

    void Animate()
    {
        anim.SetBool("idle", state == State.Idle);
        anim.SetBool("jumping", state == State.Jumping);
        anim.SetBool("landing", state == State.Landing);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "MeleeHitbox")
        {
            if(state == State.Idle)
            {
                TakeDamage(20);
            } else if(state == State.Landing)
            {
                DeflectHit();
            }
        }

        if(col.gameObject.tag == "Bullet")
        {
            if(state == State.Landing || state == State.Idle)
            {
                DeflectHit();
            }
        }
    }

    public Animator deflectAnimator;

    public GameObject hitFxDeflect;

    void DeflectHit()
    {
        deflectAnimator.SetTrigger("hit");
        Instantiate(hitFxDeflect, hitLoc.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().PlayOneShotRandom("force");
    }

    public Transform hitLoc;
    public GameObject hitFx;

    void TakeDamage(int dmg)
    {
        hp -= dmg;
        Instantiate(hitFx, hitLoc.position, Quaternion.identity);
        FindObjectOfType<CameraShaker>().ShakeScreen(false, 0.2f);
        FindObjectOfType<AudioManager>().PlayOneShotRandom("enemyhit");
    }

    public void PlayJumpSfx()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("jump");
    }

    public void PlayRockBreak()
    {
        FindObjectOfType<AudioManager>().PlayOneShotRandom("rock");
    }

}
