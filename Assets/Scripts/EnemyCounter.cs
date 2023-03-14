using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private EnemyController[] enemies;
    bool leveledUp = false;


    void Update()
    {
        enemies = FindObjectsOfType<EnemyController>();

        if(enemies.Length <= 0 && !leveledUp)
        {
            leveledUp = true;
            FindObjectOfType<QuestLog>().FinishQuest();
        }

    }
}
