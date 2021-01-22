using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionEvent : MonoBehaviour
{
    [Tooltip("Scene that will be loaded")] public string TargetScene; //Assign in editor
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().PauseAll();
            //Update TempScore
            FindObjectOfType<ScoreHolder>().UpdateTempScore();
            LoadNextScene();
        }
    }

    protected void LoadNextScene()
    {
        SceneManager.LoadScene(TargetScene);
    }
}
