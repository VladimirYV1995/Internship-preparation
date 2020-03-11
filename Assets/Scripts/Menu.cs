using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{     
    public void PlayGame()
    {
        if (SceneManager.GetActiveScene().name != "Game")
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void CallMenu()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
