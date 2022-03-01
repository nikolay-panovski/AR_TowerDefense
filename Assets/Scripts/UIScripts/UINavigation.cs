using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigation : MonoBehaviour
{
    public string home;
    public string play;
    public string tutorial;
    public string gameover;

    public void LoadHome()
    {
        SceneManager.LoadScene(home);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(play);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(tutorial);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene(gameover);
    }
}
