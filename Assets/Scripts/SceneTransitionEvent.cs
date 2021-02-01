using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionEvent : MonoBehaviour
{
    [Tooltip("Scene that will be loaded")] public string TargetScene; //Assign in editor
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Update TempScore
            FindObjectOfType<ScoreHolder>().UpdateTempScore();
            Invoke("LoadNextScene",10);
        }
    }
    
    protected void LoadNextScene()
    {
        FindObjectOfType<AudioManager>().PauseAll();
        SceneManager.LoadScene(TargetScene);
    }
}
