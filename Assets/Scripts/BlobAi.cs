using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAi : MonoBehaviour
{      
    public float speed;
    public float timer;

    enum State {
        Searching,
        Attacking
    }

    State blobState;

    void Start()
    {
        blobState = State.Searching;
    }

    void Update()
    {
        if(blobState == State.Searching)
        {
            Searching();
        }
    }

    public bool blobFirst = false;

    void Searching()
    {
        
        if(timer <= 0f)
        {
            if(!blobFirst)
            {
                timer = Random.Range(1f,2.5f);
            } else
            {
                timer = 1.2f;
            }
            speed *= -1;
            float newScale = transform.localScale.x * -1;
            transform.localScale = new Vector3(newScale, transform.localScale.y, transform.localScale.z);
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

        transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
