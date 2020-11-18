using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public enum BossState
        {
            ENGAGE,
            WAITING,
        }

        public float targetDistance;
        public Transform camera; //The object that AI around 
        public Transform player;
        public BetterWaypointFollower circut;

        [Tooltip("Angular velocity in degrees per seconds")]
        public float degPerSec = 60.0f;

        private float angled;
        private BossState _bossState = BossState.WAITING;

        // Update is called once per frame
        void Update()
        {
            //make Boss face at Player
            FaceAtPlayer();
            if (_bossState == BossState.WAITING)
            {
                FollowCamera();
            }
            else
            {
                Movement();
                CircleMovement();
            }
        }

        private void Movement()
        {
        }

        private void CircleMovement()
        {
            
            transform.RotateAround(camera.position, circut.MoveDir(), degPerSec * Time.deltaTime);
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
    }