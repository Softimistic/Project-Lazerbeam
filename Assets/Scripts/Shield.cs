﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    private int maxShield;
    private int shield;
    [SerializeField] private GameObject visualShield;
    public AudioSource shieldActive;
    // shieldText changes te text on screen
    Text shieldText;
    public AudioClip shieldBroken;
    public Slider shieldBar;


    /// Start is called before the first frame update
    void Start()
    {
        // Calling the Text in Canvas.text
        shieldText = GetComponent<Text>();
        shield = 0;
        maxShield = 100;
        shieldBar.value = shield;
        shieldBar.maxValue = maxShield;
        visualShield.SetActive(false);
        shieldActive = GetComponent<AudioSource>();
    }

    void Update()
    {
        updateUI();
    }

    private void updateUI()
    {
        shieldBar.value = shield;
    }

    /// <summary>
    /// This is a global method to call at every class. You can call this to add the score.
    /// </summary>
    public void ShieldHit(int shieldDecrease)
    {
        if (shield - shieldDecrease <= 0)
        {
            shield = 0;
            shieldActive.Stop();
            if (visualShield.activeSelf)
            {
                
                AudioSource.PlayClipAtPoint(shieldBroken, transform.position);
            }
            visualShield.SetActive(false);
        }
        else
        {
            shield = shield - shieldDecrease;
        }
    }

    public void ShieldAdd(int shieldIncrease)
    {
        if (shield + shieldIncrease > maxShield)
        {
            shield = maxShield;
        }
        else
        {
            shield = shield + shieldIncrease;
        }

        if (!visualShield.activeSelf)
        {
            shieldActive.Play();
            visualShield.SetActive(true);
        }
    }

    public void setShieldToZero(int setShield)
    {
        shield = setShield;
    }

    public int getShieldInt()
    {
        return shield;
    }

    public string getShield()
    {
        return shield.ToString();
    }

    public void Pause()
    {
        shieldActive.Stop();
    }

    public void UnPause()
    {
        if (shield > 0)
            shieldActive.Play();
    }

}
