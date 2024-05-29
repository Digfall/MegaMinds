using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemySpawn
{
    public GameObject[] enemyBase;
    public float spawnDelay;
    public float betweenEnemiesDelay = 0.5f; // Задержка между спавнами элементов enemyBase
}

public class SpawnEnemy : MonoBehaviour
{
    public EnemySpawn[] enemyWaves;
    public Transform[] spawnPositions; // Массив позиций, где будут создаваться враги

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
        yield return new WaitForSeconds(13f); // Задержка в 10 секунд перед началом спавна
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        foreach (EnemySpawn wave in enemyWaves)
        {
            canSpawn = true; // Установка флага перед каждой волной

            foreach (GameObject enemyPrefab in wave.enemyBase)
            {
                if (canSpawn)
                {
                    // Выбираем случайную позицию из массива spawnPositions
                    int randomIndex = Random.Range(0, spawnPositions.Length);
                    Transform selectedSpawnPoint = spawnPositions[randomIndex];

                    // Создаем объект врага на выбранной позиции
                    Instantiate(enemyPrefab, selectedSpawnPoint.position, Quaternion.identity);
                    yield return new WaitForSeconds(wave.betweenEnemiesDelay); // Задержка между спавнами элементов enemyBase
                }
            }

            canSpawn = false; // После спавна всех врагов в волне выключаем спавн

            // Если не последняя волна, ждем spawnDelay перед следующей
            yield return new WaitForSeconds(wave.spawnDelay);
        }
    }

    bool EnemyCastleIsAlive()
    {
        EnemyCastle enemyCastle = FindObjectOfType
        <EnemyCastle>();
        return enemyCastle != null;
    }
}
