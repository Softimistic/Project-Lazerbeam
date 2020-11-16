using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBullet : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _originalPosition;
    public int Speed;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _originalPosition = transform.position;
        _rb.AddRelativeForce(new Vector3(0, Speed, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
