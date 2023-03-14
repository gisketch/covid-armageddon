using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHpBoss : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(PauseManager.isPaused || CutsceneManager.isActive || DialogueManager.isActive || GameOverManager.isActive)
        {
            animator.SetBool("showing", false);
        } else
        {
            animator.SetBool("showing", true);
        }
    }
}
