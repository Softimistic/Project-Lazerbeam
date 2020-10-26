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
        Instantiate(projectile, transform.position, transform.rotation);
    }
}