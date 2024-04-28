using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public PlayerWarrior playerWarrior; // Ссылка на скрипт Player
    public TextMeshProUGUI totalScienceText; // Ссылка на текст с общим количеством TotalScience
    public TextMeshProUGUI priceForUpgrade; // Ссылка на текст на кнопку Апгрейд
    public TextMeshProUGUI hpText; // Ссылка на текст для отображения HP
    public TextMeshProUGUI damageText; // Ссылка на текст для отображения damage
    public TextMeshProUGUI speedText; // Ссылка на текст для отображения speed
    public int upgradeCost = 100; // Стоимость улучшения

    private void Start()
    {
        // Обновляем текст с общим количеством TotalScience при загрузке сцены
        UpdateTotalScienceText();

        // Загружаем сохраненные характеристики игрока
        LoadPlayerStats();

        // Обновляем тексты с характеристиками игрока
        UpdatePlayerStatsText();
    }
    private void LoadPlayerStats()
    {
        // Загружаем сохраненные значения характеристик из PlayerPrefs
        playerWarrior.HP = PlayerPrefs.GetInt("WarriorHP", playerWarrior.HP);
        playerWarrior.damage = PlayerPrefs.GetInt("WarriorDamage", playerWarrior.damage);
        playerWarrior.speed = PlayerPrefs.GetFloat("WarriorSpeed", playerWarrior.speed);
    }

    void Update()
    {
        priceForUpgrade.text = upgradeCost.ToString();
        UpdatePlayerStatsText();
    }

    public void UpgradePlayer()
    {
        // Проверяем, хватает ли TotalScience для улучшения
        if (GameManager.TotalScience >= upgradeCost)
        {
            // Уменьшаем TotalScience на стоимость улучшения
            GameManager.TotalScience -= upgradeCost;

            // Улучшаем характеристики персонажа
            playerWarrior.HP += 300;
            playerWarrior.damage += 50;

            // Сохраняем улучшенные характеристики
            playerWarrior.SavePlayerStats();

            // Обновляем отображение TotalScience и характеристик игрока
            UpdateTotalScienceText();
            UpdatePlayerStatsText();
        }
        else
        {
            // Если TotalScience недостаточно, выводим сообщение об этом
            Debug.Log("Недостаточно TotalScience для улучшения.");
        }
    }


    private void UpdateTotalScienceText()
    {
        // Обновляем текст с общим количеством TotalScience
        totalScienceText.text = GameManager.TotalScience.ToString();
    }

    private void UpdatePlayerStatsText()
    {
        // Обновляем тексты с характеристиками игрока
        hpText.text = playerWarrior.HP.ToString();
        damageText.text = playerWarrior.damage.ToString();
    }
}
