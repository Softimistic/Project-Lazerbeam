using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletHit : SceneTransitionEvent
{
    [SerializeField] private GameObject deathFx;
    [SerializeField] private AudioClip hitSoundFx;
    [SerializeField] private bool disableObstacleCollision;

    /// <summary>
    /// the color while the object is hit by bullets
    /// </summary>
    [SerializeField] private Color hitColor;

    /// <summary>
    /// after being hit by bullets for hitTimes, will trigger some thing
    /// </summary>
    [SerializeField] [Range(0, 100)] private int hitTimes;

    [Tooltip("Load a scene on hit")] [SerializeField] private bool SceneLoaderMode;

    private MeshRenderer _meshRenderer;
    private Color _originalColor;
    private ScoreBoard scoreBoard;
    private int _currentHitTimes = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        //init Renderer & originColor
      _meshRenderer = GetComponent<MeshRenderer>();
      _originalColor = _meshRenderer.material.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //trigger
        if (_currentHitTimes >= hitTimes && hitTimes != 0)
            //if(Input.GetMouseButtonUp(0))
        {
            //do sth here
            if (gameObject.CompareTag("BossComponent"))
            {
                if (transform.parent.childCount == 3)
                {
                    Destroy(gameObject.transform.parent.gameObject);
                    if (deathFx)
                    {
                        GameObject nwFx = Instantiate(deathFx, transform.position, Quaternion.identity);
                        FxSelfDestroy(nwFx);
                    }
                }
            }
            else if (gameObject.CompareTag("EnemyShips")){
                StartCoroutine(SelfDestroy());
                if (deathFx)
                {
                    GameObject nwFx = Instantiate(deathFx, transform.position, Quaternion.identity);
                    FxSelfDestroy(nwFx);
                    gameObject.GetComponentInChildren<Enemy>().OnDeath();
                }
            }
            else
            {
                StartCoroutine(SelfDestroy());
                if (deathFx)
                {
                    GameObject nwFx = Instantiate(deathFx, transform.position, Quaternion.identity);
                    FxSelfDestroy(nwFx);
                }
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("bullet") && this.enabled)
        {
            if (SceneLoaderMode)
            {
                LoadNextScene();
            }
            else
            {
                AudioSource.PlayClipAtPoint(hitSoundFx, transform.position);
                transform.GetComponentInParent<MidBossBody>().DecreaseBossHitTimes();
                _currentHitTimes++;
                //do sth here(eg: AUDIO)
                _meshRenderer.material.color = hitColor;
                StartCoroutine(RestoreColor(_originalColor));
                //Destory bullets
                Destroy(collision.gameObject);
                scoreBoard.ScoreHit(10);
            }
        }
        else if ((collision.gameObject.CompareTag("ParryObject") || collision.gameObject.CompareTag("Mine") || collision.gameObject.CompareTag("InstaKill")) && !disableObstacleCollision)
        {
            StartCoroutine(SelfDestroy());
            if (deathFx)
            {
                GameObject nwFx = Instantiate(deathFx, transform.position, Quaternion.identity);
                FxSelfDestroy(nwFx);
            }
            if (collision.gameObject.CompareTag("Mine"))
            {
                collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
                collision.gameObject.GetComponent<MeshCollider>().enabled = false;
                collision.gameObject.GetComponentInChildren<ParticleSystem>().Play();
                collision.gameObject.GetComponentInChildren<AudioSource>().Play();
            }
        }
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForEndOfFrame();
        //do sth here(eg: explode effect/animation)
        Destroy(gameObject);
    }


    private void FxSelfDestroy(GameObject nwFx)
    {
        ParticleSystem parts = nwFx.GetComponent<ParticleSystem>();
        //get the play time
        float totalDuration = parts.duration + parts.startLifetime;
        // delete 
        Destroy(nwFx, totalDuration);
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