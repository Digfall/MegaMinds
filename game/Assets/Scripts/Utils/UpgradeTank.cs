using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Slider upgradeSlidertank;
    public Image levelImage;
    public Image levelImageButton;
    public List<Sprite> levelSprites;
    public List<Sprite> levelSpritesButton;

    private int currentLevel = 1;

    private const string CurrentLevelTankPrefKey = "CurrentLevelTank"; // Ключ для сохранения/загрузки текущего уровня
    private const string UpgradeSliderTankValuePrefKey = "UpgradeSliderTankValue"; // Ключ для сохранения значения слайдера

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs
        currentLevel = PlayerPrefs.GetInt(CurrentLevelTankPrefKey, 1);

        // Загружаем значение слайдера из PlayerPrefs
        upgradeSlidertank.value = PlayerPrefs.GetFloat(UpgradeSliderTankValuePrefKey, 0f);

        DefineUpgradeLevels(); // Определяем уровни до их использования
        UpdateTotalScienceText();
        UpdateTankStatsText();
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
        if (currentLevel < playerTank.upgradeLevels.Count)
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
                upgradeSlidertank.value = (float)(currentLevel - 1) / (float)(playerTank.upgradeLevels.Count - 1);

                if (currentLevel - 1 < levelSprites.Count)
                {
                    levelImage.sprite = levelSprites[currentLevel - 1];
                    levelImageButton.sprite = levelSpritesButton[currentLevel - 1];
                }

                // Сохраняем текущее значение слайдера в PlayerPrefs
                PlayerPrefs.SetFloat(UpgradeSliderTankValuePrefKey, upgradeSlidertank.value);

                // Сохраняем текущий уровень в PlayerPrefs
                PlayerPrefs.SetInt(CurrentLevelTankPrefKey, currentLevel);
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

    private void UpdatePriceForUpgrade(int currentLevel)
    {
        if (currentLevel < playerTank.upgradeLevels.Count)
        {
            priceForUpgrade.text = playerTank.upgradeLevels[currentLevel].costTank.ToString();
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

    private void UpdateTankStatsText()
    {
        if (currentLevel <= playerTank.upgradeLevels.Count)
        {
            hpText.text = playerTank.HP.ToString();
            damageText.text = playerTank.damage.ToString();
            levelText.text = currentLevel.ToString();
            if (currentLevel < 3)
            {
                damageUpTextTank.text = "+" + playerTank.upgradeLevels[currentLevel - 1].damageUpTextTank.ToString();
                hpUpTextTank.text = "+" + playerTank.upgradeLevels[currentLevel - 1].hpUpTextTank.ToString();
            }
            else
            {
                damageUpTextTank.text = "";
                hpUpTextTank.text = "";
            }
        }
    }

    private void DefineUpgradeLevels()
    {
        playerTank.upgradeLevels = new List<UpgradeTanks>
        {
            new UpgradeTanks { levelTank = 1, hpTank = 800, damageTank = 75, costTank = 0, damageUpTextTank = 45, hpUpTextTank = 500 },
            new UpgradeTanks { levelTank = 2, hpTank = 1300, damageTank = 120, costTank = 300, damageUpTextTank = 50, hpUpTextTank = 700 },
            new UpgradeTanks { levelTank = 3, hpTank = 2000, damageTank = 170, costTank = 400, damageUpTextTank = 0, hpUpTextTank = 0 }
        };
    }

    private void UpgradePlayer(int levelTank)
    {
        if (levelTank > 0 && levelTank <= playerTank.upgradeLevels.Count)
        {
            playerTank.HP = playerTank.upgradeLevels[levelTank - 1].hpTank;
            playerTank.damage = playerTank.upgradeLevels[levelTank - 1].damageTank;
            playerTank.SavePlayerStats();
            if (levelTank - 1 < levelSprites.Count)
            {
                levelImage.sprite = levelSprites[levelTank - 1];
                levelImageButton.sprite = levelSpritesButton[levelTank - 1];
            }
        }
    }
}
