using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    [Header("Pause Menu ")] [SerializeField]
    public GameObject pauseMenu;

    [Header("Game Over Menu")] [SerializeField]
    public GameObject gameOverMenu;

    [Header("Player")] [SerializeField] public GameObject player;

    private bool _isPause;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverMenu.activeSelf)
        {
            if (!_isPause)
            {
                ActivePauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    /// <summary>
    /// Set time scale to default
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1f;
        ResumePlayerControl();
    }

    /// <summary>
    /// froze timescale so that game be stopped
    /// </summary>
    void Pause()
    {
        //Supposed to 0, but it will trigger Way point enter a infinite loop, in this case 0.01f is used
        Time.timeScale = 0.01f;
        DisablePlayerControl();
    }

    /// <summary>
    /// restart this level
    /// </summary>
    public void Replay()
    {
        CloseGameOverMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Show Pause menu
    /// </summary>
    public void ActivePauseMenu()
    {
        pauseMenu.SetActive(true);
        Pause();
        _isPause = true;
    }

    /// <summary>
    /// Close Pause Menu
    /// </summary>
    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        Resume();
        _isPause = false;
    }

    /// <summary>
    /// Close Game Over Menu
    /// </summary>
    public void CloseGameOverMenu()
    {
        gameOverMenu.SetActive(false);
        Resume();
    }

    /// <summary>
    /// Show Up Game Over Menu, Game will be paused
    /// </summary>
    public void ActiveGameOverMenu()
    {
        Pause();
        gameOverMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void DisablePlayerControl()
    {
        player.GetComponent<PlayerController>().enabled = false;
    }

    public void ResumePlayerControl()
    {
        player.GetComponent<PlayerController>().enabled = true;
    }

    public void Exit()
    {
        Debug.Log("quit game!");
        Application.Quit();
    }
}