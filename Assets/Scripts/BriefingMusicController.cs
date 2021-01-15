using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BriefingMusicController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region controll music
        FindObjectOfType<AudioManager>().PauseAll();
        FindObjectOfType<AudioManager>().Play(FindObjectOfType<AudioManager>().GetCurrentThemeName(SceneManager.GetActiveScene().name));
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
