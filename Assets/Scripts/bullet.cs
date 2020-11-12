using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class bullet : MonoBehaviour
{
    [SerializeField] public GameObject terrainExplosion;
    [SerializeField] public GameObject obstacleExplosion;
    [SerializeField] public GameObject enemyShipExplosion;
    private Rigidbody rb;
    public float speed;
    ScoreBoard scoreBoard;

    // Start is called before the first frame update  
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        StartCoroutine(SelfDestruct());
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
    }
    
     void OnTriggerEnter(Collider collision)
    {
        // hit on terrain
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("ParryObject"))
        {
            Destroy(gameObject);
            GameObject nwFx = Instantiate(terrainExplosion,transform.position,Quaternion.identity);
            FxSelfDestroy(nwFx);
            

        }
        //hit on Obstacle
        if (collision.gameObject.name == "Obstacle")
        {
            Destroy(gameObject);
            GameObject nwFx = Instantiate(obstacleExplosion,transform.position,Quaternion.identity);
            FxSelfDestroy(nwFx);
            
        }
        //hit on enemyships
        if (collision.gameObject.CompareTag("EnemyShips"))
        {
            Destroy(gameObject);
            GameObject nwFx = Instantiate(enemyShipExplosion,transform.position,Quaternion.identity);
            FxSelfDestroy(nwFx);
        }
    }

     private void FxSelfDestroy(GameObject nwFx)
     {
         ParticleSystem parts = nwFx.GetComponent<ParticleSystem>();
         //get the play time
         float totalDuration = parts.duration + parts.startLifetime;
         // delete 
         Destroy(nwFx, totalDuration);
     }

     public float Speed
    {
        get => speed;
        set => speed = value;
    }
    

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}