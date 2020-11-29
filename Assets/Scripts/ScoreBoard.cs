using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] int scorePerHit = 100;

    // Score begins at zero
    int score = 0;
    // scoreText changes te text on screen
    Text scoreText;

    /// Start is called before the first frame update
    void Start()
    {
        // Calling the Text in Canvas.text
        scoreText = GetComponent<Text>();
        // Converting the text from int to string
        scoreText.text = score.ToString();
        
    }
    void Update()
    {
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// This is a global method to call at every class. You can call this to add the score.
    /// </summary>
    public void ScoreHit(int scoreIncrease)
    {
        UnityEngine.Debug.Log("Scorehit function called");
        score = score + scoreIncrease;
        scoreText.text = score.ToString();

    }
}
