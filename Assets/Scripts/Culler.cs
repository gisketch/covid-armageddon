using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Culler : MonoBehaviour
{
    void OnBecameVisible()
    {
        enabled = true;

    }

    void OnBecameInvisible()
    {
        enabled = false;
        Debug.Log("I AM INVISIBLE");
    }
}
