using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrop : MonoBehaviour
{
    public GameObject dropObject;
    public GameObject trigger;
    // Start is called before the first frame update
    void OnDestroy()
    {
        FindObjectOfType<QuestLog>().FinishQuest();
        GameObject newDrop = Instantiate(dropObject, trigger.transform.position, Quaternion.identity);
    }
}
