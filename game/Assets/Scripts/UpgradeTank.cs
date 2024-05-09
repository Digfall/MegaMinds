using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeTank : MonoBehaviour
{
    public PlayerTank playerTank;
    public TextMeshProUGUI totalScienceText;
    public TextMeshProUGUI priceForUpgrade;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageUpTextTank;
    public TextMeshProUGUI hpUpTextTank;


    private int currentLevel = 0;

    private const string CurrentLevelPrefKey = "CurrentLevel"; // Ключ для сохранения/загрузки текущего уровня

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs
        currentLevel = PlayerPrefs.GetInt(CurrentLevelPrefKey, 0);

        UpdateTotalScienceText();
        UpdateTankStatsText();
        DefineUpgradeLevels();
        UpgradePlayer(currentLevel);
        UpdatePriceForUpgrade(currentLevel);
    }

    void Update()
    {
        UpdateTankStatsText();
        UpdatePriceForUpgrade(currentLevel);
    }

    public void UpgradePlayer()
    {
        if (currentLevel < playerTank.upgradeLevels.Count - 1)
        {
            int upgradeCost = playerTank.upgradeLevels[currentLevel].costTank;

            if (GameManager.TotalScience >= upgradeCost)
            {
                GameManager.TotalScience -= upgradeCost;
                currentLevel++;
                UpgradePlayer(currentLevel);
                UpdateTotalScienceText();
                UpdateTankStatsText();
                UpdatePriceForUpgrade(currentLevel);

                // Сохраняем текущий уровень в PlayerPrefs
                PlayerPrefs.SetInt(CurrentLevelPrefKey, currentLevel);
            }
            else
            {
                Debug.Log("Недостаточно TotalScience для улучшения.");
            }
        }
        else
        {
            Debug.Log("Игрок достиг максимального уровня прокачки.");
        }
    }
    public void LoadPlayerStats()
    {
        // Загружаем сохраненные значения характеристик из PlayerPrefs
        playerTank.HP = PlayerPrefs.GetInt("TankHP", playerTank.HP);
        playerTank.damage = PlayerPrefs.GetInt("TankDamage", playerTank.damage);
    }
    private void UpdatePriceForUpgrade(int nextLevel)
    {
        priceForUpgrade.text = playerTank.upgradeLevels[nextLevel].costTank.ToString();
    }

    private void UpdateTotalScienceText()
    {
        totalScienceText.text = GameManager.TotalScience.ToString();
    }

    private void UpdateTankStatsText()
    {
        hpText.text = playerTank.HP.ToString();
        damageText.text = playerTank.damage.ToString();
        levelText.text = currentLevel.ToString();
        damageUpTextTank.text = "+" + playerTank.upgradeLevels[currentLevel].damageUpTextTank.ToString();
        hpUpTextTank.text = "+" + playerTank.upgradeLevels[currentLevel].hpUpTextTank.ToString();
    }

    private void DefineUpgradeLevels()
    {
        playerTank.upgradeLevels = new List<UpgradeTanks>
        {
            new UpgradeTanks { levelTank = 1, hpTank = 400, damageTank = 20, costTank = 100, damageUpTextTank = 20, hpUpTextTank = 480 },
            new UpgradeTanks { levelTank = 2, hpTank = 880, damageTank = 40, costTank = 500, damageUpTextTank = 26, hpUpTextTank = 480 },
            new UpgradeTanks { levelTank = 3, hpTank = 1320, damageTank = 66, costTank = 1000, damageUpTextTank = 26, hpUpTextTank = 480 },
            new UpgradeTanks { levelTank = 4, hpTank = 1520, damageTank = 100, costTank = 1500, damageUpTextTank = 0, hpUpTextTank = 0 }
        };
    }

    private void UpgradePlayer(int levelTank)
    {
        if (levelTank >= 0 && levelTank < playerTank.upgradeLevels.Count)
        {
            playerTank.HP = playerTank.upgradeLevels[levelTank].hpTank;
            playerTank.damage = playerTank.upgradeLevels[levelTank].damageTank;
            playerTank.SavePlayerStats();
        }
    }
}
