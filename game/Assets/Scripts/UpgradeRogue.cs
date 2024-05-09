using System.Collections.Generic;
using TMPro;
using UnityEngine;

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


    [SerializeField] private int currentLevelrogue = 0;

    private const string CurrentLevelPrefKey = "CurrentLevel"; // Ключ для сохранения/загрузки текущего уровня

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs
        currentLevelrogue = PlayerPrefs.GetInt(CurrentLevelPrefKey, 0);

        UpdateTotalScienceText();
        UpdateRogueStatsText();
        DefineUpgradeLevels();
        UpgradePlayer(currentLevelrogue);
        UpdatePriceForUpgrade(currentLevelrogue);
    }

    void Update()
    {
        UpdateRogueStatsText();
        UpdatePriceForUpgrade(currentLevelrogue);
    }

    public void UpgradePlayer()
    {
        if (currentLevelrogue < playerRogue.upgradeLevels.Count - 1)
        {
            int upgradeCost = playerRogue.upgradeLevels[currentLevelrogue].costrog;

            if (GameManager.TotalScience >= upgradeCost)
            {
                GameManager.TotalScience -= upgradeCost;
                currentLevelrogue++;
                UpgradePlayer(currentLevelrogue);
                UpdateTotalScienceText();
                UpdateRogueStatsText();
                UpdatePriceForUpgrade(currentLevelrogue);

                // Сохраняем текущий уровень в PlayerPrefs
                PlayerPrefs.SetInt(CurrentLevelPrefKey, currentLevelrogue);
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
        levelText.text = currentLevelrogue.ToString();
        damageUpTextrog.text = "+" + playerRogue.upgradeLevels[currentLevelrogue].damageUpTextrog.ToString(); // Прирост урона
        hpUpTextrog.text = "+" + playerRogue.upgradeLevels[currentLevelrogue].hpUpTextrog.ToString(); // Прирост здоровья
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
