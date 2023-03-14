using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RooftopScene : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void NextScene()
    {
        FindObjectOfType<LevelManager>().LoadScene(11);
    }
}
