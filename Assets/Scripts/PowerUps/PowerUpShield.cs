using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : PowerUp
{
    public AudioClip shieldSound;

    protected override void Pickup(Collider player)
    {
        AudioSource.PlayClipAtPoint(shieldSound, transform.position);
        player.GetComponent<Shield>().ShieldAdd(15);
    }
}
