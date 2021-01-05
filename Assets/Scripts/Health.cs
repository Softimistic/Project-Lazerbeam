using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int maxHealth;
    private int health;
    // healthText changes te text on screen
    Text healthText;

    /// Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        health = maxHealth;
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

    public void SetHealthToZero(int setHealth)
    {
        health = setHealth;
    }

    public int GetHealthInt()
    {
        return health;
    }

    public string GetHealth()
    {
        return health.ToString();
    }
}
