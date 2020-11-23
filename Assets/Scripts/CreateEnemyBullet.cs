using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(GameObject projectile)
    {
        int randomNumber = GetComponentInParent<Enemy>().accuracy;
        Quaternion randomShootingField = Quaternion.Euler(Random.Range(transform.rotation.eulerAngles.x - randomNumber, transform.rotation.eulerAngles.x + randomNumber), Random.Range(transform.rotation.eulerAngles.y - randomNumber, transform.rotation.eulerAngles.y + randomNumber), Random.Range(transform.rotation.eulerAngles.z - randomNumber, transform.rotation.eulerAngles.z + randomNumber));
        Instantiate(projectile, transform.position, randomShootingField);
    }
}