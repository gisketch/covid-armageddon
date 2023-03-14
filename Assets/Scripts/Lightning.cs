using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public void PlayLightningOne()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("light1");
    }
    
    public void PlayLightningTwo()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("light2");
    }
}
