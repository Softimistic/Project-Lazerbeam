using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private Rigidbody rb;
    public float speed = 100.0f;
    public int Lifetime;

    // Start is called before the first frame update  
    void Start()
    {
        StartCoroutine(SelfDestruct());
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(Lifetime);
        Destroy(gameObject);
    }
}
