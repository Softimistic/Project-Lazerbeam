using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HankExplosionScene : SceneTransitionEvent
{
    [Tooltip("FX Prefab")] [SerializeField] ParticleSystem deathFX;
    bool AlOntploft = false;
    private GameObject controller;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("HankDies", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HankDies()
    {
        deathFX.Play();
    }
}
