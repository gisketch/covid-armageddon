using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;


    [Header("Stats")]
    [Range(0,100)]
    public int hp = 100;
    [Range(0,100)]
    public int armor = 100;

    [Header("HP Panel")]    
    public Slider hpSlider;
    public Slider armorSlider;
    
    public GameObject facemask;


    [Header("Weapon")]
    public bool onNoWeapon;

    public bool hasMelee;
    public bool hasGun;
    public bool hasFlamethrower;

    public int gunAmmo;
    public int gunTotal;
    float gunTimer;
    public int flameAmmo;
    public int flameTotal;
    float flameTimer;

    public float meleeCd;
    public float meleeTimer;

    [Header("Aiming")]
    public bool isAiming;
    public GameObject aimingHands;
    public GameObject aimGun;
    public GameObject aimFlamethrower;
    public CircleCollider2D flameCollider;
    public GameObject bullet;
    public Transform bulletPos;

    [Header("InventoryUI")]
    public Animator meleeAnim;
    public Animator gunAnim;
    public Animator flameAnim;

    [Header("Ammo UI")]
    public TMP_Text ammoText;
    public TMP_Text ammoTotalText;
    public Animator ammoAnim;

    [Header("FX")]
    public GameObject muzzleFx;
    public ParticleSystem flamethrowerFx;
    public GameObject hpGainParticle;
    

    [Header("GoBack")]
    public float goBackCd;
    public float goBackTimer;
    bool goingBack;

    [Header("FlinchTimer")]
    public float flinchCd;
    public float flinchTimer;

    public GameObject damaged;

    bool flinching;

    public enum WeaponState
    {
        Idle,
        Melee,
        Gun,
        Flamethrower
    }
    
    public WeaponState weaponState;

    [Header("Weapon Sprites")]
    public GameObject melee;


    public bool grounded;

    [Header("COLLIDER FOR SLOPE")]
    public float slopeCheckDistance;
    private Vector2 colliderSize;
    public CapsuleCollider2D cc;
    public LayerMask whatIsGround;
    public LayerMask whatIsSlope;
    private float slopeDownAngle;
    private Vector2 slopeNormalPerp;
    float slopeDownAngleOld;
    public bool isOnSlope;



    void Start()
    {
        var em = flamethrowerFx.emission;
        em.enabled = false;

        colliderSize = cc.size;
    }

    void Update()
    {

        TimeController();

        if(CutsceneManager.isActive || DialogueManager.isActive || PauseManager.isPaused || GameOverManager.isActive)
        {
            //turn off animations //back2idle
            anim.SetBool("Running", false);
            return;
        };

        GoBackTimer();
        if(goingBack) return;

        FlinchTimer();
        if(flinching) return;

        Weaponizer();
        Melee();
        Gun();
        Flamethrower();
        AimBool();
        Aiming();
        Animate();
        HpUi();
        InventoryUi(); 
        AmmoUi();
        NoWeapon();
        Jump();
        SlopeCheck();
        Grounder();
    }

    void FixedUpdate()
    {   
        if(CutsceneManager.isActive || DialogueManager.isActive || PauseManager.isPaused || GameOverManager.isActive)
        {
            return;
        }

        
        Move();

    }

    void SlopeCheck()
    {
        Vector2 checkPos = cc.transform.position - new Vector3(0.0f, colliderSize.y / 2);

        SlopeCheckVertical(checkPos);
    }

    void SlopeCheckHorizontal(Vector2 checkPos)
    {

    }

    void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance * 1.7f, whatIsGround);

        
        isOnSlope = Physics2D.Raycast(cc.transform.position, Vector2.down, 6f, whatIsSlope);

        if(hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);

            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }

    public bool changeToSlowMo = false;

    void TimeController()
    {
        if(GameOverManager.isActive)
        {
            Time.timeScale = 0.12f;
        }
        else
        if(PauseManager.isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            if(!changeToSlowMo)
            {
                Time.timeScale = 1f;
            } else
            {
                Time.timeScale = 0.12f;
            }
        }
    }

    void Move()
    {
        //Movement script
        float horizontal = Input.GetAxis("Horizontal");

        
            transform.position = transform.position + new Vector3(horizontal * speed * Time.deltaTime, 0, 0);

        if(isOnSlope)
        {
            rb.velocity = new Vector3(speed * slopeNormalPerp.x * -horizontal, speed *slopeNormalPerp.y * -horizontal);

            grounded = true;
        }


        if(!isAiming)
        {
            if(horizontal > 0)
            {
                transform.localScale = new Vector3(1,transform.localScale.y,5);
            } else if(horizontal < 0)
            {
                transform.localScale = new Vector3(-1,transform.localScale.y,5);
            }
        }

        //ANIMATE
        
        if(horizontal != 0)//if rigidbody is moving
        {
            //Animate Running
            anim.SetBool("Running", true);
        } else
        {
            anim.SetBool("Running", false);
        }
    }

    void Jump()
    {
        //Jump script
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            grounded = false;
            //Set animation trigger
            anim.SetTrigger("Jump");
        }
    }

    void Weaponizer()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            weaponState = WeaponState.Idle;
            
        }

        if(onNoWeapon)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                GetComponent<DialogueTrigger>().StartDialogue();
            }
        }

        if(!onNoWeapon)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(hasMelee)
                {
                    weaponState = WeaponState.Melee;
                }
            }

            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                if(hasGun)
                {
                    weaponState = WeaponState.Gun;
                }
            }

            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                if(hasFlamethrower)
                {
                    weaponState = WeaponState.Flamethrower;
                }
            }
        }

        //Weapon Sprites
        melee.SetActive(weaponState == WeaponState.Melee);
    }

    void Melee()
    {

        if(weaponState == WeaponState.Melee)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && meleeTimer <= 0f)
            {
                meleeTimer = meleeCd;
                FindObjectOfType<AudioManager>().PlayOneShotRandom("slash");
                anim.SetTrigger("Melee");
            }
        }

        //MeleeCooldown
        if(meleeTimer > 0)
        {
            meleeTimer -= Time.deltaTime;
        }
    }

    void Gun()
    {

        if(weaponState == WeaponState.Gun)
        {
            var em = flamethrowerFx.emission;
            em.enabled = false;

            if(Input.GetKey(KeyCode.Mouse0))
            {
                if(gunTimer <= 0 && gunAmmo > 0)
                {
                    gunTimer = 0.2f;
                    //SHOOOOOT
                    FindObjectOfType<CameraShaker>().ShakeScreen(false, 0.08f);
                    Instantiate(bullet, bulletPos.position, bulletPos.rotation);
                    Instantiate(muzzleFx, bulletPos.position, bulletPos.rotation);
                    ammoAnim.SetTrigger("Decrease");
                    gunAmmo--;
                    FindObjectOfType<AudioManager>().Play("gunshot");
                }
            }

            if(gunTimer > 0)
            {
                gunTimer -= Time.deltaTime;
            }


        }
    }

    void Flamethrower()
    {
        if(weaponState == WeaponState.Flamethrower)
        {
            var em = flamethrowerFx.emission;
            //FLAMETHROOOWER
            if(Input.GetKey(KeyCode.Mouse0) && flameAmmo > 0)
            {   
                em.enabled = true;
                flameCollider.enabled = true;
                if(flameTimer <= 0 )
                {
                    flameTimer = 0.15f;
                    flameAmmo -= 1;
                    ammoAnim.SetTrigger("Decrease");
                }
            }

            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                em.enabled = false;
                flameCollider.enabled = false;
            }

            if(flameTimer > 0)
            {
                flameTimer -= Time.deltaTime;
            }

            if(flameAmmo <= 0)
            {
                flameCollider.enabled = false;
                em.enabled = false;
            }

        }
    }

    void AimBool()
    {
        if(weaponState == WeaponState.Gun || weaponState == WeaponState.Flamethrower)
        {
            isAiming = true;
        } else
        {
            isAiming = false;
        }
    }

    public Texture2D aimCursor;
    public Texture2D normalCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Aiming()
    {
            anim.SetBool("Aiming", isAiming);

            if(isAiming)
            {   
                Cursor.SetCursor(aimCursor, new Vector2(32,32), cursorMode);

                Vector3 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 curPos = transform.position;

                //FLIP ACCORDINGLY
                if(curPos.x <= curMousePos.x)
                {

                    Vector3 difference = curMousePos - curPos;

                    difference.Normalize();

                    transform.localScale = new Vector3(1,transform.localScale.y,5);
                    flamethrowerFx.transform.localScale = new Vector3(1, 1, 1);

                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    aimingHands.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(rotationZ,-55,55));

                } else
                {

                    Vector3 difference = curPos - curMousePos;

                    difference.Normalize();

                    transform.localScale = new Vector3(-1,transform.localScale.y,5);
                    flamethrowerFx.transform.localScale = new Vector3(-1, 1, 1);

                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    aimingHands.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(rotationZ,-55,55));
                }

                //SWITCH GUN FLAMTHROWER
                aimGun.SetActive(weaponState == WeaponState.Gun);
                aimFlamethrower.SetActive(weaponState == WeaponState.Flamethrower);

            } else
            {
                
                Cursor.SetCursor(normalCursor, hotSpot, cursorMode);
            }

            
            if(weaponState != WeaponState.Gun && weaponState != WeaponState.Flamethrower)
            {
                aimGun.SetActive(false);
                aimFlamethrower.SetActive(false);
            }

            
    }

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     //Ground Check
    //     if(col.gameObject.CompareTag("Ground"))
    //     {
    //         grounded = true;
    //     }
    // }

    void Grounder()
    {
        grounded = Physics2D.Raycast(cc.transform.position, Vector2.down, 8f, whatIsGround);


        Debug.DrawRay(cc.transform.position, Vector3.down, Color.cyan);
    }

    void Animate()
    {
        anim.SetBool("Grounded", grounded);
    }

    public void GoBack()
    {
        goBackTimer = goBackCd;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void GoBackTimer()
    {
        if(transform.localScale.x < 0) //If looking forward
        {
            if(goBackTimer > 0)
            {
                transform.position = transform.position + new Vector3(-1 * speed * Time.deltaTime, 0, 0);
                goBackTimer -= Time.deltaTime;
                goingBack = true;
            } else
            {
                goingBack = false;
            }
        }
        else
        if(transform.localScale.x > 0) //If looking backwards
        {
            if(goBackTimer > 0)
            {
                transform.position = transform.position + new Vector3(1 * speed * Time.deltaTime, 0, 0);
                goBackTimer -= Time.deltaTime;
                goingBack = true;
            } else
            {
                goingBack = false;
            }
        }
    }

    public void Flinch()
    {
        flinchTimer = flinchCd;
    }

    void FlinchTimer()
    {
        if(transform.localScale.x > 0) //If looking forward
        {
            if(flinchTimer > 0)
            {
                transform.position = transform.position + new Vector3(-1 * speed * Time.deltaTime, 0, 0);
                flinchTimer -= Time.deltaTime;
                flinching = true;
            } else
            {
                flinching = false;
            }
        }
        else
        if(transform.localScale.x < 0) //If looking backwards
        {
            if(flinchTimer > 0)
            {
                transform.position = transform.position + new Vector3(1 * speed * Time.deltaTime, 0, 0);
                flinchTimer -= Time.deltaTime;
                flinching = true;
            } else
            {
                flinching = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if(armor > 0) armor -= damage;
        if(armor <= 0) hp -= damage;

        Instantiate(damaged, transform.position, transform.rotation);

        FindObjectOfType<AudioManager>().PlayOneShot("hurt" + Random.Range(1,4).ToString());
        
        FindObjectOfType<CameraShaker>().ShakeScreen(true, 0.35f);

        anim.SetTrigger("Flinch");
        Flinch();

    }
    
    void HpUi()
    {
        float smoothing = 5f;

        Mathf.Clamp(hp, 0, 100);
        Mathf.Clamp(armor, 0, 100);

        if(hpSlider.value != hp)
        {
            hpSlider.value = Mathf.Lerp(hpSlider.value, hp, smoothing * Time.deltaTime);
        }
        if(armorSlider.value != armor)
        {
            armorSlider.value = Mathf.Lerp(armorSlider.value, armor, smoothing * Time.deltaTime);
        }

        if(hp > 100)
        {
            hp = 100;
        }

        if(armor > 100)
        {
            armor = 100;
        }

        facemask.SetActive(armor > 0);

        // if(Input.GetKeyDown(KeyCode.P)) TakeDamage(10);
    }

    void InventoryUi()
    {
        meleeAnim.gameObject.SetActive(hasMelee);
        gunAnim.gameObject.SetActive(hasGun);
        flameAnim.gameObject.SetActive(hasFlamethrower);

        meleeAnim.SetBool("isActive", weaponState == WeaponState.Melee);
        gunAnim.SetBool("isActive", weaponState == WeaponState.Gun);
        flameAnim.SetBool("isActive", weaponState == WeaponState.Flamethrower);


    }

    void AmmoUi()
    {

        Mathf.Clamp(gunAmmo, 0, gunTotal);
        Mathf.Clamp(flameAmmo, 0, flameTotal);

        if(gunAmmo >= gunTotal)
        {
            gunAmmo = gunTotal;
        }

        if(flameAmmo >= flameTotal)
        {
            flameAmmo = flameTotal;
        }

        if(weaponState == WeaponState.Gun)
        {   
            ammoAnim.gameObject.SetActive(true);
            ammoText.text = gunAmmo.ToString();
            ammoTotalText.text = gunTotal.ToString();
        } else
        if(weaponState == WeaponState.Flamethrower)
        {   
            ammoAnim.gameObject.SetActive(true);
            ammoText.text = flameAmmo.ToString();
            ammoTotalText.text = flameTotal.ToString();
        } else
        {   
            ammoAnim.gameObject.SetActive(false);
        }
    }

    Transform locToTp;

    public void TpHero(Transform newLoc)
    {
        
        locToTp = newLoc;
        Invoke("Tp", 0.1f);
        GameObject.Find("BlackTransition").GetComponent<Animator>().SetTrigger("FadeIn");


    }

    void Tp()
    {
        transform.position = locToTp.position;
    }

    void NoWeapon()
    {
        if(onNoWeapon) weaponState = WeaponState.Idle;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "NoWeapon")
        {
            onNoWeapon = true;
        }

        if(col.gameObject.tag == "Loot")
        {
            col.gameObject.GetComponent<LootController>().Gain();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "NoWeapon")
        {
            onNoWeapon = false;
        }
    }

    public void GainHp(int gainedHp)
    {
        hp += gainedHp;
        GameObject.Find("HPGain").GetComponent<Animator>().SetTrigger("Start");
        GameObject newParticle = Instantiate(hpGainParticle, transform.position, transform.rotation);
        newParticle.transform.SetParent(transform);
    }

    public void GainArmor(int gainedArmor)
    {
        armor += gainedArmor;
        GameObject.Find("ArmorGain").GetComponent<Animator>().SetTrigger("Start");
        GameObject newParticle = Instantiate(hpGainParticle, transform.position, transform.rotation);
        newParticle.transform.SetParent(transform);
    }

    

    public void GainAmmo(int gainedAmmo)
    {
        gunAmmo += gainedAmmo;
        GameObject.Find("AmmoGain").GetComponent<Animator>().SetTrigger("Start");
    }

    

    public void GainFlameAmmo(int gainedFlameAmmo)
    {
        flameAmmo += gainedFlameAmmo;
        GameObject.Find("AmmoGain").GetComponent<Animator>().SetTrigger("Start");
    }
}
