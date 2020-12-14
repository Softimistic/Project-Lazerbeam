using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityStandardAssets.Utility;

public abstract class Enemy : MonoBehaviour
{

    public enum gameState
    {
        active,
        inactive,
        attached
    }

    public float spawningRange;
    public float speed;
    public float despawnTime;
    protected float despawnTimer;
    public float maxDistance;
    public float shootingRange;
    public float shootingSpeed; // in seconds
    protected float shootingTimer;
    public int accuracy; // the lower the better
    protected float time;
    protected gameState thisGameState;

    public GameObject projectile;
    protected CreateEnemyBullet gun;
    protected Transform player;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        shootingTimer = shootingSpeed;
        despawnTimer = despawnTime;

        gun = transform.Find("EnemyGun").GetComponent<CreateEnemyBullet>();
    }

    public virtual void Update()
    {
        // Checks if the enemy is active. If the enemy is not active the timer will still continue but the enemy won't do anything
        time = Time.deltaTime;
        DespawnEnemy();
        if (thisGameState == gameState.active || thisGameState == gameState.attached)
        {
            CheckPlayerInRange();
            Movement();
            Shooting();
        }
    }

    private void CheckPlayerInRange()
    {
        Vector3 direction = (player.position) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        // checks if the enemy is too close to the player and turns it on inactive when it is
        if (transform.position.z < player.position.z + 3.0f)
        {
            thisGameState = gameState.inactive;
            despawnTimer = 0;
        }
        else if (thisGameState != gameState.attached && Vector3.Distance(transform.position, player.position) <= maxDistance)
        {
            transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
            thisGameState = gameState.attached;
        }
    }

    protected abstract void Movement();

    private void Shooting()
    {
        // Checks if the player is in range for the enemy. If it is then the enemy will shoot at it
        if (shootingTimer <= 0 && Vector3.Distance(transform.position, player.position) < shootingRange)
        {
            gun.Shoot(projectile);
            shootingTimer = shootingSpeed;
        }
        else
        {
            shootingTimer -= time;
        }
    }

    private void DespawnEnemy()
    {
        if (despawnTimer <= 10 && despawnTimer >= 0)
        {
            thisGameState = gameState.inactive;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 150, transform.position.z), speed * time);
        }
        else if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            despawnTimer -= time;
        }
    }
}