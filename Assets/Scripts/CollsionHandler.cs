using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CollsionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print("Player triggered something!");
    }

    private void StartDeathSequence()
    {
        print("Player is losing health");
        SendMessage("OnPlayerDeath");

    }
}
