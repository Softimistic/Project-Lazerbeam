using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyBullet : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    private void Update()
    {
        
    }

    public void Shoot(GameObject player)
    {
        Vector3 direction = (player.transform.position + player.transform.forward * 6f) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation * Quaternion.Euler(90, 0, 0);

        int randomNumber = GetComponentInParent<Enemy>().GetAccuracy();
        Quaternion randomShootingField = Quaternion.Euler(Random.Range(transform.rotation.eulerAngles.x - randomNumber, transform.rotation.eulerAngles.x + randomNumber), Random.Range(transform.rotation.eulerAngles.y - randomNumber, transform.rotation.eulerAngles.y + randomNumber), Random.Range(transform.rotation.eulerAngles.z - randomNumber, transform.rotation.eulerAngles.z + randomNumber));
        Instantiate(projectile, transform.position, randomShootingField);
    }
}