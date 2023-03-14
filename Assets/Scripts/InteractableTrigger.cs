using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Events;

public class InteractableTrigger : MonoBehaviour
{
    [Header("Animator Controller")]
    public Animator animator;
    
    bool hasProgressed;
    bool isPlayerInRange;

    private BoxCollider2D boxCol;

    [Header("Settings")]
    public bool requiresProgress;
    public bool progressOnUse;
    public int progressRequirement;

    public bool activateOnTouch;
    public bool activated = false;

    public bool dialogue = true;

    [Header("Function To Run")]
    public UnityEvent eventsToRun;


    QuestLog questLog;
    
    void Start()
    {
        questLog = FindObjectOfType<QuestLog>();
        boxCol = this.GetComponent<BoxCollider2D>();
        animator = this.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(requiresProgress && progressRequirement != questLog.progress) return;

        if(col.CompareTag("Player"))
        {
            isPlayerInRange = true;
            animator.SetBool("inRange", true);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(requiresProgress && progressRequirement != questLog.progress) return;
        

        if(col.CompareTag("Player"))
        {
            animator.SetBool("inRange", isPlayerInRange);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(requiresProgress && progressRequirement != questLog.progress) return;

        if(col.CompareTag("Player"))
        {
            isPlayerInRange = false;
            animator.SetBool("inRange", false);
        }
    }

    void Update()
    {
        if(QuestManager.isActive || CutsceneManager.isActive || DialogueManager.isActive || PauseManager.isPaused || GameOverManager.isActive ) return;
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {   
            if(dialogue)
            {
                this.GetComponent<DialogueTrigger>().StartDialogue();
            }
            //PREVENT SPAMMING OF E
            if(progressOnUse)
            {
                questLog.FinishQuest();
                isPlayerInRange = false;
                animator.SetBool("inRange", false);
            }

            eventsToRun.Invoke();
        }

        if(isPlayerInRange && activateOnTouch && !activated)
        {
            activated = true;

            if(dialogue) this.GetComponent<DialogueTrigger>().StartDialogue();
            //PREVENT SPAMMING OF E
            if(progressOnUse)
            {
                questLog.FinishQuest();
                isPlayerInRange = false;
                animator.SetBool("inRange", false);
            }

            eventsToRun.Invoke();
        }

        //Enables Box Collider when progressIsFulfilled
        if(requiresProgress)
        {
            boxCol.enabled = progressRequirement == questLog.progress;
            if(isPlayerInRange)
            {
                animator.SetBool("inRange", progressRequirement == questLog.progress);
                isPlayerInRange = progressRequirement == questLog.progress;
            }
        }

        if(!isPlayerInRange) activated = false;
    }

    public void DestroySelf()
    {
        animator.SetBool("inRange", false);
        Destroy(this.gameObject, 0.5f);
    }
}
