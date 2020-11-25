using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    
    public Transform target;
    public Vector3 direction;

    //float speed = 50;
    //float rotationSpeed = 5;
    private float rocketTurnSpeed;
    private float rocketSpeed;
    private float randomOffset;

    private float timerSinceLaunch_Contor = 0;
	private float objectLifeTimerValue = 5;

    private void Start()
    {
        rocketTurnSpeed = 50.0f;
        rocketSpeed = 45f;
        randomOffset = 0.0f;
        target = GameObject.Find("Player Ship").transform;
        
    }

    void Update()
    {
        timerSinceLaunch_Contor += Time.deltaTime;
        if (target != null)
        {
            if(timerSinceLaunch_Contor > 1)
            {
                if((target.position - transform.position).magnitude > 50)
                {
                    randomOffset = 100.0f;
                    rocketTurnSpeed = 90.0f;
                }
                else
                {
                    randomOffset = 5f;
                    if((target.position - transform.position).magnitude < 10)
                    {
                        rocketSpeed = 180.0f;
                    }
                }
            }

            direction = target.position - transform.position + Random.insideUnitSphere * randomOffset;
            direction = direction.normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rocketTurnSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * rocketSpeed * Time.deltaTime);
        }

        if (timerSinceLaunch_Contor > objectLifeTimerValue)
        {
            StartCoroutine(SelfDestruct());
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
