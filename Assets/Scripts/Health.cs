using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health;
    // healthText changes te text on screen
    Text healthText;

    /// Start is called before the first frame update
    void Start()
    {
        // Calling the Text in Canvas.text
        healthText = GetComponent<Text>();
        // Converting the text from int to string
        //healthText.text = health.ToString();

    }

    /// <summary>
    /// This is a global method to call at every class. You can call this to add the score.
    /// </summary>
    public void HealthHit(int healthDescrease)
    {
        health = health - healthDescrease;
    }

    public void HealthAdd(int healthIncrease)
    {
        if(health + healthIncrease > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health = health + healthIncrease;
        }    
    }

    public void setHealthToZero(int setHealth)
    {
        health = setHealth;
    }

    public string getHealth()
    {
        return health.ToString();
    }
}
