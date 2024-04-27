using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    // Префабы юнитов
    public GameObject roguePrefab;
    public int rogueCost = 3; // Стоимость юнита Rogue

    public GameObject tankPrefab;
    public int tankCost = 5; // Стоимость юнита Tank

    public GameObject warriorPrefab;
    public int warriorCost = 4; // Стоимость юнита Warrior

    public GameObject rangerPrefab;
    public int rangerCost = 6; // Стоимость юнита Ranger

    public Transform[] spawnPositions; // Массив позиций, где будет создаваться юнит

    public CoinManager coinManager;
    public TextMeshProUGUI coinCountText;

    // Метод спавна юнита
    public void SpawnUnit(GameObject unitPrefab, int cost)
    {
        // Если у игрока достаточно монет для покупки юнита
        if (coinManager.coinCount >= cost)
        {
            // Уменьшаем количество монет на стоимость юнита
            coinManager.coinCount -= cost;

            // Обновляем текст с количеством монет в CoinManager
            coinManager.UpdateCoinCountText();

            // Выбираем случайную позицию из массива
            int randomIndex = Random.Range(0, spawnPositions.Length);
            Transform selectedSpawnPoint = spawnPositions[randomIndex];

            // Создаем объект юнита на выбранной позиции
            GameObject unitInstance = Instantiate(unitPrefab, selectedSpawnPoint.position, Quaternion.identity);

            // Если у объекта юнита есть компонент HealthBar, активируем его
            HealthBar healthBar = unitInstance.GetComponentInChildren
                <HealthBar>();

            // Если компонент Healthbar найден
            if (healthBar != null)
            {
                // Активируем объект Healthbar (Slider)
                healthBar.gameObject.SetActive(true);
            }
        }
    }

    // Методы спавна конкретных юнитов
    public void SpawnRogue()
    {
        SpawnUnit(roguePrefab, rogueCost);
    }

    public void SpawnTank()
    {
        SpawnUnit(tankPrefab, tankCost);
    }

    public void SpawnWarrior()
    {
        SpawnUnit(warriorPrefab, warriorCost);
    }

    public void SpawnRanger()
    {
        SpawnUnit(rangerPrefab, rangerCost);
    }
}
