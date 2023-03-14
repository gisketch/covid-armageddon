using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootNew : MonoBehaviour
{

    private Rigidbody2D rb;
    public LayerMask playerMask;
    private PlayerController player;
    public float speed;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 100);
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        
    }
    

}
