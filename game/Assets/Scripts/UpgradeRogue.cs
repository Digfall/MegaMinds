using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeRogue : MonoBehaviour
{
    public PlayerRogue playerRogue;
    public TextMeshProUGUI totalScienceText;
    public TextMeshProUGUI priceForUpgrade;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageUpTextrog;
    public TextMeshProUGUI hpUpTextrog;
    public Slider upgradeRogueSlider;


    private int currentLevel = 0;

    private const string CurrentLevelRogPrefKey = "CurrentLevelRog"; // Ключ для сохранения/загрузки текущего уровня

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs
        currentLevel = PlayerPrefs.GetInt(CurrentLevelRogPrefKey, 0);

        UpdateTotalScienceText();
        UpdateRogueStatsText();
        DefineUpgradeLevels();
        UpgradePlayer(currentLevel);
        UpdatePriceForUpgrade(currentLevel);
    }

    void Update()
    {
        UpdateRogueStatsText();
        UpdatePriceForUpgrade(currentLevel);
    }

    public void UpgradePlayer()
    {
        if (currentLevel < playerRogue.upgradeLevels.Count - 1)
        {
            int upgradeCost = playerRogue.upgradeLevels[currentLevel].costrog;

            if (GameManager.TotalScience >= upgradeCost)
            {
                GameManager.TotalScience -= upgradeCost;
                currentLevel++;
                UpgradePlayer(currentLevel);
                UpdateTotalScienceText();
                UpdateRogueStatsText();
                UpdatePriceForUpgrade(currentLevel);
                upgradeRogueSlider.value = (float)currentLevel / (float)(playerRogue.upgradeLevels.Count - 1);

                // Сохраняем текущий уровень в PlayerPrefs
                PlayerPrefs.SetInt(CurrentLevelRogPrefKey, currentLevel);
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
        playerRogue.HP = PlayerPrefs.GetInt("RogueHP", playerRogue.HP);
        playerRogue.damage = PlayerPrefs.GetInt("RogueDamage", playerRogue.damage);
    }
    private void UpdatePriceForUpgrade(int nextLevel)
    {
        priceForUpgrade.text = playerRogue.upgradeLevels[nextLevel].costrog.ToString();
    }

    private void UpdateTotalScienceText()
    {
        totalScienceText.text = GameManager.TotalScience.ToString();
    }

    private void UpdateRogueStatsText()
    {
        hpText.text = playerRogue.HP.ToString();
        damageText.text = playerRogue.damage.ToString();
        levelText.text = currentLevel.ToString();
        damageUpTextrog.text = "+" + playerRogue.upgradeLevels[currentLevel].damageUpTextrog.ToString(); // Прирост урона
        hpUpTextrog.text = "+" + playerRogue.upgradeLevels[currentLevel].hpUpTextrog.ToString(); // Прирост здоровья
    }

    private void DefineUpgradeLevels()
    {
        playerRogue.upgradeLevels = new List<UpgradeRogues>
        {
            new UpgradeRogues { levelrog = 1, hprog = 50, damagerog = 50, costrog = 100, damageUpTextrog = 50, hpUpTextrog = 70 },
            new UpgradeRogues { levelrog = 2, hprog = 120, damagerog = 100, costrog = 500, damageUpTextrog = 80, hpUpTextrog = 60 },
            new UpgradeRogues { levelrog = 3, hprog = 180, damagerog = 180, costrog = 1000, damageUpTextrog = 25, hpUpTextrog = 50 },
            new UpgradeRogues { levelrog = 4, hprog = 280, damagerog = 200, costrog = 1500, damageUpTextrog = 0, hpUpTextrog = 0 }
        };
    }

    private void UpgradePlayer(int levelrog)
    {
        if (levelrog >= 0 && levelrog < playerRogue.upgradeLevels.Count)
        {
            playerRogue.HP = playerRogue.upgradeLevels[levelrog].hprog;
            playerRogue.damage = playerRogue.upgradeLevels[levelrog].damagerog;
            playerRogue.SavePlayerStats();
        }
    }
}
