using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class bullet : MonoBehaviour
{ 
    private Rigidbody rb;
    float speed = 20.0f;

    // Start is called before the first frame update  
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        StartCoroutine(SelfDestruct());
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}