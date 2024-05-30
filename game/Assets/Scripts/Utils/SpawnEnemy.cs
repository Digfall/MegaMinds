using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemySpawn
{
    public GameObject[] enemyBase;
    public float spawnDelay;
    public float betweenEnemiesDelay = 0.5f;
    public int level;
}

public class SpawnEnemy : MonoBehaviour
{
    public EnemySpawn[] enemyWaves;
    public Transform[] spawnPositions;

    private bool canSpawn = true;

    void Start()
    {
        if (EnemyCastleIsAlive())
        {
            StartCoroutine(StartSpawnWithDelay());
        }
    }

    IEnumerator StartSpawnWithDelay()
    {
        yield return new WaitForSeconds(13f);
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        foreach (EnemySpawn wave in enemyWaves)
        {
            canSpawn = true;

            foreach (GameObject enemyPrefab in wave.enemyBase)
            {
                if (canSpawn)
                {
                    int randomIndex = Random.Range(0, spawnPositions.Length);
                    Transform selectedSpawnPoint = spawnPositions[randomIndex];

                    GameObject enemyObject = Instantiate(enemyPrefab, selectedSpawnPoint.position, Quaternion.identity);

                    EnemyBase enemyBase = enemyObject.GetComponent<EnemyBase>();
                    if (enemyBase != null)
                    {
                        enemyBase.level = wave.level;
                        enemyBase.ApplyLevelAdjustments();
                    }

                    yield return new WaitForSeconds(wave.betweenEnemiesDelay);
                }
            }

            canSpawn = false;

            yield return new WaitForSeconds(wave.spawnDelay);
        }
    }

    bool EnemyCastleIsAlive()
    {
        EnemyCastle enemyCastle = FindObjectOfType<EnemyCastle>();
        return enemyCastle != null;
    }
}
