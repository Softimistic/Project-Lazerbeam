using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{

    public Transform target;
    Vector3 direction;

    float speed = 50;
    float rotationSpeed = 5;

    private void Start()
    {
        target = GameObject.Find("Player Ship").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement of the missle
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //Rotation
        if(target != null)
        {
            direction = target.position - transform.position;
            direction = direction.normalized;
            // kijken naar de player
            var rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }
    }
}
