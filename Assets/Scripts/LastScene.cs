using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CutsceneTrigger>().StartCutscene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
