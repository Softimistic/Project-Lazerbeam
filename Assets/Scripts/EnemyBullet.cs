using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private Rigidbody rb;
    private float speed;

    // Start is called before the first frame update  
    void Start()
    {
        speed = 20.0f;
        StartCoroutine(SelfDestruct());
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
