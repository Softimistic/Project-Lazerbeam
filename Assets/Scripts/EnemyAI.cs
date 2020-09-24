using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float targetDistance = 10.0f;

    public float enemySpeed = 0.0f;

    public GameObject plane;
    
    public Transform aroundPoint;//围绕的物体
    public float angularSpeed;//角速度
    public float aroundRadius;//半径
    [Tooltip("Angular velocity in degrees per seconds")]
    public float degPerSec = 60.0f;

    [Tooltip("Rotation axis")]
    public Vector3 rotAxis = Vector3.up;

    private float angled;
    // Start is called before the first frame update
    void Start()
    {
        //設置物體初始位置為圍繞物體的正前方距離為半徑的點
        // Vector3 p = aroundPoint.rotation * Vector3.forward * aroundRadius;
        // transform.position = new Vector3(p.x, aroundPoint.position.y, p.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        //GameObject plane = GameObject.FindGameObjectWithTag("PlayerPlane");
        //if the player's plane pass the certain line, the enemy plane will spawn
        if (transform.position.z - plane.transform.position.z <= targetDistance)
        {
            Vector3 newPosition = transform.position;
            angled += (angularSpeed * Time.deltaTime) % 360;//累加已經轉過的角度
            newPosition.x = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);//計算x位置
            newPosition.y = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);//計算y位置
            newPosition.z = plane.transform.position.z + targetDistance;
            transform.position = newPosition ;
             transform.Rotate(rotAxis, degPerSec * Time.deltaTime);
        }
        else
        {
            transform.position += transform.forward * enemySpeed * Time.deltaTime;
        }
    }
}