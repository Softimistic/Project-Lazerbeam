using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightEnemy : Enemy
{
    public override void Start()
    {
        base.Start();
    }
    protected override void Movement()
    {
        if (thisGameState != gameState.attached)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * time);
        }
        else if (thisGameState == gameState.attached)
        {

        }
    }

}
