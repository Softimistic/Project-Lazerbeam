using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;
using Debug = UnityEngine.Debug;

public class createBullet : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public GameObject ship;
    public GameObject camera;
    public bool BossMode;
    [SerializeField] private int speedfactor;

    // Start is called before the first frame update
    void Start()
    {
        //set speed to bullet
        if (BossMode)
        {
            bulletPrefab.GetComponent<bullet>().Speed = speedfactor;
        }
        else
        {
            bulletPrefab.GetComponent<bullet>().Speed = camera.GetComponent<BetterWaypointFollower>().routeSpeed * speedfactor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && ship.GetComponent<PlayerController>().isActiveAndEnabled)
        {
            shootBullet();
        }
    }
    public void shootBullet()
    {
        //Bullet gets called with position/rotation of gun object
         GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
         Debug.Log("Bullet Speed : " + bulletPrefab.GetComponent<bullet>().Speed);
    }
}
