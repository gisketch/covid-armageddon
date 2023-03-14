using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationLabelManager : MonoBehaviour
{
    public Image locImg;
    public Animator animator;

    public void ShowLocation(LocationLabel locLabel){
        locImg.sprite = locLabel.locationLabelImage;
        animator.SetTrigger("Show");
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
