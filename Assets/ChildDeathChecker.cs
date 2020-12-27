﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildDeathChecker : SceneTransitionEvent
{
    private int _counter = 0;

    void FixedUpdate()
    {
        if (GetComponentsInChildren<Transform>().Length == 1)
        {
            if (_counter > 100)
            {
                LoadNextScene();
            }
            _counter++;
        }
    }
}