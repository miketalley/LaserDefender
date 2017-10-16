using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width = 12f;
    public float height = 5f;
    public float speed = 4f;
    public float spawnDelay = 0.5f;
    public Text message;

    private float timer = 3.0f;
    private bool gameStarted;
    private float xmin;
    private float xmax;

	// Use this for initialization
	void Start () {
        gameStarted = false;
        float distance = transform.position.z - Camera.main.transform.position.z;
        float padding = width / 2;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftMost.x + padding;
        xmax = rightMost.x - padding;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    void Update () {
        if (gameStarted)
        {
            UpdateEnemyPositions();
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                gameStarted = true;
                SpawnAllEnemies();
            }
        }
    }

    void UpdateEnemyPositions()
    {
        this.transform.position += new Vector3((speed * Time.deltaTime), 0, 0);
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        if (newX == xmax || newX == xmin)
        {
            speed = -speed;
        }

        if (AllEnemiesDead())
        {
            SpawnUntilFull();
        }
    }

    void SpawnUntilFull ()
    {
        Transform position = NextFreePosition();
        if (position)
        {
            SpawnEnemy(position);
            Invoke("SpawnUntilFull", spawnDelay);
        }
        else
        {
            CancelInvoke("SpawnUntilFull");
        }
    }

    void SpawnEnemy (Transform position)
    {
        if (position)
        {
            GameObject enemy = Instantiate(enemyPrefab, position.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = position;
        }
    }

    void SpawnAllEnemies()
    {
        foreach (Transform child in transform)
        {
            SpawnEnemy(child);
        }
    }

    bool AllEnemiesDead ()
    {
        foreach(Transform childPosition in transform)
        {
            if (childPosition.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    Transform NextFreePosition ()
    {
        foreach (Transform childPosition in transform)
        {
            if (childPosition.childCount == 0)
            {
                return childPosition;
            }
        }
        return null;
    }
}
