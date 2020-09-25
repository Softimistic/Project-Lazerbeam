using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;
using Debug = UnityEngine.Debug;

public class createBullet : MonoBehaviour
{
    
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
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
