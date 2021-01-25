using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBoost : PowerUp
{
    private PlayerController player1;
    public AudioClip boostSound;
    
    protected override void Pickup(Collider player)
    {
        AudioSource.PlayClipAtPoint(boostSound, transform.position);
        player1.addBoost(20);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        player1 = FindObjectOfType<PlayerController>();
    }

    
}
