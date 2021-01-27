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
    [Tooltip("FX Prefab")] [SerializeField] GameObject impactFX;
    public AudioClip gettingHitByEnemySound;
    public AudioClip gettingHitByMeteoriteSound;

    int healthDecreaseOnMeteoriteHit = 10;
    int healthDecreaseOnMineHit = 25;
    int healthDecreaseOnEnemyRocketHit = 30;
    int healthDecreasePerHit = 5;
    int healthDecreasePerHitByMissle = 25;
    int healthDecreasePerHitByTrain = 1;

    Health health;
    Shield shield;
    bool isAlGehit = false;
    

    private GameObject controller;
    
    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        shield = GameObject.FindGameObjectWithTag("Player").GetComponent<Shield>();
    }

    private void FixedUpdate()
    {
        if (health.GetHealthInt() > 25 && hitFX.active)
        {
            hitFX.SetActive(false);
        }
        else if (health.GetHealthInt() <= 0)
        {
            PlayerDies();
        }
        else if (health.GetHealthInt() <= 25)
        {
            hitFX.SetActive(true);
        }
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
            AudioSource.PlayClipAtPoint(gettingHitByMeteoriteSound, transform.position);
        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            HealthCheckNChange(collision.GetComponent<EnemyBullet>().Damage);
            AudioSource.PlayClipAtPoint(gettingHitByEnemySound, transform.position);
        }

        if (collision.gameObject.CompareTag("EnemyRocket"))
        {
            HealthCheckNChange(healthDecreaseOnEnemyRocketHit);
        }

        if (collision.gameObject.CompareTag("Train"))
        {
            HealthCheckNChange(healthDecreasePerHitByTrain);
        }

        if (collision.gameObject.CompareTag("Mine"))
        {
            HealthCheckNChange(healthDecreaseOnMineHit);
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<MeshCollider>().enabled = false;
            collision.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            collision.gameObject.GetComponentInChildren<AudioSource>().Play();
        }

        if (collision.gameObject.CompareTag("InstaKill"))
        {
            PlayerDies();
        }
    }

    private void HealthCheckNChange(int healthDecrease)
    {
        impactFX.GetComponent<ParticleSystem>().Play();
        impactFX.GetComponent<AudioSource>().Play();
        isAlGehit = true;
        if (health.GetHealthInt() >= 0)
        {
            int leftover = (healthDecrease - shield.getShieldInt() < 0)? 0 : healthDecrease - shield.getShieldInt();
            shield.ShieldHit(healthDecrease);
            health.HealthHit(leftover);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("missle") && !isAlGehit)
        {
            HealthCheckNChange(healthDecreasePerHitByMissle);
        }
        isAlGehit = false;
        
    }

    //When the player dies
    public void PlayerDies()
    {
        health.SetHealthToZero(0);
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