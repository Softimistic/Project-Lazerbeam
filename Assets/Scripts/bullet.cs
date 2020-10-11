using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class bullet : MonoBehaviour
{ 
    private Rigidbody rb;
    public float speed;
    
    // Start is called before the first frame update  
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        StartCoroutine(SelfDestruct());
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(speed);
        Destroy(gameObject);
    }
}