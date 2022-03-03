using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigation : MonoBehaviour
{
    public string home = "UIHome";
    public string play = "AssetsScriptsTestScene";
    public string tutorial = "UITutorial";
    public string gameover = "UIGameOver";

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
