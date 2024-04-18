using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab; // Префаб объекта Player
    public Transform spawnPosition; // Позиция, где будет создаваться Player

    public int costOfUnit = 3; // Стоимость одного юнита в монетах

    public CoinManager coinManager;
    public TextMeshProUGUI coinCountText;

    public void SpawnPlayers()
    {
        // Если у игрока достаточно монет для покупки юнита
        if (coinManager.coinCount >= costOfUnit)
        {
            // Уменьшаем количество монет на стоимость юнита
            coinManager.coinCount -= costOfUnit;

            // Обновляем текст с количеством монет в CoinManager
            coinManager.UpdateCoinCountText();

            // Создаем объект Player на указанной позиции
            GameObject playerInstance = Instantiate(playerPrefab, spawnPosition.position, Quaternion.identity);

            // Получаем компонент Player из созданного объекта Player
            Player playerComponent = playerInstance.GetComponent<Player>();

            // Если компонент Player найден
            if (playerComponent != null)
            {
                // Получаем компонент Healthbar из объекта Player
                HealthBar healthBar = playerComponent.GetComponentInChildren<HealthBar>();

                // Если компонент Healthbar найден
                if (healthBar != null)
                {
                    // Активируем объект Healthbar (Slider)
                    healthBar.gameObject.SetActive(true);
                }
            }
        }
    }

}
