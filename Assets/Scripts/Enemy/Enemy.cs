using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public abstract class Enemy : MonoBehaviour
{

    public enum GameState
    {
        active,
        inactive,
        attached,
        despawning
    }

    [Header("Base variables")]
    [SerializeField] private float spawningRange;
    [SerializeField] protected float speed;
    [SerializeField] private float despawnTime;
    protected float despawnTimer;
    [SerializeField] private float maxDistance;
    [Header("Shooting variables")]
    [SerializeField] private float shootingRange;
    [SerializeField] private float shootingSpeed; // in seconds
    protected float shootingTimer;
    [SerializeField] private int accuracy; // the lower the better
    protected float time;
    protected GameState thisGameState;
    [Header("Item drop variables")]
    [SerializeField] protected int itemDropChance; // in percentage (60 = 60%)
    [SerializeField] protected PowerUpHealth healthPowerUp;
    [SerializeField] protected PowerUpBoost boostPowerUp;
    [SerializeField] protected PowerUpShield shieldPowerUp;

    protected List<GameObject> guns = new List<GameObject>();
    protected int currentGun;
    protected GameObject player;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        shootingTimer = shootingSpeed;
        despawnTimer = despawnTime;
        thisGameState = GameState.inactive;

        currentGun = 0;
        foreach (Transform child in transform) if (child.CompareTag("EnemyGun"))
            {
                guns.Add(child.gameObject);
            }
    }

    public virtual void Update()
    {
        // Checks if the enemy is active. If the enemy is not active the timer will still continue but the enemy won't do anything
        time = Time.deltaTime;
        DespawnEnemy();
        if (thisGameState != GameState.despawning)
        {
            CheckPlayerInRange();
        }
        if (thisGameState == GameState.active || thisGameState == GameState.attached)
        {
            Movement();
            Shooting();
        }
    }

    private void CheckPlayerInRange()
    {
        Vector3 direction = (player.transform.position + player.transform.forward * 3) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        // checks if the enemy is too close to the player and makes it despawn when it is
        if (transform.position.z < player.transform.position.z + 3.0f)
        {
            despawnTimer = 0;
        }
        // if the enemy is in maxDistance it will get attached to the camera so that it stays in place
        else if (thisGameState != GameState.attached && Vector3.Distance(transform.position, player.transform.position) <= maxDistance)
        {
            transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
            thisGameState = GameState.attached;
        }
        // when the player gets in the spawningRange of the enemy the enemy will turn active
        else if (thisGameState != GameState.attached && thisGameState != GameState.active && Vector3.Distance(transform.position, player.transform.position) <= spawningRange)
        {
            thisGameState = GameState.active;
        }
    }

    protected abstract void Movement();

    private void Shooting()
    {
        // Checks if the player is in range for the enemy. If it is then the enemy will shoot at it
        if (shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) < shootingRange)
        {
            guns[currentGun].GetComponent<CreateEnemyBullet>().Shoot(player);
            currentGun = (currentGun == guns.Count - 1) ? 0 : currentGun + 1;
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
            thisGameState = GameState.despawning;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,transform.position.y + 150, transform.position.z), (player.GetComponent<PlayerController>().IsBoosting())? speed * time * 3 : speed * time);
        }
        else if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
        despawnTimer -= (player.GetComponent<PlayerController>().IsBoosting())? time * 3 : time;
    }

    public int GetAccuracy()
    {
        return accuracy;
    }

    public void OnDeath()
    {
        int willDrop = Random.Range(1, 101);
        if(willDrop <= itemDropChance)
        {
        int itemToDrop = Random.Range(0, 3);
        PowerUp pickup = null;
        switch (itemToDrop)
        {
            case 0: pickup = healthPowerUp;
                break;
            case 1: pickup = boostPowerUp;
                break;
            case 2: pickup = shieldPowerUp;
                break;
        }
        Instantiate(pickup, transform.position, transform.rotation);
        }
    }
}