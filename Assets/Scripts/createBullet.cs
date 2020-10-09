using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;
using Debug = UnityEngine.Debug;

public class createBullet : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public GameObject ship;
    [SerializeField] [Range(2, 10)] private int speedfactor;

    // Start is called before the first frame update
    void Start()
    {
        //set speed to bullet
        bulletPrefab.GetComponent<bullet>().Speed = ship.GetComponent<PlayerController>().Speed * speedfactor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {

            shootBullet();
        }
       
    }
    public void shootBullet()
    {
        //Bullet gets called with position/rotation of gun object
         GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
         
    
    }
}
