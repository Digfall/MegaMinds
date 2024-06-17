using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTank : MonoBehaviour
{
    [SerializeField] private PlayerTank playerTank;
    [SerializeField] private TextMeshProUGUI totalScienceText;
    [SerializeField] private TextMeshProUGUI priceForUpgrade;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI damageUpTextTank;
    [SerializeField] private TextMeshProUGUI hpUpTextTank;
    [SerializeField] private Slider upgradeSlidertank;
    [SerializeField] private Image levelImage;
    [SerializeField] private Image levelImageButton;
    [SerializeField] private List<Sprite> levelSprites;
    [SerializeField] private List<Sprite> levelSpritesButton;

    private int currentLevelTank = 1;

    private const string CurrentLevelTankPrefKey = "CurrentLevelTank"; // Ключ для сохранения/загрузки текущего уровня
    private const string UpgradeSliderTankValuePrefKey = "UpgradeSliderTankValue"; // Ключ для сохранения значения слайдера

    private void Start()
    {
        currentLevelTank = PlayerPrefs.GetInt(CurrentLevelTankPrefKey, 1);

        upgradeSlidertank.value = PlayerPrefs.GetFloat(UpgradeSliderTankValuePrefKey, 0f);

        DefineUpgradeLevels();
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

                PlayerPrefs.SetFloat(UpgradeSliderTankValuePrefKey, upgradeSlidertank.value);
                PlayerPrefs.SetInt(CurrentLevelTankPrefKey, currentLevelTank);
            }

        }

    }

    public void LoadPlayerStats()
    {
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
            new UpgradeTanks { levelTank = 2, hpTank = 1300, damageTank = 120, costTank = 450, damageUpTextTank = 50, hpUpTextTank = 700 },
            new UpgradeTanks { levelTank = 3, hpTank = 2000, damageTank = 170, costTank = 700, damageUpTextTank = 0, hpUpTextTank = 0 }
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
