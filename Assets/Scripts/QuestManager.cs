using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public TMP_Text questName;
    public TMP_Text questShortDesc;

    public TMP_Text questNameHuge;
    public TMP_Text questDesc;

    public Animator animator;
    public static bool isActive = false;

    public void DisplayQuest(Quest quest)
    {
        questName.text = quest.questName;
        questShortDesc.text = quest.questShortDesc;
        questNameHuge.text = quest.questName;
        questDesc.text = quest.questDescription;
    }

    void Update()
    {
        if(CutsceneManager.isActive || DialogueManager.isActive || PauseManager.isPaused || GameOverManager.isActive) return;

        if(Input.GetKeyDown(KeyCode.Tab) && !isActive)
        {
            isActive = true;
            animator.SetBool("isActive", true);
            Debug.Log("Mission Active");
        }

        if(isActive && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            isActive = false;
            animator.SetBool("isActive", false);
            Debug.Log("Mission Inactive");
        }
    }

    public void QuestShow()
    {
        
            isActive = true;
            animator.SetBool("isActive", true);
            Debug.Log("Mission Active");
    }
}
