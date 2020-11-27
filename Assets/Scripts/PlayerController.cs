using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    [Tooltip("in ms^-1")] [SerializeField] float speed = 20f;
    [Tooltip("in ms")] [SerializeField] float xRange = 5f;
    [Tooltip("in ms")] [SerializeField] float yRange = 3f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;
   
    [Header("Henk's abilities")]
    [Tooltip("FOV when braking")] public float brakeFOV = 50f;
    [Tooltip("FOV when boosting")] public float boostFOV = 90f;

    [Header("Effects")]
    public GameObject boostTrail;
    public GameObject boostPop;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    void Update()
    {
        // Boost
        if (Input.GetButton("Fire3"))
        {
            Boost(true);
        }

        if (Input.GetButtonUp("Fire3"))
        {
            Boost(false);
        }

        // Brake
        /*        if (Input.GetButtonDown("Fire1"))
                {
                    Brake(true);
                }

                if (Input.GetButtonUp("Fire1"))
                {
                    Brake(false);
                }*/

        // Barrel Roll
        /*       if (Input.GetButtonDown("TriggerL"))
               {
                   int dir = Input.GetButtonDown("TriggerL") ? -1 : 1;
                   QuickSpin(dir);
               }*/
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
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

    void Boost(bool boosting)
    {
        float cameraSpeed = this.transform.parent.GetComponent<BetterWaypointFollower>().routeSpeed;
        if (boosting)
        {
            boostPop.SetActive(true);
            boostTrail.SetActive(true);
            //boostTrail.Play();
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, boostFOV, 1f * Time.deltaTime);
            cameraSpeed = 75f;
            //Debug.Log("speed" + cameraSpeed);
            boostPop.GetComponent<TrailRenderer>().emitting = boosting;

        }
        else if(Camera.main.fieldOfView > 60f)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, 1f);
            //Debug.Log("else" + Camera.main.fieldOfView);
            cameraSpeed = 30f;
            //Debug.Log("speed else" + cameraSpeed);
            boostPop.SetActive(false);
            boostTrail.SetActive(false);
        }
    }
  
/*
    void Brake(bool braking)
    {
        //reduce speed
        //reduce FOV
    }*/

    /*   public void QuickSpin(int dir)
       {
           if (!DOTween.IsTweening(playerModel))
           {
               playerModel.DOLocalRotate(new Vector3(playerModel.localEulerAngles.x, playerModel.localEulerAngles.y, 360 * -dir), .4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
               barrel.Play();
           }
       }*/

}