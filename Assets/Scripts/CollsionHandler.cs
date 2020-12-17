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


    [SerializeField] int healthDecreaseOnMeteoriteHit = 25;
    [SerializeField] int healthDecreaseOnEnemyBulletHit = 5;
    [SerializeField] int healthDecreaseOnEnemyRocketHit = 30;
    [SerializeField] int healthDecreasePerHit = 5;
    [SerializeField] int healthDecreasePerHitByMissle = 25;

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
        if (!collision.gameObject.CompareTag("EventTrigger") && !collision.gameObject.CompareTag("bullet") && !isAlGehit)
        {
           
            isAlGehit = true;
            if (Int32.Parse(health.getHealth()) >= 0)
            {
                
                if (collision.gameObject.CompareTag("ParryObject"))
                    health.HealthHit(healthDecreaseOnMeteoriteHit);
                if (collision.gameObject.CompareTag("EnemyBullet"))
                    health.HealthHit(healthDecreaseOnEnemyBulletHit);
                if (collision.gameObject.CompareTag("EnemyRocket"))
                    health.HealthHit(healthDecreaseOnEnemyRocketHit);
            }

            if (Int32.Parse(health.getHealth()) <= 25)
            {
                hitFX.SetActive(true);
            }

            if (Int32.Parse(health.getHealth()) <= 0)
            {
                PlayerDies();
            }
        }

        if (collision.gameObject.CompareTag("missle") && !isAlGehit)
        {
            isAlGehit = true;
            if (Int32.Parse(health.getHealth()) >= 0)
            {
                health.HealthHit(healthDecreasePerHitByMissle);

            }
            if (Int32.Parse(health.getHealth()) <= 25)
            {
                hitFX.SetActive(true);
            }

            if (Int32.Parse(health.getHealth()) <= 0)
            {
                PlayerDies();
            }
        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log(collision.name + " ");
            health.HealthHit(healthDecreaseOnEnemyBulletHit);
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
        GameObject.FindWithTag("StateMenuUI").GetComponent<GameStateController>().ActiveGameOverMenu();
    }
}