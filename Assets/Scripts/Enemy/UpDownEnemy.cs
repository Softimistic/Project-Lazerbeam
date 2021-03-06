﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownEnemy : Enemy
{
    private float up;
    private bool moveUp;
    private Vector3 pos;
    [Header("Angle variables")]
    [SerializeField] private float frequency;
    [SerializeField] private float magnitude;

    public override void Start()
    {
        base.Start();
        pos = transform.position;
        up = 0;
    }
    protected override void Movement()
    {
        if (thisGameState != GameState.attached)
        {
            pos += transform.forward * time * -speed;
            transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        }
        else if (thisGameState == GameState.attached)
        {
            pos = transform.position;
            if (up <= 0)
            {
                moveUp = false;
            }
            else if (up >= magnitude * frequency)
            {
                moveUp = true;
            }
            if (moveUp)
            {
                transform.position = pos + transform.up * -speed * time;
                up += -speed * time;
            }
            else if (!moveUp)
            {
                transform.position = pos + transform.up * speed * time;
                up += speed * time;
            }
        }
    }
}
