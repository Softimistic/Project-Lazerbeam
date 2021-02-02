﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [Tooltip("in ms^-1")] [SerializeField] float speed = 20f;
    [Tooltip("in ms")] [SerializeField] float xRange = 5f;
    [Tooltip("in ms")] [SerializeField] float yRange = 3f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;
   
    [Header("Henk's abilities")]
    [Tooltip("FOV in it's default state")] public float defaultFOV = 60f;
    [Tooltip("FOV when braking")] public float brakeFOV = 50f;
    private bool _braking;
    [Tooltip("FOV when boosting")] public float boostFOV = 90f;
    private bool _boosting;
    [Header("Effects")]
    public GameObject boostTrail;
    public GameObject boostPop;

    public Slider BoostBar;
    public float Boost1;
    public float boostOverTime;
    public bool boostIsLeeg = false;
    public bool BossMode;
    public bool WarpSpeed;
    public float maxBoost = 100;
    public AudioForPlayer audio;
    public bool audioPlaying = false;
    


    float xThrow, yThrow;
    bool isControlEnabled = true;

    private void Start()
    {
        if (BoostBar)
        {
            BoostBar.maxValue = Boost1;
            updateUI();
        }

        
    }

    void Update()
    {
        // Boost
        if (Input.GetButton("Fire3") && !BossMode)
        {
            _boosting = true;
        }

        if (Input.GetButtonUp("Fire3") && !BossMode)
        {
            _boosting = false;
        }

        // Brake
        if (Input.GetButton("Fire1") && !BossMode)
        {
            _braking = true;
        }

        if (Input.GetButtonUp("Fire1") && !BossMode)
        {
            _braking = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            Boost();
            Brake();
            updateUI();
            if(Boost1 <= 0)
            {
                boostIsLeeg = true;
            }
            else
            {
                
                boostIsLeeg = false;
            }
        }
    }

    void OnPlayerDeath() // Uit CollisionHandler. Zorgt ervoor dat de controls niet meer bewegen. 
    {
        isControlEnabled = false;
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    public float Speed => speed;

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void Boost()
    {
        
        if (_boosting && !boostIsLeeg)
        {
            if (!audioPlaying)
            {
                audio.Play("Boosting");
                audioPlaying = true;
            }
            Boost1 -= boostOverTime * Time.deltaTime;
            boostPop.SetActive(true);
            boostTrail.SetActive(true);
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, boostFOV, 1f * Time.deltaTime);
            int warp = 1;
            if (WarpSpeed)
            {
                warp = 10;
            }

            transform.parent.GetComponent<BetterWaypointFollower>().routeSpeed = 75f * warp;
        }
        else if(mainCamera.fieldOfView > defaultFOV)
        {
            
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 60f, 1f * Time.deltaTime);
            transform.parent.GetComponent<BetterWaypointFollower>().routeSpeed = 30f;
            boostPop.SetActive(false);
            boostTrail.SetActive(false);
        }

        if(audioPlaying && !_boosting || audioPlaying && boostIsLeeg)
        {
            audio.Stop("Boosting");
            audioPlaying = false;
        }

        
    }

    void Brake()
    {
        if (_braking)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, brakeFOV, 1f * Time.deltaTime);
            transform.parent.GetComponent<BetterWaypointFollower>().routeSpeed = 10f; 
        }
        else if (mainCamera.fieldOfView < defaultFOV)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 60f, 1f * Time.deltaTime);
            transform.parent.GetComponent<BetterWaypointFollower>().routeSpeed = 30f;
        }
    }

    void updateUI()
    {
        if (BoostBar)
        {
            Boost1 = Mathf.Clamp(Boost1, 0, 100);
            BoostBar.value = Boost1;
        }
    }

    public void addBoost(int boostIncrease)
    {
        if(Boost1 + boostIncrease > maxBoost)
        {
            Boost1 = maxBoost;
        }
        else
        {
            Boost1 = Boost1 + boostIncrease;
        }
        
    }

    public bool IsBoosting()
    {
        
        return _boosting;
    }
    public bool IsBraking()
    {
        return _braking;
    }
}