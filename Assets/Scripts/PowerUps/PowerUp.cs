using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
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
        
    }

    protected abstract void Pickup(Collider player);
    
}
