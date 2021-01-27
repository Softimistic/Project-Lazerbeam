using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected GameObject player;
    protected float timer;
    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
            Destroy(gameObject);
        }
        
    }

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = 10;
    }

    public virtual void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > 1 && distance < 150)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                Destroy(gameObject);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, distance * Time.deltaTime);
        }
        
    }

    protected abstract void Pickup(Collider player);
    
}
