using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public int Damage;
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private int Lifetime;

    // Start is called before the first frame update  
    void Start()
    {
        transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
        StartCoroutine(SelfDestruct());
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(Lifetime);
        Destroy(gameObject);
    }
}
