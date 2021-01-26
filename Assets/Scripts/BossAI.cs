﻿using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    public enum BossState
    {
        ENGAGE,
        WAITING,
    }
    
    private enum BossStage
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4
    }

    [Header("System Settings")]
    public float targetDistance;
    public Transform camera; //The object that AI around 
    public Transform player;
    public BetterWaypointFollower circut;

    [Header("Combat")]
    public AudioSource LaserSoundEffect;
    public GameObject[] turrets;
    public GameObject lazer;
    public float lazerAttackPeriod;
    public int lazerBeamNum;
    [Tooltip("Angular velocity in degrees per seconds")]public float degPerSec = 60.0f;

    [Header("Coloring")] 
    [SerializeField] [Range(0f, 1f)] float lerpTime;
    [SerializeField] Color[] finalColors;
    
    private float lazerAttackTimer = 0;
    private float angled;
    private BossState _bossState = BossState.WAITING;
    private MeshRenderer _meshRenderer;
    private int len;
    private float t;
    private int colotIndex;
    private BossStage _bossStage = BossStage.Stage1;
    public Slider healthBarBoss;
    public float bossHitTimes;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        len = finalColors.Length;
        healthBarBoss.value = bossHitTimes;
        healthBarBoss.maxValue = bossHitTimes;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updateUI();
        #region Combat
        if (_bossState == BossState.WAITING)
        {
            FollowCamera();
        }
        else
        {
            Movement();
            CircleMovement();
        }

        #endregion
        #region Color
        _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, finalColors[colotIndex],
            lerpTime * Time.deltaTime);
        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            colotIndex++;
            colotIndex = (colotIndex >= len) ? 0 : colotIndex;
        }

        #endregion
    }

    private void LazerUlt()
    {
        lazerAttackTimer += Time.deltaTime;
        if (lazerAttackTimer > lazerAttackPeriod)
        {
            Debug.Log(lazerAttackTimer);
            //TODO turn on turret
            foreach (GameObject eachTurret in turrets)
            {
                if (eachTurret)
                {
                    int i = lazerBeamNum;
                    while (i != 0)
                    {
                        if (eachTurret.transform.GetChild(0))
                        {
                            eachTurret.transform.GetChild(0).GetComponent<CreateBossLazer>().Shoot(lazer, player);
                            LaserSoundEffect.Play();
                            i--;
                        }
                    }
                }
            }
            //reset timer
            lazerAttackTimer = 0;
        }
    }

    private void Movement()
    {
        //TODO collision detection
        
    }

    private void CircleMovement()
    {
        FaceAtPlayer();
        LazerUlt();
        // switch (_bossStage)
        // {
        //     case BossStage.Stage1:
        //         if (turrets[0] == null)
        //         {
        //             Debug.Log("stage 1 finish!");
        //             _bossStage = BossStage.Stage2;
        //         }
        //         else
        //         {
        //             //make Boss face at Player
        //             FaceAtPlayer();
        //             LazerUlt();
        //         }
        //         break;
        //     case BossStage.Stage2:
        //         Debug.Log("enter stage 2");
        //         FaceAtPlayer();
        //         
        //         break;
        //     default:
        //         break;
        // }
        // keep going circle movement
        if (!circut.MoveDir().Equals(Vector3.zero))
        {
            transform.RotateAround(camera.position, circut.MoveDir(), degPerSec * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(player.position,Vector3.forward, degPerSec * Time.deltaTime * 10f);
        }
    }

    private void FollowCamera()
    {
        //check if Player enter the attack range
        if (_bossState != BossState.ENGAGE &&
            Vector3.Distance(transform.position, player.position) <= targetDistance)
        {
            _bossState = BossState.ENGAGE;
            transform.parent = camera;
        }
        else
        {
            _bossState = BossState.WAITING;
        }
    }

    private void FaceAtPlayer()
    {
        //make Boss lookat Player
        Vector3 direction = (player.position) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    public void updateUI()
    {
        healthBarBoss.value = bossHitTimes;
    }

    public void DecreaseBossHitTimes()
    {
        bossHitTimes--;
    }
}