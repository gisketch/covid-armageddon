using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    void Next()
    {
        
        FindObjectOfType<LevelManager>().LoadScene(0);   
    }
}
