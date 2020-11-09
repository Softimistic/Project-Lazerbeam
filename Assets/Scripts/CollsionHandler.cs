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

    
    void OnTriggerEnter(Collider collision)
    {
       // check if it's player's bullet, if it's not then collision happen, ship destroyed 
        if (!collision.gameObject.CompareTag("bullet"))
        {
            
             StartDeathSequence(); /// Start de doodsequence
             deathFX.SetActive(true);
             Invoke("ReloadScene", levelLoadDelay);
             
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