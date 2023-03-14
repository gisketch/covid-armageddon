using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] private EnemyController[] enemies;
    public Animator animator;

    public bool doorOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        enemies = FindObjectsOfType<EnemyController>();

        if(enemies.Length <= 0 && doorOpened == false)
        {
            doorOpened = true;
            animator.SetTrigger("openDoor");
            FindObjectOfType<QuestLog>().FinishQuest();
        }

    }
}
