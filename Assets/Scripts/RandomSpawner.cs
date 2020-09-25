using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public Transform parent;
    public float xRnage = 1f;
    public float yRnage = 1f;
    public float minSpawnTime = 1f;
    public float maxSpawnTIme = 10f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnWall",Random.Range(minSpawnTime,maxSpawnTIme));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnWall()
    {
        float xOffset = Random.Range(-xRnage, xRnage);
        float yOffest = Random.Range(-yRnage, yRnage);
        int spawnObjectIndex = Random.Range(0, spawnObjects.Length);
        GameObject instance =  GameObject.Instantiate(spawnObjects[spawnObjectIndex],
            transform.position + new Vector3(xOffset,yOffest,0.0f),
            spawnObjects[spawnObjectIndex].transform.rotation
            ) as GameObject;
        instance.transform.parent = parent;
        Invoke("SpawnWall",Random.Range(minSpawnTime,maxSpawnTIme));
    }
}
