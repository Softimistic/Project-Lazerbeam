using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChildDeathChecker : SceneTransitionEvent
{
    [Tooltip("Disable GameObject instead of loading scene")] [SerializeField]
    public bool DisableMode;

    private int _counter = 0;
    private bool isTriggered = false;

    void FixedUpdate()
    {
        if (transform.childCount == 0)
        {
            if (SceneManager.GetActiveScene().name.Equals("FinalBossPhase2") && !isTriggered)
            {
                FindObjectOfType<ScoreHolder>().StoreScoreToDatabase();
                FindObjectOfType<ScoreHolder>().ResetTempScore();
                Invoke("LoadNextScene", 3);
                isTriggered = true;
            }
            else
            {
                if (DisableMode)
                {
                    this.gameObject.SetActive(false);
                }
                else
                {
                    if (!isTriggered)
                    {
                        //Update TempScore
                        Debug.Log("updating score!");
                        FindObjectOfType<ScoreHolder>().UpdateTempScore();
                        Invoke("LoadNextScene", 3);
                        isTriggered = true;
                    }
                }

                // if (_counter > 100)
                // { 
                //     LoadNextScene();
                // }
                // _counter++;
            }
        }
    }
}