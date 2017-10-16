using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public Text tryAgain;
    public Text quit;

    public void LoadLevel(string scene)
    {
        ClearText();
        SceneManager.LoadScene(scene);
    }

    public void LoadScene (string scene)
    {
        ClearText();
        SceneManager.LoadScene(scene);
    }

    public void StartNewGame ()
    {
        ScoreKeeper scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        scoreKeeper.score = 0;
        LoadLevel("Game");
    }

    public void RestartLevel()
    {
        LoadLevel("Game");
    }

    public void GameOver()
    {
        tryAgain.text = "try again";
        quit.text = "quit";
    }

    public void Quit ()
    {
        Application.Quit();
    }

    public void ClearText()
    {
        if (tryAgain)
        {
            tryAgain.text = "";
        }
        if (quit)
        {
            quit.text = "";
        }
    }
}
