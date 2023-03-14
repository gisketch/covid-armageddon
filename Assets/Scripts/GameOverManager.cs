using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    PlayerController player;
    Animator animator;


    public static bool isActive;
    
    public bool hasOver = false;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();    
        animator = GetComponent<Animator>();
        hasOver = false;
    }

    void Update()
    {
        if(player.hp <= 0 && !hasOver)
        {
            hasOver = true;
            isActive = true;

            FindObjectOfType<AudioManager>().Play("gameover");
            //Stop BGM
            BgmManager[] Bgms = FindObjectsOfType<BgmManager>();
            foreach(BgmManager bgm in Bgms)
            {
                AudioSource[] audios = bgm.GetComponents<AudioSource>();
                foreach(AudioSource audio in audios)
                {
                    audio.Stop();
                }
            }

            animator.SetTrigger("GameOver");

            FindObjectOfType<PlayerController>().changeToSlowMo = true;
        } else
        {
            isActive = false;
        }

    }
}
