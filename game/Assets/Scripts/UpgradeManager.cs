using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Player player; // Ссылка на скрипт Player
    public TextMeshProUGUI totalScienceText; // Ссылка на текст с общим количеством TotalScience
    public TextMeshProUGUI priceForUpgrade; // Ссылка на текст на кнопку Апгрейд
    public TextMeshProUGUI hpText; // Ссылка на текст для отображения HP
    public TextMeshProUGUI damageText; // Ссылка на текст для отображения damage
    public TextMeshProUGUI speedText; // Ссылка на текст для отображения speed
    public int upgradeCost = 2000; // Стоимость улучшения

    private void Start()
    {
        // Обновляем текст с общим количеством TotalScience при загрузке сцены
        UpdateTotalScienceText();
        UpdatePlayerStatsText();
    }

    void Update()
    {
        priceForUpgrade.text = upgradeCost.ToString();
    }

    public void UpgradePlayer()
    {
        // Проверяем, хватает ли TotalScience для улучшения
        if (GameManager.TotalScience >= upgradeCost)
        {
            // Уменьшаем TotalScience на стоимость улучшения
            GameManager.TotalScience -= upgradeCost;

            // Улучшаем характеристики персонажа
            player.HP += 200;
            player.damage += 50;
            player.speed += 0.5f;

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
        hpText.text = player.HP.ToString();
        damageText.text = player.damage.ToString();
        speedText.text = player.speed.ToString();
    }
}
