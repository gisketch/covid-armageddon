using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    public Animator animator;
    public PauseManager pauseManager;

    public Image cutsceneImage;
    public TMP_Text cutsceneText;


    Cutscene[] currentCutscenes;
    int activeCutscene = 0;

    public static bool isActive = false;

    public TextTypewriter typewriter;

    UnityEvent eventToRunOnEnd = new UnityEvent();
    bool hasEventToRunOnEnd;


    public void OpenCutscene(Cutscene[] cutscenes, bool hasEventToRun, UnityEvent eventToRun) {
        pauseManager.GetComponent<Animator>().SetTrigger("Dialogue");
        animator.SetTrigger("Start");
        currentCutscenes = cutscenes;
        activeCutscene = 0;
        isActive = true;

        
        if(hasEventToRun)
        {
            eventToRunOnEnd = new UnityEvent();
            eventToRunOnEnd = eventToRun;
            hasEventToRunOnEnd = hasEventToRun;
        }

        Debug.Log("Started Cutscene! Loaded Cutscenes: " + cutscenes.Length);
        DisplayCutscene();
    }

    void DisplayCutscene() {
        Cutscene cutsceneToDisplay = currentCutscenes[activeCutscene];
        cutsceneImage.sprite = cutsceneToDisplay.sprite;
        
        //Animate Typewriter
        typewriter.StartShowing(cutsceneToDisplay.message);
    }

    public void NextCutscene() {
        activeCutscene++;
        if (activeCutscene < currentCutscenes.Length) {
            
            FindObjectOfType<AudioManager>().Play("nextmsg");
            DisplayCutscene();
        } else {
            
            //IF TRIGGER HAS EVENT TO RUN ON END, THEN RUN IT

            if(hasEventToRunOnEnd)
            {
                eventToRunOnEnd.Invoke();
            }
            
            
            Debug.Log("Cutscene Ended");
            FindObjectOfType<AudioManager>().Play("donemsg");
            isActive = false;
            animator.SetTrigger("Exit");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if(typewriter.isActive) return;
        if(Input.GetKeyDown(KeyCode.Return) && isActive)
        {
            NextCutscene();
        }
    }
}
