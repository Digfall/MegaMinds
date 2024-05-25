using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeRanger : MonoBehaviour
{
    public PlayerRanger playerRanger;
    public TextMeshProUGUI totalScienceText;
    public TextMeshProUGUI priceForUpgrade;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageUpTextran;
    public TextMeshProUGUI hpUpTextran;
    public Slider upgradeRangerSlider;


    private int currentLevel = 0;

    private const string CurrentLevelRngPrefKey = "CurrentLevelRng"; // Ключ для сохранения/загрузки текущего уровня

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs
        currentLevel = PlayerPrefs.GetInt(CurrentLevelRngPrefKey, 0);

        UpdateTotalScienceText();
        UpdateRangerStatsText();
        DefineUpgradeLevels();
        UpgradePlayer(currentLevel);
        UpdatePriceForUpgrade(currentLevel);
    }

    void Update()
    {
        UpdateRangerStatsText();
        UpdatePriceForUpgrade(currentLevel);
    }

    public void UpgradePlayer()
    {
        if (currentLevel < playerRanger.upgradeLevels.Count - 1)
        {
            int upgradeCost = playerRanger.upgradeLevels[currentLevel].costran;

            if (GameManager.TotalScience >= upgradeCost)
            {
                GameManager.TotalScience -= upgradeCost;
                currentLevel++;
                UpgradePlayer(currentLevel);
                UpdateTotalScienceText();
                UpdateRangerStatsText();
                UpdatePriceForUpgrade(currentLevel);
                upgradeRangerSlider.value = (float)currentLevel / (float)(playerRanger.upgradeLevels.Count - 1);

                // Сохраняем текущий уровень в PlayerPrefs
                PlayerPrefs.SetInt(CurrentLevelRngPrefKey, currentLevel);
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
        playerRanger.HP = PlayerPrefs.GetInt("RangerHP", playerRanger.HP);
        playerRanger.damage = PlayerPrefs.GetInt("RangerDamage", playerRanger.damage);
    }
    private void UpdatePriceForUpgrade(int nextLevel)
    {
        priceForUpgrade.text = playerRanger.upgradeLevels[nextLevel].costran.ToString();
    }

    private void UpdateTotalScienceText()
    {
        totalScienceText.text = GameManager.TotalScience.ToString();
    }

    private void UpdateRangerStatsText()
    {
        hpText.text = playerRanger.HP.ToString();
        damageText.text = playerRanger.damage.ToString();
        levelText.text = currentLevel.ToString();
        damageUpTextran.text = "+" + playerRanger.upgradeLevels[currentLevel].damageUpTextran.ToString();
        hpUpTextran.text = "+" + playerRanger.upgradeLevels[currentLevel].hpUpTextran.ToString();
    }

    private void DefineUpgradeLevels()
    {
        playerRanger.upgradeLevels = new List<UpgradeRangers>
        {
            new UpgradeRangers { levelran = 1, hpran = 60, damageran = 20, costran = 100, damageUpTextran = 20, hpUpTextran = 72 },
            new UpgradeRangers { levelran = 2, hpran = 132, damageran = 40, costran = 500, damageUpTextran = 20, hpUpTextran = 68 },
            new UpgradeRangers { levelran = 3, hpran = 198, damageran = 60, costran = 1000, damageUpTextran = 20, hpUpTextran = 105 },
            new UpgradeRangers { levelran = 4, hpran = 250, damageran = 80, costran = 1500, damageUpTextran = 0, hpUpTextran = 0 }
        };
    }

    private void UpgradePlayer(int levelran)
    {
        if (levelran >= 0 && levelran < playerRanger.upgradeLevels.Count)
        {
            playerRanger.HP = playerRanger.upgradeLevels[levelran].hpran;
            playerRanger.damage = playerRanger.upgradeLevels[levelran].damageran;
            playerRanger.SavePlayerStats();
        }
    }
}
