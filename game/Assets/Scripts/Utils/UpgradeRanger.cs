using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeRanger : MonoBehaviour
{
    [SerializeField] private PlayerRanger playerRanger;
    [SerializeField] private TextMeshProUGUI totalScienceText;
    [SerializeField] private TextMeshProUGUI priceForUpgrade;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI damageUpTextran;
    [SerializeField] private TextMeshProUGUI hpUpTextran;
    [SerializeField] private Slider upgradeRangerSlider;
    [SerializeField] private Image levelImage;
    [SerializeField] private Image levelImageButton;
    [SerializeField] private List<Sprite> levelSprites;
    [SerializeField] private List<Sprite> levelSpritesButton;

    private int currentLevelRanger = 1;

    private const string CurrentLevelRngPrefKey = "CurrentLevelRng"; // Ключ для сохранения/загрузки текущего уровня
    private const string UpgradeSliderRangerValuePrefKey = "UpgradeSliderRangerValue"; // Ключ для сохранения значения слайдера

    private void Start()
    {
        currentLevelRanger = PlayerPrefs.GetInt(CurrentLevelRngPrefKey, 1);

        upgradeRangerSlider.value = PlayerPrefs.GetFloat(UpgradeSliderRangerValuePrefKey, 0f);

        DefineUpgradeLevels();
        FindObjectOfType<OtherScene>().UpdateTotalScienceText();
        UpgradePlayer(currentLevelRanger);
        UpdateRangerStatsText();
        UpdatePriceForUpgrade(currentLevelRanger);
    }

    void Update()
    {
        UpdateRangerStatsText();
        UpdatePriceForUpgrade(currentLevelRanger);
    }

    public void UpgradePlayer()
    {
        if (currentLevelRanger < playerRanger.upgradeLevels.Count)
        {
            int upgradeCost = playerRanger.upgradeLevels[currentLevelRanger].costran;

            if (GameManager.TotalScience >= upgradeCost)
            {
                GameManager.TotalScience -= upgradeCost;
                currentLevelRanger++;
                UpgradePlayer(currentLevelRanger);
                FindObjectOfType<OtherScene>().UpdateTotalScienceText();
                UpdateRangerStatsText();
                UpdatePriceForUpgrade(currentLevelRanger);
                upgradeRangerSlider.value = (float)(currentLevelRanger - 1) / (float)(playerRanger.upgradeLevels.Count - 1);

                if (currentLevelRanger - 1 < levelSprites.Count)
                {
                    levelImage.sprite = levelSprites[currentLevelRanger - 1];
                    levelImageButton.sprite = levelSpritesButton[currentLevelRanger - 1];
                }

                PlayerPrefs.SetFloat(UpgradeSliderRangerValuePrefKey, upgradeRangerSlider.value);
                PlayerPrefs.SetInt(CurrentLevelRngPrefKey, currentLevelRanger);
            }

        }

    }

    public void LoadPlayerStats()
    {
        playerRanger.HP = PlayerPrefs.GetInt("RangerHP", playerRanger.HP);
        playerRanger.damage = PlayerPrefs.GetInt("RangerDamage", playerRanger.damage);
    }

    private void UpdatePriceForUpgrade(int currentLevelRanger)
    {
        if (currentLevelRanger < playerRanger.upgradeLevels.Count)
        {
            priceForUpgrade.text = playerRanger.upgradeLevels[currentLevelRanger].costran.ToString();
        }
        else
        {
            priceForUpgrade.text = "Max";
        }
    }

    private void UpdateRangerStatsText()
    {
        if (currentLevelRanger <= playerRanger.upgradeLevels.Count)
        {
            hpText.text = playerRanger.HP.ToString();
            damageText.text = playerRanger.damage.ToString();
            levelText.text = currentLevelRanger.ToString();
            if (currentLevelRanger < playerRanger.upgradeLevels.Count)
            {
                damageUpTextran.text = "+" + playerRanger.upgradeLevels[currentLevelRanger - 1].damageUpTextran.ToString();
                hpUpTextran.text = "+" + playerRanger.upgradeLevels[currentLevelRanger - 1].hpUpTextran.ToString();
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
            new UpgradeLevel { levelwar = 1, hpwar = 24, damagewar = 12, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 2, hpwar = 27, damagewar = 14, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 3, hpwar = 30, damagewar = 15, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 4, hpwar = 33, damagewar = 17, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 5, hpwar = 36, damagewar = 18, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 6, hpwar = 39, damagewar = 19, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 7, hpwar = 42, damagewar = 20, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 8, hpwar = 45, damagewar = 21, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 9, hpwar = 48, damagewar = 23, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 10, hpwar = 51, damagewar = 24, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 }
        };
    }

    private void UpgradePlayer(int levelran)
    {
        if (levelran > 0 && levelran <= playerRanger.upgradeLevels.Count)
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
