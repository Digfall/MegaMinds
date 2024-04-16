using System.Collections;
using UnityEngine;

[System.Serializable]
public class SpawnScenario
{
    public int enemyCount;
    public float spawnDelay;
}

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPosition;
    public SpawnScenario[] spawnScenarios;

    private float spawnTimer = 0f;
    private bool canSpawn = true; // Флаг для контроля спавна врагов

    void Update()
    {
        if (EnemyCastleIsAlive()) // Проверяем, что EnemyCastle не уничтожен
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnScenarios[0].spawnDelay)
            {
                SpawnRandomScenario();
                spawnTimer = 0f;
            }
        }
        else
        {
            canSpawn = false; // Если EnemyCastle уничтожен, останавливаем спавн
        }
    }

    void SpawnRandomScenario()
    {
        int randomIndex = Random.Range(0, spawnScenarios.Length);
        SpawnScenario randomScenario = spawnScenarios[randomIndex];

        if (canSpawn) // Проверяем флаг на возможность спавна
        {
            StartCoroutine(SpawnEnemies(randomScenario.enemyCount, randomScenario.spawnDelay));
        }
    }

    IEnumerator SpawnEnemies(int enemyCount, float spawnDelay)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Добавляем метод для проверки наличия EnemyCastle
    bool EnemyCastleIsAlive()
    {
        EnemyCastle enemyCastle = FindObjectOfType<EnemyCastle>();
        return enemyCastle != null;
    }
}
