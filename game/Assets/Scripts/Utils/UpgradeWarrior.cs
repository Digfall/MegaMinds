using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public PlayerWarrior playerWarrior;
    public TextMeshProUGUI totalScienceText;
    public TextMeshProUGUI priceForUpgrade;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageUpTextwar;
    public TextMeshProUGUI hpUpTextwar;
    public Slider upgradeSlider;


    private int currentLevel = 0;

    private const string CurrentLevelWarPrefKey = "CurrentLevelWar"; // Ключ для сохранения/загрузки текущего уровня

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs
        currentLevel = PlayerPrefs.GetInt(CurrentLevelWarPrefKey, 0);

        UpdateTotalScienceText();
        UpdateWarriorStatsText();
        DefineUpgradeLevels();
        UpgradePlayer(currentLevel);
        UpdatePriceForUpgrade(currentLevel);
    }

    void Update()
    {
        UpdateWarriorStatsText();
        UpdatePriceForUpgrade(currentLevel);
    }

    public void UpgradePlayer()
    {
        if (currentLevel < playerWarrior.upgradeLevels.Count - 1)
        {
            int upgradeCost = playerWarrior.upgradeLevels[currentLevel].costwar;

            if (GameManager.TotalScience >= upgradeCost)
            {
                GameManager.TotalScience -= upgradeCost;
                currentLevel++;
                UpgradePlayer(currentLevel);
                UpdateTotalScienceText();
                UpdateWarriorStatsText();
                UpdatePriceForUpgrade(currentLevel);
                upgradeSlider.value = (float)currentLevel / (float)(playerWarrior.upgradeLevels.Count - 1);


                // Сохраняем текущий уровень в PlayerPrefs
                PlayerPrefs.SetInt(CurrentLevelWarPrefKey, currentLevel);
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
        playerWarrior.HP = PlayerPrefs.GetInt("WarriorHP", playerWarrior.HP);
        playerWarrior.damage = PlayerPrefs.GetInt("WarriorDamage", playerWarrior.damage);
    }
    private void UpdatePriceForUpgrade(int nextLevel)
    {
        priceForUpgrade.text = playerWarrior.upgradeLevels[nextLevel].costwar.ToString();
    }

    private void UpdateTotalScienceText()
    {
        totalScienceText.text = GameManager.TotalScience.ToString();
    }

    private void UpdateWarriorStatsText()
    {
        hpText.text = playerWarrior.HP.ToString();
        damageText.text = playerWarrior.damage.ToString();
        levelText.text = currentLevel.ToString();
        damageUpTextwar.text = "+" + playerWarrior.upgradeLevels[currentLevel].damageUpTextwar.ToString();
        hpUpTextwar.text = "+" + playerWarrior.upgradeLevels[currentLevel].hpUpTextwar.ToString();
    }

    private void DefineUpgradeLevels()
    {
        playerWarrior.upgradeLevels = new List<UpgradeLevel>
        {
            new UpgradeLevel { levelwar = 1, hpwar = 200, damagewar = 50, costwar = 100, damageUpTextwar = 50, hpUpTextwar = 300 },
            new UpgradeLevel { levelwar = 2, hpwar = 500, damagewar = 100, costwar = 500, damageUpTextwar = 65, hpUpTextwar = 250 },
            new UpgradeLevel { levelwar = 3, hpwar = 750, damagewar = 165, costwar = 750, damageUpTextwar = 105, hpUpTextwar = 105 },
            new UpgradeLevel { levelwar = 4, hpwar = 1000, damagewar = 165, costwar = 1000, damageUpTextwar = 0, hpUpTextwar = 0 }
        };
    }

    private void UpgradePlayer(int levelwar)
    {
        if (levelwar >= 0 && levelwar < playerWarrior.upgradeLevels.Count)
        {
            playerWarrior.HP = playerWarrior.upgradeLevels[levelwar].hpwar;
            playerWarrior.damage = playerWarrior.upgradeLevels[levelwar].damagewar;
            playerWarrior.SavePlayerStats();
        }
    }
}
