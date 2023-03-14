using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRequirement : MonoBehaviour
{
    
    void Update()
    {
        if(FindObjectOfType<PlayerController>().hasMelee)
        {
            Destroy(this.gameObject);
        }
    }
}
