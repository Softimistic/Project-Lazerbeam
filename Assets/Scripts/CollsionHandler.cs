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
    [Tooltip("FX Prefab")] [SerializeField] ParticleSystem impactFX;


    int healthDecreaseOnMeteoriteHit = 10;
    int healthDecreaseOnEnemyRocketHit = 30;
    int healthDecreasePerHit = 5;
    int healthDecreasePerHitByMissle = 25;

    Health health;
    Shield shield;
    bool isAlGehit = false;
    

    private GameObject controller;
    
    private void Start()
    {
        health = FindObjectOfType<Health>();
        shield = FindObjectOfType<Shield>();
    }

    /// <summary>
    /// Check if there is no collision with the bullet and checking if the player is already hit. Descreasing health when hit once in the level. If the health is zero you die.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {
       // check if it's player's bullet, if it's not then collision happen, ship destroyed 
        if (collision.gameObject.CompareTag("ParryObject") && !isAlGehit)
        {

            HealthCheckNChange(healthDecreaseOnMeteoriteHit);
        }

        if (collision.gameObject.CompareTag("missle") && !isAlGehit)
        {
            HealthCheckNChange(healthDecreasePerHitByMissle);
        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            HealthCheckNChange(collision.GetComponent<EnemyBullet>().Damage);
        }

        if (collision.gameObject.CompareTag("EnemyRocket"))
        {
            HealthCheckNChange(healthDecreaseOnEnemyRocketHit);
        }
    }

    private void HealthCheckNChange(int healthDecrease)
    {
        impactFX.Play();
        isAlGehit = true;
        if (Int32.Parse(health.getHealth()) >= 0)
        {
            int leftover = (healthDecrease - shield.getShieldInt() < 0)? 0 : healthDecrease - shield.getShieldInt();
            shield.ShieldHit(healthDecrease);
            health.HealthHit(leftover);
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

    //When the player dies
    public void PlayerDies()
    {
        health.setHealthToZero(0);
        StartDeathSequence(); /// Start de doodsequence
        deathFX.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("ParryObject")){
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