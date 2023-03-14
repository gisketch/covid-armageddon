using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEventScene2021 : MonoBehaviour
{

    public int progressRequirement;

    void Start()
    {

        QuestLog questLog = FindObjectOfType<QuestLog>();

        if(progressRequirement == questLog.progress)
        GetComponent<DialogueTrigger>().StartDialogue();
    }

    void Update()
    {
        
    }
}
