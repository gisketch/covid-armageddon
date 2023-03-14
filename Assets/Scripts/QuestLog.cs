using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public Quest[] quests;
    public int progress;
    public Animator animator;

    void Start()
    {
        StartQuest();
    }

    public void StartQuest()
    {
        animator.SetTrigger("Start");
        FindObjectOfType<QuestManager>().DisplayQuest(quests[progress]);
    }

    public void FinishQuest()
    {
        progress++;
        StartQuest();
        FindObjectOfType<AudioManager>().Play("positive");
    }

    void Update()
    {
        
    }

    //EVENTS
    public void ObtainMelee()
    {
        //TODO: Change this code into something that can be called
        FindObjectOfType<PlayerController>().hasMelee = true;
    }
    //EVENTS
    public void ObtainGun()
    {
        //TODO: Change this code into something that can be called
        FindObjectOfType<PlayerController>().hasGun = true;
    }
    //EVENTS
    public void ObtainFlame()
    {
        //TODO: Change this code into something that can be called
        FindObjectOfType<PlayerController>().hasFlamethrower = true;
    }
}

[System.Serializable]
public class Quest {
    public string questName;
    [TextArea]
    public string questDescription;
    public string questShortDesc;
}
