using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBullet : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _originalPosition;
    private int _counter;
    public int Duration;
    private AudioSource _boom;
    public int Speed;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _boom = GetComponent<AudioSource>();
        _originalPosition = transform.position;
        _rb.AddRelativeForce(new Vector3(0, Speed, 0));
        _counter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _counter++;
        if (_counter > Duration)
        {
            transform.position = _originalPosition;
            _boom.Play();
            _counter = 0;
        }
    }
}
