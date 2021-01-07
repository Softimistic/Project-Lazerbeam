using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealth : PowerUp
{
    public AudioClip healthSound;

    protected override void Pickup(Collider player)
    {
        AudioSource.PlayClipAtPoint(healthSound, transform.position);
        player.GetComponent<Health>().HealthAdd(25);
    }
}
