using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ProximitySound : MonoBehaviour
{

    [SerializeField] private AudioSource sound;
    private PlayerController player;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {

        float diff = Vector2.Distance(transform.position, player.transform.position);


        //Clamps the difference to 0-20
        float newDiff = Mathf.Clamp(diff - 10,0,50);


        float newNewDiff = Mathf.Abs(newDiff - 50); //if newDiff = 0, newnewdiff = -20, if new diff is 20, newnewdiff = 0


        sound.volume = (0.02f * newNewDiff) * 0.45f;

    }
}
