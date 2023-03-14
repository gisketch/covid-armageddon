using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
         {
             var animator = GetComponent<Animator>();
             animator.Update(Random.value);
         }

    // Update is called once per frame
    void Update()
    {
        
    }
}
