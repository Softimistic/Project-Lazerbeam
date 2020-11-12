using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private ColliderTrigger colliderTrigger;

    // Spawns enemies in specific positions after the player gets to a spawning checkpoint
    private void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs ea)
    {
        Instantiate(enemyTransform, new Vector3(20, 1, 150), new Quaternion());
        Instantiate(enemyTransform, new Vector3(30, 1, 150), new Quaternion());
        Instantiate(enemyTransform, new Vector3(40, 1, 150), new Quaternion());
    }
}
