using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Microsoft.SqlServer.Server;
using UnityEngine;
using Vectrosity;

public class EyeBall : MonoBehaviour
{
    public GameObject bullet;
    private GameObject player;

    private Vector3 preDir;

    private Vector3 playerCurrentPos;

    private bool isShooting = false;
    
    private enum EyeBallState
    {
        AIMING,
        SHOOT
    }

    private EyeBallState currentState = EyeBallState.AIMING;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        InvokeRepeating("SwitchState",2f,5f);
    }

    private void SwitchState()
    {
        if (currentState == EyeBallState.AIMING)
        {
            currentState = EyeBallState.SHOOT;
        }
        else
        {
            currentState = EyeBallState.AIMING;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FaceAtPlayer();
        if (currentState == EyeBallState.AIMING)
        {
            playerCurrentPos = GameObject.FindWithTag("Player").transform.position;
            Vector3 dir = playerCurrentPos - transform.position;
            VectorLine.SetRay3D(Color.green, Time.deltaTime, transform.position, dir);
        }
        else
        {
            if (!isShooting)
            {
                ShootRay();
            }
        }

        //AimRay();
    }
    

    private void FaceAtPlayer()
    {
        transform.LookAt(player.transform);
    }

    private void ShootRay()
    {
        isShooting = true;
        playerCurrentPos = GameObject.FindWithTag("Player").transform.position;
        Vector3 dir = transform.position - playerCurrentPos;
       //TODO shooting
       Debug.Log("射击！！！！！");
       bullet.transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(-90, 0, 0);
       Instantiate(bullet, transform.position,bullet.transform.rotation);
       Debug.Log("射击成功");
       Invoke("ChangeShootingState",0.1f);
      
    }

    private void ChangeShootingState()
    {
        isShooting = !isShooting;
    }
}