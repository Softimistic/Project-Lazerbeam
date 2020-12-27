using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class HighScoreTable : MonoBehaviour
{
    private Transform _entryContainer;
    private Transform _enteryTemplate;
    private List<Transform> _highScoreEntryTransformList;

    private void Awake()
    {
        _entryContainer = transform.Find("HighScoreEntryContainer");
        _enteryTemplate = _entryContainer.Find("HighScoreEntryTemplate");
        //ResetDatabase();
        //AddNewScoreEntry(100000);
        _enteryTemplate.gameObject.SetActive(false);
        String jsonString = PlayerPrefs.GetString("ScoreTable");
        if (jsonString != "")
        {
            HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
            if (highScores.highscoreList.Count != 0)
            {
                //sort Scores desending
                highScores.highscoreList.Sort(
                    delegate(HighScoreEntry p1, HighScoreEntry p2) { return p2.score.CompareTo(p1.score); });
                _highScoreEntryTransformList = new List<Transform>();
                //render
                for (int i = 0; i < 5; i++)
                {
                    if (i < highScores.highscoreList.Count)
                    {
                        CreateHighScoreEntry(highScores.highscoreList[i], _entryContainer,
                            _highScoreEntryTransformList);
                    }
                }
            }
        }
    }

    private void ResetDatabase()
    {
        PlayerPrefs.DeleteKey("ScoreTable");
    }

    public void AddNewScoreEntry(int scoreToAdd)
    {
        //Create new ScoreEntry
        HighScoreEntry nwHighScoreEntry = new HighScoreEntry {score = scoreToAdd};
        //Load table
        String jsonString = PlayerPrefs.GetString("ScoreTable");
        if (jsonString != "")
        {
            HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString); //Add Score
            highScores.highscoreList.Add(nwHighScoreEntry);
            //Save
            string json = JsonUtility.ToJson(highScores);
            PlayerPrefs.SetString("ScoreTable", json);
            PlayerPrefs.Save();
        }
        else
        {
            HighScores nwHighScores = new HighScores
            {
                highscoreList = new List<HighScoreEntry>()
            };
            nwHighScores.highscoreList.Add(nwHighScoreEntry);
            //Save
            string json = JsonUtility.ToJson(nwHighScores);
            PlayerPrefs.SetString("ScoreTable", json);
            PlayerPrefs.Save();
        }
    }

    private void CreateHighScoreEntry(HighScoreEntry highScoreEntry, Transform container,
        List<Transform> transformsList)
    {
        float templateHeight = 35f;
        Transform entryTransform = Instantiate(_enteryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformsList.Count);
        entryTransform.gameObject.SetActive(true);
        //名次
        int rank = transformsList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;
        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
        int score = highScoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();
        transformsList.Add(entryTransform);
    }

    private class HighScores
    {
        public List<HighScoreEntry> highscoreList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
    }
}