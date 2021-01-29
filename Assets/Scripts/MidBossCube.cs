using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MidBossCube : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //rotate plain
        transform.Rotate (Vector3.forward,-60f * Time.deltaTime);
        //check if eye boss dead
        if (transform.root.childCount == 2)
        {
            Invoke("LoadNextScene",5);
        }
    }

    private void LoadNextScene()
    { 
        FindObjectOfType<AudioManager>().PauseAll();
        //Update TempScore
        FindObjectOfType<ScoreHolder>().UpdateTempScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}


