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
            Debug.Log("updating score!");
            //Update TempScore
            FindObjectOfType<ScoreHolder>().UpdateTempScore();
            Debug.Log("From here!");
            Invoke("LoadNextScene",3);
            //LoadNextScene();
        }
    }

    protected void LoadNextScene()
    {
        FindObjectOfType<AudioManager>().PauseAll();
        SceneManager.LoadScene(TargetScene);
    }
}
