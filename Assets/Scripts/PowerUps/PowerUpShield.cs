using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : PowerUp
{
    private Shield shield;
    protected override void Pickup(Collider player)
    {
        shield.ShieldAdd(15);
    }

    // Start is called before the first frame update
    void Start()
    {
        shield = FindObjectOfType<Shield>();
    }
}
