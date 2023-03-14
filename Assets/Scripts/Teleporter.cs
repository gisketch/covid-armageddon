using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Teleporter : MonoBehaviour
{
    GameObject hero;
    public Transform newLoc;

    void Start()
    {
        hero = FindObjectOfType<PlayerController>().gameObject;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Invoke("Teleport", 0.1f);
            GameObject.Find("BlackTransition").GetComponent<Animator>().SetTrigger("FadeIn");
        }
    }

    void Teleport()
    {
        hero.transform.position = newLoc.position;
    }
}
