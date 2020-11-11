using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyCount = 1;
    [SerializeField] float waitTimeBetweenSpawn = 0;

    // TODO: Temporary variable for testing - likely not how we will organize levels
    [SerializeField] float startSpawningAfter = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator SpawnEnemies()
    {
        int spawnedEnemies = 0;
        yield return new WaitForSeconds(startSpawningAfter);
        while (spawnedEnemies < enemyCount)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waitTimeBetweenSpawn);
            spawnedEnemies++;
        }
    }
}
