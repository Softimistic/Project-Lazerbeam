using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealth : PowerUp
{
    private Health health;
    public AudioClip healthSound;

    protected override void Pickup(Collider player)
    {
        AudioSource.PlayClipAtPoint(healthSound, transform.position);
        health.HealthAdd(25);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<Health>();
    }

}
