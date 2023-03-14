using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneAnimator : MonoBehaviour
{
    public static bool isActive;
    Animator animator;

    string animString;

    public void AnimateCutscene(Animator anim)
    {
        animator = anim;
        animator.SetTrigger("Start");
        isActive = true;
        StartCoroutine("OnCompleteAnimation");
    }

    IEnumerator OnCompleteAnimation()
    {       

        //TODO: This code is specifically for scene 1 to 2
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("TravelTo2021Start")) yield return null;
        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) yield return null;
        isActive = false;
        FindObjectOfType<LevelManager>().LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("ANIMATION ENDED");
    }
}
