using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    

    // Health begins at 100
    int health = 100;
    // healthText changes te text on screen
    Text healthText;

    /// Start is called before the first frame update
    void Start()
    {
        // Calling the Text in Canvas.text
        healthText = GetComponent<Text>();
        // Converting the text from int to string
        healthText.text = health.ToString();

    }

    /// <summary>
    /// This is a global method to call at every class. You can call this to add the score.
    /// </summary>
    public void HealthHit(int healthDescrease)
    {
        health = health - healthDescrease;
        healthText.text = health.ToString();

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
