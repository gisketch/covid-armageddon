using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public bool hasChoices = false;

    public Choices[] choices;

    public bool hasEventToRunOnEnd = false;
    public UnityEvent eventToRunOnEnd;

    public void StartDialogue() {
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors, choices, hasChoices, eventToRunOnEnd, hasEventToRunOnEnd);
    }
}

[System.Serializable]
public class Message {
    public int actorId;
    [TextArea]
    public string message;
}

[System.Serializable]
public class Actor {
    public string name;
    public Sprite sprite;
}

[System.Serializable]
public class Choices {
    public string choice;
    public UnityEvent events;
}
