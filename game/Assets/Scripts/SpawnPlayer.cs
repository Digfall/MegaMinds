using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Добавьте эту директиву, если она ещё не добавлена

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab; // префаб объекта Player

    public Transform spawnPosition; // позиция, где будет создаваться Player

    public void SpawnPlayers()
    {
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
            else
            {
                Debug.LogError("Healthbar component not found on the playerPrefab.");
            }
        }
        else
        {
            Debug.LogError("Player component not found on the playerPrefab.");
        }
    }
}