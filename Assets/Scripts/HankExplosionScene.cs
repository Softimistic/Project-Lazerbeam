using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HankExplosionScene : SceneTransitionEvent
{
    [Tooltip("FX Prefab")] [SerializeField] ParticleSystem deathFX;
    private GameObject controller;
    AudioSource myAudio;
    public MeshRenderer MeshSpaceship;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        Invoke("HankDies", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HankDies()
    {
        deathFX.Play();
        myAudio.Play();
        Invoke("SpaceshipDestroyed", 0.5f);
        Invoke("LoadNextScene", 1.5f);
    }
    public void SpaceshipDestroyed()
    {
        MeshSpaceship.enabled = false;
    }
}
