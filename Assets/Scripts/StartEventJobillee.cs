using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEventJobillee : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<DialogueTrigger>().StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
