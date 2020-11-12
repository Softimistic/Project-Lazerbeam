using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollsionHandler : MonoBehaviour
{

    [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX Prefab")] [SerializeField] GameObject deathFX;
    [Tooltip("FX Prefab")] [SerializeField] GameObject hitFX;


    [SerializeField] int healthDecreasePerHit = 50;

    Health health;
    bool isAlGehit = false;

    private GameObject controller;
    
    private void Start()
    {
        health = FindObjectOfType<Health>();
    }

    /// <summary>
    /// Check if there is no collision with the bullet and checking if the player is already hit. Descreasing health when hit once in the level. If the health is zero you die.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {
       // check if it's player's bullet, if it's not then collision happen, ship destroyed 
        if (!collision.gameObject.CompareTag("bullet") && !isAlGehit)
        {
            isAlGehit = true;
            if (Int32.Parse(health.getHealth()) >= 0)
            {
                health.HealthHit(healthDecreasePerHit);
                //print(health.getHealth());
                hitFX.SetActive(true);
            }
            if(Int32.Parse(health.getHealth()) <= 0)
            {
                PlayerDies();
            }
        }
    }

    //When the player dies
    public void PlayerDies()
    {
        health.setHealthToZero(0);
        StartDeathSequence(); /// Start de doodsequence
        deathFX.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("bullet")){
            isAlGehit = false;
        }
        
    }

    public void StartDeathSequence() /// Zorgt ervoor dat de controls bevroren worden. Dit gebeurt bij de PlayerController
    {
        GetComponent<PlayerController>().enabled = false;
        print("Controls are frozen");

    }

    private void ReloadScene() /// string in OnTriggerEnter
    {
        SceneManager.LoadScene(2);
    }
}