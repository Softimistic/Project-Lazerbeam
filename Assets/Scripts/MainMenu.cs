using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Fly()
    {
        SceneManager.LoadScene(1);
    }


    public void LoadLeaderBoard()
    {
        Debug.Log("SHOW UP LEADERBOARD!");
    }

    public void Quit()
    {
        Debug.Log("Game Quit");
        //Application.Quit();
    }
}
