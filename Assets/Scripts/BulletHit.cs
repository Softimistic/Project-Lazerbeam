using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletHit : MonoBehaviour
{ 
    [SerializeField] private Color hitColor;
    [SerializeField] private int hitTimes;
    private MeshRenderer _meshRenderer;
    private Color _originalColor;
    private int _currentHitTimes = 0;
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalColor = _meshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHitTimes >= hitTimes)
        {
            //do sth here
            StartCoroutine(SelfDestroy());
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            _currentHitTimes++;
            //do sth here(eg: AUDIO)
            _meshRenderer.material.color = hitColor;
            StartCoroutine(Delay(_originalColor));
            //Destory bullets
            Destroy(collision.gameObject);
        }
    }
    
    IEnumerator SelfDestroy()
    {
        yield return new WaitForEndOfFrame();
        //do sth here(eg: explode)
        Destroy(gameObject);
    }
    
    IEnumerator Delay(Color b) {
        while(_meshRenderer.material.color != b)
        {
            _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, b, Time.deltaTime);
            yield return 0;
        }
    }
}
