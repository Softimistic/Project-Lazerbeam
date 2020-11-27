using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class CreateBossLazer:MonoBehaviour
    {
        public void Shoot(GameObject projectile,Transform player)
        {
            int randomNumber = 20;
            Quaternion randomShootingField = Quaternion.Euler(
                Random.Range(transform.rotation.eulerAngles.x - randomNumber, transform.rotation.eulerAngles.x + randomNumber),
                Random.Range(transform.rotation.eulerAngles.y - randomNumber, transform.rotation.eulerAngles.y + randomNumber), 
                Random.Range(transform.rotation.eulerAngles.z - randomNumber, transform.rotation.eulerAngles.z + randomNumber));
            Instantiate(projectile, transform.position, randomShootingField);
        }
    }
}