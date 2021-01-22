using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHolder : MonoBehaviour
{

    public static ScoreHolder instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StoreScoreToDatabase()
    {
        if (GetCurrentScore() != 0)
        {
            FindObjectOfType<HighScoreTable>().AddNewScoreEntry(GetCurrentScore());
        }
    }

    public int GetCurrentScore()
    {
        GameObject stb = GameObject.FindWithTag("ScoreText");
        if (stb != null)
        {
            return int.Parse(stb.GetComponent<Text>().text);
        }

        return 0;
    }

    public void UpdateTempScore()
    {
        PlayerPrefs.SetInt("TempScore", GetCurrentScore());
        PlayerPrefs.Save();
    }

    public void ResetTempScore()
    {
        PlayerPrefs.DeleteKey("TempScore");
    }

    public int ReadTempScore()
    {
        int tempScore = PlayerPrefs.GetInt("TempScore");
        Debug.Log( tempScore + " 6523");
        if (tempScore != 0)
        {
            return tempScore;
        }
        else
        {
            return 0;
        }
    }
}
