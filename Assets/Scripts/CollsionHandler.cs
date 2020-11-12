using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CollsionHandler : MonoBehaviour
{
    

    [Tooltip("In seconds")] [SerializeField] float loadDelay = 1f;
    [Tooltip("FX Prefab")] [SerializeField] GameObject deathFX;

    private GameObject controller;
    
    void Start()
    {
        controller = GameObject.FindWithTag("StateMenuUI");
    }

    void OnTriggerEnter(Collider collision)
    {
       // check if it's player's bullet, if it's not then collision happen, ship destroyed 
        if (!collision.gameObject.CompareTag("bullet"))
        {
             controller.GetComponent<GameStateController>().DisablePlayerControl(); /// Start de doodsequence
             deathFX.SetActive(true);
             //call Game over
             Invoke("ShowGameOverMenu", loadDelay);
        }

       
    }
    
    private void ShowGameOverMenu()
    {
        controller.GetComponent<GameStateController>().ActiveGameOverMenu();
    }
}