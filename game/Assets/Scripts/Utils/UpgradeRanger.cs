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
    public Image levelImage;
    public Image levelImageButton;
    public List<Sprite> levelSprites;
    public List<Sprite> levelSpritesButton;

    private int currentLevel = 1;

    private const string CurrentLevelRngPrefKey = "CurrentLevelRng"; // Ключ для сохранения/загрузки текущего уровня
    private const string UpgradeSliderRangerValuePrefKey = "UpgradeSliderRangerValue"; // Ключ для сохранения значения слайдера

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs
        currentLevel = PlayerPrefs.GetInt(CurrentLevelRngPrefKey, 1);

        // Загружаем значение слайдера из PlayerPrefs
        upgradeRangerSlider.value = PlayerPrefs.GetFloat(UpgradeSliderRangerValuePrefKey, 0.367f);

        DefineUpgradeLevels(); // Определяем уровни до их использования
        UpdateTotalScienceText();
        UpdateRangerStatsText();
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
        if (currentLevel < playerRanger.upgradeLevels.Count)
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
                upgradeRangerSlider.value = (float)(currentLevel - 1) / (float)(playerRanger.upgradeLevels.Count - 1);

                if (currentLevel - 1 < levelSprites.Count)
                {
                    levelImage.sprite = levelSprites[currentLevel - 1];
                    levelImageButton.sprite = levelSpritesButton[currentLevel - 1];
                }

                // Сохраняем текущее значение слайдера в PlayerPrefs
                PlayerPrefs.SetFloat(UpgradeSliderRangerValuePrefKey, upgradeRangerSlider.value);

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

    private void UpdatePriceForUpgrade(int currentLevel)
    {
        if (currentLevel < playerRanger.upgradeLevels.Count)
        {
            priceForUpgrade.text = playerRanger.upgradeLevels[currentLevel].costran.ToString();
        }
        else
        {
            priceForUpgrade.text = "Max";
        }
    }

    private void UpdateTotalScienceText()
    {
        totalScienceText.text = GameManager.TotalScience.ToString();
    }

    private void UpdateRangerStatsText()
    {
        if (currentLevel <= playerRanger.upgradeLevels.Count)
        {
            hpText.text = playerRanger.HP.ToString();
            damageText.text = playerRanger.damage.ToString();
            levelText.text = currentLevel.ToString();
            if (currentLevel < 3)
            {
                damageUpTextran.text = "+" + playerRanger.upgradeLevels[currentLevel - 1].damageUpTextran.ToString();
                hpUpTextran.text = "+" + playerRanger.upgradeLevels[currentLevel - 1].hpUpTextran.ToString();
            }
            else
            {
                damageUpTextran.text = "";
                hpUpTextran.text = "";
            }
        }
    }

    private void DefineUpgradeLevels()
    {
        playerRanger.upgradeLevels = new List<UpgradeRangers>
        {
            new UpgradeRangers { levelran = 1, hpran = 150, damageran = 35, costran = 0, damageUpTextran = 35, hpUpTextran = 150 },
            new UpgradeRangers { levelran = 2, hpran = 300, damageran = 70, costran = 100, damageUpTextran = 70, hpUpTextran = 150 },
            new UpgradeRangers { levelran = 3, hpran = 450, damageran = 140, costran = 300, damageUpTextran = 0, hpUpTextran = 0 }
        };
    }

    private void UpgradePlayer(int levelran)
    {
        if (levelran >= 0 && levelran < playerRanger.upgradeLevels.Count)
        {
            playerRanger.HP = playerRanger.upgradeLevels[levelran - 1].hpran;
            playerRanger.damage = playerRanger.upgradeLevels[levelran - 1].damageran;
            playerRanger.SavePlayerStats();
            if (levelran - 1 < levelSprites.Count)
            {
                levelImage.sprite = levelSprites[levelran - 1];
                levelImageButton.sprite = levelSpritesButton[levelran - 1];
            }
        }
    }
}
