using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Enemy : MonoBehaviour
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
    private float despawnTimer;
    public float maxDistance;
    public float shootingRange;
    public float shootingSpeed; // in seconds
    private float shootingTimer;
    public int accuracy; // the lower the better
    public float movement;
    private float time;
    private gameState thisGameState;
    public float frequency;
    public float magnitude;
    private Vector3 axis;
    private Vector3 pos;

    public GameObject projectile;
    private CreateEnemyBullet gun;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        shootingTimer = shootingSpeed;
        despawnTimer = despawnTime;
        pos = transform.position;
        axis = transform.right;

        gun = transform.Find("EnemyGun").GetComponent<CreateEnemyBullet>();
    }

    void Update()
    {
        // Checks if the enemy is active. If the enemy is not active the timer will still continue but the enemy won't do anything
        time = Time.deltaTime;
        DespawnEnemy();
        if (thisGameState == gameState.active || thisGameState == gameState.attached)
        {
            Movement();
            Shooting();
        }
    }

    private void CheckPlayerInRange()
    {
        if (Vector3.Distance(transform.position, player.position) <= spawningRange)
        {
            gameObject.SetActive(true);
            thisGameState = gameState.active;
        }
    }

    private void Movement()
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
        switch (movement)
        {
            case 1:
                MoveToPlayer();
                break;
            case 2:
                ZigZag();
                break;
            default:
                MoveToPlayer();
                break;
        }
    }

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

    // Moves the enemy straight to the player
    private void MoveToPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) > maxDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * time);
        }
        else if (Vector3.Distance(transform.position, player.position) <= maxDistance)
        {

        }
    }

    // Moves the enemy towards the player with a zigzag movement
    private void ZigZag()
    {
        if (Vector3.Distance(transform.position, player.position) > maxDistance)
        {
            pos += transform.forward * time * -player.GetComponent<PlayerController>().Speed;
            transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
        }
        else if (Vector3.Distance(transform.position, player.position) <= maxDistance)
        {
            pos += transform.position * time * 1;
            transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
        }
    }
}
