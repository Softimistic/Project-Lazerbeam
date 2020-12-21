﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleTower : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject missle;
    public int range;
    int index = 0;
    int delayMissleTime = 5;
    bool lockOn = false;
    public Transform target;
    bool onetime = false;
    public float health = 100;

    void Start()
    {
        if (lockOn)
        {
            Invoke("SpawnMissle", 2f);
        }
    }

    void SpawnMissle()
    {
        
        GameObject newMissle = Instantiate(missle, spawnPoints[index].position, spawnPoints[index].rotation) as GameObject;
        if(index < spawnPoints.Length)
        {
            index++;
        }
        if(index == 1)
        {
            index = 0;
        }
        if (lockOn)
        {
            Invoke("SpawnMissle", 2f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < range)
        {
            
            if (!onetime)
            {
                lockOn = true;
                Start();
                onetime = true;
            }
        }
        else
        {
            lockOn = false;
            onetime = false;
        }
      
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(gameObject);
        }
    }
}
