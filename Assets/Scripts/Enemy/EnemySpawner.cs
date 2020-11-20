using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyCount = 1;
    [SerializeField] float waitTimeBetweenSpawn = 0;
    [SerializeField] float startSpawningAfter = 0;

    public bool isSpawning = false;
    public bool hasFinishedSpawning = false;

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
        isSpawning = true;
        while (spawnedEnemies < enemyCount)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waitTimeBetweenSpawn);
            spawnedEnemies++;
        }
        isSpawning = false;
        hasFinishedSpawning = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Sprite enemySprite = enemyPrefab.GetComponent<SpriteRenderer>().sprite;
        //Gizmos.DrawIcon(transform.position, AssetDatabase.GetAssetPath(enemySprite), true);
    }
#endif
}
