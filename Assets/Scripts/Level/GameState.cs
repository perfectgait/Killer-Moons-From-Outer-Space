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

    private bool nextSceneTriggered = false;

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

        if (EnemiesHaveFinishedSpawning() && AllEnemiesAreDestroyed() && PlayerIsAlive() && !nextSceneTriggered)
        {
            StartCoroutine(LoadNextSceneAfterWaitTime());
        }
    }

    private IEnumerator LoadNextSceneAfterWaitTime()
    {
        // Set this flag so that we don't keep triggering this corouting
        nextSceneTriggered = true;
        audioManager.StopCurrentlyPlayingMusic();

        // Allow some silence to listen to an explosion
        yield return new WaitForSeconds(1f);

        // Play Victory theme and wait for it to finish
        audioManager.PlayMusic("Victory");
        yield return new WaitForSeconds(3.9f);
        levelLoader.LoadNextLevelWithTransition();
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
