using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    AudioManager audioManager;
    LevelLoader levelLoader;
    Health playerHealth;
    EnemySpawner[] enemySpawners;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        levelLoader = FindObjectOfType<LevelLoader>();
        var player = GameObject.Find("Player");
        playerHealth = player.GetComponent<Health>();

        GameObject enemySpawnerContainer = GameObject.Find("Enemy Spawners");
        enemySpawners = enemySpawnerContainer.GetComponentsInChildren<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinState();
    }

    private void CheckWinState()
    {
        // Give some buffer before checking win states
        if (Time.timeSinceLevelLoad < 2f)
        {
            return;
        }

        if (EnemiesHaveFinishedSpawning() && AllEnemiesAreDestroyed() && PlayerIsAlive())
        {
            StartCoroutine(LoadWinScreenAfterWaitTime());
        }
    }

    private IEnumerator LoadWinScreenAfterWaitTime()
    {
        audioManager.StopCurrentlyPlayingMusic();
        yield return new WaitForSeconds(2f);
        levelLoader.LoadWinScreen();
    }

    private bool PlayerIsAlive()
    {
        return playerHealth.GetHealth() > 0;
    }

    private bool AllEnemiesAreDestroyed()
    {
        return FindObjectsOfType<Enemy>().Length == 0;
    }

    private bool EnemiesHaveFinishedSpawning()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (!spawner.hasFinishedSpawning)
            {
                return false;
            }
        }

        return true;
    }
}
