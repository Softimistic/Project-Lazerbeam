using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;
    
    // Checks if the player is at a spawning checkpoint and triggers an event
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            // The player enters the spawning zone
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}