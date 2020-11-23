using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleTower : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject missle;
    int index = 0;
    int delayMissleTime = 5;

    void Start()
    {
        Invoke("SpawnMissle", 5f);
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
        Invoke("SpawnMissle", 5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
