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

    private int currentLevelTank = 1;

    private const string CurrentLevelTankPrefKey = "CurrentLevelTank"; // Ключ для сохранения/загрузки текущего уровня
    private const string UpgradeSliderTankValuePrefKey = "UpgradeSliderTankValue"; // Ключ для сохранения значения слайдера

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs
        currentLevelTank = PlayerPrefs.GetInt(CurrentLevelTankPrefKey, 1);

        // Загружаем значение слайдера из PlayerPrefs
        upgradeSlidertank.value = PlayerPrefs.GetFloat(UpgradeSliderTankValuePrefKey, 0f);

        DefineUpgradeLevels(); // Определяем уровни до их использования
        FindObjectOfType<OtherScene>().UpdateTotalScienceText();
        UpdateTankStatsText();
        UpgradePlayer(currentLevelTank);
        UpdatePriceForUpgrade(currentLevelTank);
    }

    void Update()
    {
        UpdateTankStatsText();
        UpdatePriceForUpgrade(currentLevelTank);
    }

    public void UpgradePlayer()
    {
        if (currentLevelTank < playerTank.upgradeLevels.Count)
        {
            int upgradeCost = playerTank.upgradeLevels[currentLevelTank].costTank;

            if (GameManager.TotalScience >= upgradeCost)
            {
                GameManager.TotalScience -= upgradeCost;
                currentLevelTank++;
                UpgradePlayer(currentLevelTank);
                FindObjectOfType<OtherScene>().UpdateTotalScienceText();
                UpdateTankStatsText();
                UpdatePriceForUpgrade(currentLevelTank);
                upgradeSlidertank.value = (float)(currentLevelTank - 1) / (float)(playerTank.upgradeLevels.Count - 1);

                if (currentLevelTank - 1 < levelSprites.Count)
                {
                    levelImage.sprite = levelSprites[currentLevelTank - 1];
                    levelImageButton.sprite = levelSpritesButton[currentLevelTank - 1];
                }

                // Сохраняем текущее значение слайдера в PlayerPrefs
                PlayerPrefs.SetFloat(UpgradeSliderTankValuePrefKey, upgradeSlidertank.value);

                // Сохраняем текущий уровень в PlayerPrefs
                PlayerPrefs.SetInt(CurrentLevelTankPrefKey, currentLevelTank);
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

    private void UpdatePriceForUpgrade(int currentLevelTank)
    {
        if (currentLevelTank < playerTank.upgradeLevels.Count)
        {
            priceForUpgrade.text = playerTank.upgradeLevels[currentLevelTank].costTank.ToString();
        }
        else
        {
            priceForUpgrade.text = "Max";
        }
    }

    private void UpdateTankStatsText()
    {
        if (currentLevelTank <= playerTank.upgradeLevels.Count)
        {
            hpText.text = playerTank.HP.ToString();
            damageText.text = playerTank.damage.ToString();
            levelText.text = currentLevelTank.ToString();
            if (currentLevelTank < 3)
            {
                damageUpTextTank.text = "+" + playerTank.upgradeLevels[currentLevelTank - 1].damageUpTextTank.ToString();
                hpUpTextTank.text = "+" + playerTank.upgradeLevels[currentLevelTank - 1].hpUpTextTank.ToString();
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
