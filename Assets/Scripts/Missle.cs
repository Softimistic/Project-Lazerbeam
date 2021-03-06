﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public AudioClip missleSound;
    public Transform target;
    public Vector3 direction;

    //float speed = 50;
    //float rotationSpeed = 5;
    private float rocketTurnSpeed;
    private float rocketSpeed;
    private float randomOffset;

    private float timerSinceLaunch_Contor = 0;
	private float objectLifeTimerValue = 5;

    public GameObject boostTrail;
    public GameObject explosion;

    private void Start()
    {
        rocketTurnSpeed = 50.0f;
        rocketSpeed = 60f;
        randomOffset = 0.0f;
        target = GameObject.Find("Player Ship").transform;
        AudioSource.PlayClipAtPoint(missleSound, transform.position);

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
                    randomOffset = 100f;
                    rocketTurnSpeed = 150f;
                }
                else
                {
                    randomOffset = 5.5f;
                    if((target.position - transform.position).magnitude < 10)
                    {
                        rocketSpeed = 100.0f; 
                        rocketTurnSpeed = 70f;
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

        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
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
            GameObject nwFx = Instantiate(explosion, transform.position, Quaternion.identity);
            FxSelfDestroy(nwFx);

        }
    }

    private void FxSelfDestroy(GameObject nwFx)
    {
        ParticleSystem parts = nwFx.GetComponent<ParticleSystem>();
        //get the play time
        float totalDuration = parts.duration + parts.startLifetime;
        //delete 
        Destroy(nwFx, totalDuration);
    }

   

}
