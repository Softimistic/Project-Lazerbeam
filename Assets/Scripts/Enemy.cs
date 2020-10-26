using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float despawnTime;
    private float despawnTimer;
    public float maxDistance;
    public float shootingRange;
    public float shootingSpeed;
    private float shootingTimer;

    public GameObject projectile;
    private CreateEnemyBullet gun;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;

        shootingTimer = shootingSpeed;
        despawnTimer = despawnTime;

        gun = transform.Find("EnemyGun").GetComponent<CreateEnemyBullet>();
    }

    void Update()
    {
        MoveToPlayer();

        Shooting();

        DespawnEnemy();

    }

    private void MoveToPlayer()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        if (Vector3.Distance(transform.position, player.position) > maxDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player.position) <= maxDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
    }

    private void Shooting()
    {
        if (shootingTimer <= 0 && Vector3.Distance(transform.position, player.position) < shootingRange)
        {
            gun.Shoot(projectile);
            shootingTimer = shootingSpeed;
        }
        else
        {
            shootingTimer -= Time.deltaTime;
        }
    }

    private void DespawnEnemy()
    {
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 10), speed * Time.deltaTime);
        }
        else
        {
            despawnTimer -= Time.deltaTime;
        }

    }
}
