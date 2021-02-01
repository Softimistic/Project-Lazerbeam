using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ToNextSceneAfterVideo : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public string SceneName;
    void Start()
    {
        #region controll music
        FindObjectOfType<AudioManager>().PauseAll();
        FindObjectOfType<AudioManager>().Play(FindObjectOfType<AudioManager>().GetCurrentThemeName(SceneManager.GetActiveScene().name));
        #endregion
        VideoPlayer.loopPointReached += LoadScene;
    }
    void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            SceneManager.LoadScene(SceneName);
        }
    }
    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneName);
    }
}
