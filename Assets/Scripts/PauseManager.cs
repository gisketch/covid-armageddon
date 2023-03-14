using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public Animator anim;
    public static bool isPaused = false;

    void Update()
    {
        if(CutsceneManager.isActive || DialogueManager.isActive) return;

        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            anim.SetTrigger("Pause");
            isPaused = true;
        }

    }

    public void Resume()
    {
        anim.SetTrigger("Resume");
        isPaused = false;
    }
}
