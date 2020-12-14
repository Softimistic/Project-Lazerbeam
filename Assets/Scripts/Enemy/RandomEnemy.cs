using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : Enemy
{
    private Vector2 field = new Vector2(5f, 5f);
    private Vector3 pos;
    private bool firstAttach;
    public override void Start()
    {
        base.Start();
        firstAttach = false;
    }
    protected override void Movement()
    {
        if (thisGameState != gameState.attached)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * time);
        }
        else if (thisGameState == gameState.attached && !firstAttach)
        {
            pos = transform.position;
            firstAttach = true;
        }
        else if (thisGameState == gameState.attached && firstAttach)
        {

        }
    }
}
