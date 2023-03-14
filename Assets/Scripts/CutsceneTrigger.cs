using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CutsceneTrigger : MonoBehaviour
{
    public Cutscene[] cutscenes;
    public bool hasEventToRunOnEnd;
    public UnityEvent eventToRunOnEnd = new UnityEvent();

    public void StartCutscene() {
        FindObjectOfType<CutsceneManager>().OpenCutscene(cutscenes, hasEventToRunOnEnd, eventToRunOnEnd);
    }
}

[System.Serializable]
public class Cutscene {
    public Sprite sprite;
    [TextArea(10,20)]
    public string message;
}