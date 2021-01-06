using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Fly()
    {
        SceneManager.LoadScene(5);
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().PauseAll();
        FindObjectOfType<AudioManager>().Play("Theme");
    }

    public void Quit()
    {
        Debug.Log("Game Quit");
        //Application.Quit();
    }
}
