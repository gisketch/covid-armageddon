using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class BgmManager : MonoBehaviour
{

    public Sound[] sounds;

    void Awake()
    {

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null)
        {
            Debug.LogWarning("SOUND NOT FOUND");
            return;
        }
        
        foreach(Sound sound in sounds)
        {
            if(s != sound)
                s.source.Stop();
            else
                Debug.Log("Playing " + sound.name);
                s.source.Play();
        }
    }

    
    public void PlayOneShot (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null)
        {
            Debug.LogWarning("SOUND NOT FOUND");
            return;
        }
        
        s.source.PlayOneShot(s.clip);
    }

    public void PlayOneShotRandom (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null)
        {
            Debug.LogWarning("SOUND NOT FOUND");
            return;
        }
        
        s.volume = UnityEngine.Random.Range(0.5f,1f);
        s.pitch = UnityEngine.Random.Range(0.2f,1.9f);
        s.source.PlayOneShot(s.clip);
    }
    
    public string soundName;

    void Start()
    {
        Play(soundName);
    }
}
