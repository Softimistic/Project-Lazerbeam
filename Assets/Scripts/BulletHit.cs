using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletHit : MonoBehaviour
{
    /// <summary>
    /// the color while the object is hit by bullets
    /// </summary>
    [SerializeField] private Color hitColor;
    /// <summary>
    /// after being hit by bullets for hitTimes, will trigger some thing
    /// </summary>
    [SerializeField][Range(0,100)] private int hitTimes;

    private MeshRenderer _meshRenderer;
    private Color _originalColor;

    private int _currentHitTimes = 0;

    // Start is called before the first frame update
    void Start()
    {
        //init Renderer & originColor
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalColor = _meshRenderer.material.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //trigger
        if (_currentHitTimes >= hitTimes && hitTimes != 0)
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
            StartCoroutine(RestoreColor(_originalColor));
            //Destory bullets
            Destroy(collision.gameObject);
        }
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForEndOfFrame();
        //do sth here(eg: explode effect/animation)
        Destroy(gameObject);
    }

    IEnumerator RestoreColor(Color originColor)
    {
        while (_meshRenderer.material.color != originColor)
        {
            _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, originColor, Time.deltaTime);
            yield return 0;
        }
    }
}