using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    
    public TMP_Text positiveText;
    public TMP_Text negativeText;
    
    public Button positiveBtn;
    public Button negativeBtn;

    public RectTransform backgroundBox;
    public Animator animator;
    public Animator controllerAnim;

    public GameObject indicator;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    UnityEvent eventToRunOnEnd = new UnityEvent();

    bool hasOptions;
    bool hasEventToRunOnEnd;

    public static bool isActive = false;

    public TextTypewriter typewriter;

    public Animator uiAnim;

    public void OpenDialogue(Message[] messages, Actor[] actors, Choices[] choices, bool hasChoices, UnityEvent eventToRun, bool hasEventToRun) {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        
        FindObjectOfType<AudioManager>().Play("donemsg");


        //CHOICES
        if(hasChoices)
        {
            positiveBtn.onClick.RemoveAllListeners();
            negativeBtn.onClick.RemoveAllListeners();
            positiveText.text = choices[0].choice;
            negativeText.text = choices[1].choice;
            positiveBtn.onClick.AddListener(choices[0].events.Invoke);
            negativeBtn.onClick.AddListener(choices[1].events.Invoke);
            hasOptions = hasChoices;
        }
        if(hasEventToRun)
        {
            eventToRunOnEnd = new UnityEvent();
            eventToRunOnEnd = eventToRun;
            hasEventToRunOnEnd = hasEventToRun;
        }

        Debug.Log("Started Convo! Loaded messages " + messages.Length);
        indicator.transform.localScale = Vector3.one;
        DisplayMessage();
        

        
    }

    void DisplayMessage() {
        
        uiAnim.SetTrigger("Dialogue");

        Message messageToDisplay = currentMessages[activeMessage];
        //Animate Typewriter
        typewriter.StartShowing(messageToDisplay.message);

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        if(activeMessage == currentMessages.Length - 1)
        {
            indicator.transform.localScale = Vector3.zero;
        }

    }

    public void NextMessage() {
        activeMessage++;
        if(activeMessage < currentMessages.Length) {
            DisplayMessage();
            FindObjectOfType<AudioManager>().Play("nextmsg");
            if(activeMessage == currentMessages.Length - 1)
            {
                indicator.transform.localScale = Vector3.zero;
            }
        } else
        {
            
            uiAnim.SetTrigger("Resume");  
            //IF TRIGGER HAS EVENT TO RUN ON END, THEN RUN IT
            if(hasEventToRunOnEnd)
            {
                eventToRunOnEnd.Invoke();
            }

            

            Debug.Log("Conversation ended");
            FindObjectOfType<AudioManager>().Play("donemsg");
            isActive = false;
            controllerAnim.SetBool("isActive", false);
            hasOptions = false;
            hasEventToRunOnEnd = false;
        }
    }

    void Update()
    {
        animator.SetBool("isActive", isActive);
        if(typewriter.isActive) return;

        if(currentMessages != null && activeMessage == currentMessages.Length - 1 && hasOptions) 
        {
            controllerAnim.SetBool("isActive", isActive);
            return;
        }

        if((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && isActive)
        {
            Debug.Log("Next");
            NextMessage();
        }
    }
}
