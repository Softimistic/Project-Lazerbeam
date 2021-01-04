using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealth : PowerUp
{
    private Health health;

    protected override void Pickup(Collider player)
    {
        health.HealthAdd(25);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<Health>();
    }

}
