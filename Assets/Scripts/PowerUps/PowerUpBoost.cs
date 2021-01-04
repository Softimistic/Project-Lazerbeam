using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBoost : PowerUp
{
    PlayerController player1;

    protected override void Pickup(Collider player)
    {
        player1.addBoost(20);
    }

    // Start is called before the first frame update
    void Start()
    {
        player1 = FindObjectOfType<PlayerController>();
    }

    
}
