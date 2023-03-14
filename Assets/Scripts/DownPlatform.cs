using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPlatform : MonoBehaviour
{

    PolygonCollider2D polygonCollider2D;
    PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(player.isOnSlope)
            {
                polygonCollider2D.enabled = false;
                Invoke("ReEnableCol", 1f);            
            }
        }
    }

    void ReEnableCol()
    {
        polygonCollider2D.enabled = true;
    }



}
