﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossBody : MonoBehaviour
{
    [Header("Settings")] public float degreePerS;

    private Vector3 startPosition;
    private bool _pause = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
            RotateBody();
            MoveVertical();
    }
    
    private void RotateBody()
    {
        transform.Rotate(Vector3.up, degreePerS * Time.deltaTime, Space.Self);
    }

    private void MoveVertical()
    {
        transform.position = new Vector3(startPosition.x, startPosition.y + Mathf.Sin(Time.time * 3), startPosition.z);
    }
}