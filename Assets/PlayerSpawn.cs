using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawn : MonoBehaviour
{
    public static int livesLeft;
    public GameObject playerPrefab;
    public Text livesText;
    public Text messageText;
    private float initialSpawnDelay = 2.0f;
    private float spawnDelay = 3.0f;
    private float delayleft;
    private bool isPaused;

    void Start()
    {
        delayleft = initialSpawnDelay;
        messageText.text = "get ready!";
        PlayerController playerController = playerPrefab.GetComponent<PlayerController>();
        livesLeft = playerController.lives;
        livesText.text = livesLeft.ToString();
    }

    private void Update()
    {
        if (messageText.text.Length > 0 && !isPaused)
        {
            UpdateSpawnDelay();
        }
        else
        {
            if (PlayerIsDead())
            {
                HandlePlayerDeath();
            } else if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }

    private void HandlePlayerDeath()
    {
        if (livesLeft > 0)
        {
            livesLeft -= 1;
            livesText.text = livesLeft.ToString();
            string lifeWord = livesLeft == 1 ? " life" : " lives";
            string message = livesLeft > 0 ? (livesLeft.ToString() + lifeWord + " remaining!") : "last life!";
            messageText.text = message;
            delayleft = spawnDelay;
        }
        else
        {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().GameOver();
        }
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            messageText.text = "";
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;
            messageText.text = "Paused";
        }
    }

    private void UpdateSpawnDelay()
    {
        if (delayleft > 0)
        {
            delayleft -= Time.deltaTime;
        }
        else
        {
            SpawnPlayer();
            messageText.text = "";
        }
    }

    bool PlayerIsDead()
    {
        foreach (Transform childPosition in transform)
        {
            if (childPosition.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnPlayer()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(playerPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }
}
