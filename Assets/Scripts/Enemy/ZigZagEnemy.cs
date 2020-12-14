using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagEnemy : Enemy
{
    private float right;
    private bool moveRight;
    private Vector3 pos;
    public float frequency;
    public float magnitude;

    public override void Start()
    {
        base.Start();
        pos = transform.position;
        right = 0;
    }
    protected override void Movement()
    {
        if (thisGameState != gameState.attached)
        {
            pos += transform.forward * time * -speed;
            transform.position = pos + transform.right * Mathf.Sin(Time.time * frequency) * magnitude;
        }
        else if (thisGameState == gameState.attached)
        {
            pos = transform.position;
            if (right <= 0)
            {
                moveRight = false;
            }
            else if (right >= magnitude * frequency)
            {
                moveRight = true;
            }
            if (moveRight)
            {
                transform.position = pos + transform.right * -speed * time;
                right += -speed * time;
            }
            else if (!moveRight)
            {
                transform.position = pos + transform.right * speed * time;
                right += speed * time;
            }
        }
    }
}
