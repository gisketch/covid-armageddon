using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderAnimator : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void LoadLoader()
    {
        anim.SetTrigger("Start");
    }
}
