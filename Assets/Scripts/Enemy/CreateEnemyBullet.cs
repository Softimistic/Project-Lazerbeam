using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyBullet : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [Header("Optional")]
    [SerializeField] private GameObject projectileAlternate;

    private void Update()
    {
        
    }

    public void Shoot(GameObject player)
    {
        Vector3 direction = (player.transform.position) - transform.position;


        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation * Quaternion.Euler(90, 0, 0);

        int randomNumber = GetComponentInParent<Enemy>().GetAccuracy();
        Quaternion randomShootingField = Quaternion.Euler(Random.Range(transform.rotation.eulerAngles.x - randomNumber, transform.rotation.eulerAngles.x + randomNumber),
            Random.Range(transform.rotation.eulerAngles.y - randomNumber, transform.rotation.eulerAngles.y + randomNumber),
            Random.Range(transform.rotation.eulerAngles.z - randomNumber, transform.rotation.eulerAngles.z + randomNumber));

        GameObject boolet;
        if (projectileAlternate != null)
        {
            int cock = Random.Range(0, 10);
            if (cock > 5)
            {
                boolet = projectile;
            }
            else
            {
                boolet = projectileAlternate;
            }
        }
        else
        {
            boolet = projectile;
        }
        Instantiate(boolet, transform.position, randomShootingField);
    }
}