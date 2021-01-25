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
    [SerializeField] [Range(2, 300)] private int speedfactor;
    bool spacebarPressed = false;
    public float cooldown_ms;
    float timer_ms = 0;
    // Start is called before the first frame update
    void Start()
    {
        //set speed to bullet
        if (camera.GetComponent<BetterWaypointFollower>().routeSpeed != 0)
        {
            bulletPrefab.GetComponent<bullet>().Speed = camera.GetComponent<BetterWaypointFollower>().routeSpeed * speedfactor;
        }
        else
        {
            bulletPrefab.GetComponent<bullet>().Speed = 300f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && ship.GetComponent<PlayerController>().isActiveAndEnabled)
        {
            spacebarPressed = true;
        }
        else if (Input.GetKeyUp("space"))
        {
            spacebarPressed = false;
        }

        if (spacebarPressed)
        {
            if (timer_ms <= 0)
            {
                shootBullet();
                timer_ms = cooldown_ms;
            }
        }
        if (timer_ms > 0)
        {
            timer_ms -= Time.deltaTime * 1000;
        }

    }
    public void shootBullet()
    {
        //Bullet gets called with position/rotation of gun object
        GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        //Debug.Log("Bullet Speed : " + bulletPrefab.GetComponent<bullet>().Speed);
    }
}
