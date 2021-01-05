using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    private int maxShield;
    private int shield;
    [SerializeField] private GameObject visualShield;
    // shieldText changes te text on screen
    Text shieldText;

    /// Start is called before the first frame update
    void Start()
    {
        // Calling the Text in Canvas.text
        shieldText = GetComponent<Text>();
        shield = 0;
        maxShield = 100;
        visualShield.SetActive(false);
    }

    /// <summary>
    /// This is a global method to call at every class. You can call this to add the score.
    /// </summary>
    public void ShieldHit(int shieldDecrease)
    {
        if (shield - shieldDecrease <= 0)
        {
            shield = 0;
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
        visualShield.SetActive(true);
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
}
