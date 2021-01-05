using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : PowerUp
{
    protected override void Pickup(Collider player)
    {
        player.GetComponent<Shield>().ShieldAdd(15);
    }
}
