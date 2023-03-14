using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressRequirement : MonoBehaviour
{

    public int progress;
    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<QuestLog>().progress > progress) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
