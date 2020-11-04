using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CollsionHandler : MonoBehaviour
{
    [Header("GameStateController")] [SerializeField]
    public GameObject controller;

    [Tooltip("In seconds")] [SerializeField] float loadDelay = 1f;
    [Tooltip("FX Prefab")] [SerializeField] GameObject deathFX;
    

    void OnTriggerEnter(Collider collision)
    {
       // check if it's player's bullet, if it's not then collision happen, ship destroyed 
        if (!collision.gameObject.CompareTag("bullet"))
        {
             controller.GetComponent<GameStateController>().DisablePlayerControl(); /// Start de doodsequence
             deathFX.SetActive(true);
             Invoke("ShowGameOverMenu", loadDelay);
        }

       
    }

    // public void StartDeathSequence() /// Zorgt ervoor dat de controls bevroren worden. Dit gebeurt bij de PlayerController
    // {
    //     controller.GetComponent<GameStateController>().DisablePlayerControl();
    //     print("Controls are frozen");
    //
    // }

    private void ShowGameOverMenu()
    {
        controller.GetComponent<GameStateController>().ActiveGameOverMenu();
    }
}