using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
     void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;
           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
           s.source.loop = s.loop;
        }
    }

      void Start()
     {
         Play("Theme");
     }

     public void Play(string name)
     {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }

        s.source.Play();
     }

     public void PauseTheme(string name)
     {
         Sound s = Array.Find(sounds, sound => sound.name == name);
         if (s == null)
         {
             return;
         }

         s.source.spatialBlend = 1f;
         s.source.volume = 1f;
     }

     public void ResumeTheme(string name)
     {
         Sound s = Array.Find(sounds, sound => sound.name == name);
         if (s == null)
         {
             return;
         }

         s.source.spatialBlend = 0f;
         s.source.volume = 0.2f;
     }
}
