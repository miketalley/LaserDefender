using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private const string LEVEL_NAME_PREFIX = "Level_";
    private const int MAX_LEVEL = 3;

    public static int currentLevel;

    public void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadScene (string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void StartNewGame ()
    {
        currentLevel = 0;
        LoadNextLevel();
    }

    public void Retry ()
    {
        string nextLevel = LEVEL_NAME_PREFIX + currentLevel;
        SceneManager.LoadScene(nextLevel);
    }

    public void LoadNextLevel ()
    {
        currentLevel++;
        
        if (currentLevel <= MAX_LEVEL)
        {
            string nextLevel = LEVEL_NAME_PREFIX + currentLevel;
            SceneManager.LoadScene(nextLevel);
        } else
        {
            SceneManager.LoadScene("Win");
        }
    }

    public void Quit ()
    {
        Application.Quit();
    }
}
