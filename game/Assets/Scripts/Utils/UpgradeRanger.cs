using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeRanger : MonoBehaviour
{
    [SerializeField] private PlayerRanger playerRanger;
    [SerializeField] private UpgradeSoundManager soundManager;
    [SerializeField] private ClassUnlocker unlocker;
    [SerializeField] private TextMeshProUGUI totalScienceText;
    [SerializeField] private TextMeshProUGUI priceForUpgrade;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI damageUpTextran;
    [SerializeField] private TextMeshProUGUI hpUpTextran;
    [SerializeField] private Slider upgradeRangerSlider;

    [SerializeField] private List<Sprite> bodySprites;
    [SerializeField] private List<Sprite> wepSprites;

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
            if (currentLevelRanger < 10)
            {
                int upgradeCost = playerRanger.upgradeLevels[currentLevelRanger].costran;

                if (GameManager.TotalScience >= upgradeCost)
                {
                    GameManager.TotalScience -= upgradeCost;
                    currentLevelRanger++;
                    UpgradePlayer(currentLevelRanger);
                    soundManager.PlayUpgradeSuccessSound();
                    FindObjectOfType<OtherScene>().UpdateTotalScienceText();
                    UpdateRangerStatsText();
                    unlocker.UpgradeImagesRanger(currentLevelRanger);
                    UpdatePriceForUpgrade(currentLevelRanger);
                    upgradeRangerSlider.value = (float)(currentLevelRanger - 1) / (float)(playerRanger.upgradeLevels.Count - 1);

                    PlayerPrefs.SetFloat(UpgradeSliderRangerValuePrefKey, upgradeRangerSlider.value);
                    PlayerPrefs.SetInt(CurrentLevelRngPrefKey, currentLevelRanger);
                }
                else
                {
                    soundManager.PlayUpgradeFailSound();
                }
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
            if (currentLevelRanger < 10)
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
            new UpgradeRangers
            {
                levelran = 1,
                hpran = 24,
                damageran = 12,
                costran = 0,
                damageUpTextran = 2,
                hpUpTextran = 3,
                bodySprite = bodySprites[0],
                wepSprite = wepSprites[0]
                },
            new UpgradeRangers
            {
                levelran = 2,
                hpran = 27,
                damageran = 14,
                costran = 50,
                damageUpTextran = 1,
                hpUpTextran = 3,
                bodySprite = bodySprites[0],
                wepSprite = wepSprites[0]
                },
            new UpgradeRangers
            {
                levelran = 3,
                hpran = 30,
                damageran = 15,
                costran = 55,
                damageUpTextran = 2,
                hpUpTextran = 3,
                bodySprite = bodySprites[0],
                wepSprite = wepSprites[0]
                },
            new UpgradeRangers
            {
                levelran = 4,
                hpran = 33,
                damageran = 17,
                costran = 60,
                damageUpTextran = 1,
                hpUpTextran = 3,
                bodySprite = bodySprites[0],
                wepSprite = wepSprites[0]
                },
            new UpgradeRangers
            {
                levelran = 5,
                hpran = 36,
                damageran = 18,
                costran = 65,
                damageUpTextran = 1,
                hpUpTextran = 3,
                bodySprite = bodySprites[1],
                wepSprite = wepSprites[1]
                },
            new UpgradeRangers
            {
                levelran = 6,
                hpran = 39,
                damageran = 19,
                costran = 70,
                damageUpTextran = 1,
                hpUpTextran = 3,
                bodySprite = bodySprites[1],
                wepSprite = wepSprites[1]
                },
            new UpgradeRangers
            {
                levelran = 7,
                hpran = 42,
                damageran = 20,
                costran = 75,
                damageUpTextran = 1,
                hpUpTextran = 3,
                bodySprite = bodySprites[1],
                wepSprite = wepSprites[1]
                },
            new UpgradeRangers
            {
                levelran = 8,
                hpran = 45,
                damageran = 21,
                costran = 80,
                damageUpTextran = 2,
                hpUpTextran = 3,
                bodySprite = bodySprites[2],
                wepSprite = wepSprites[2]
                },
            new UpgradeRangers
            {
                levelran = 9,
                hpran = 48,
                damageran = 23,
                costran = 85,
                damageUpTextran = 1,
                hpUpTextran = 3,
                bodySprite = bodySprites[2],
                wepSprite = wepSprites[2]
                },
            new UpgradeRangers
            {
                levelran = 10,
                hpran = 51,
                damageran = 24,
                costran = 90,
                damageUpTextran = 0,
                hpUpTextran = 0,
                bodySprite = bodySprites[2],
                wepSprite = wepSprites[2]
            }
        };
    }

    private void UpgradePlayer(int levelran)
    {
        if (levelran > 0 && levelran <= playerRanger.upgradeLevels.Count)
        {
            playerRanger.HP = playerRanger.upgradeLevels[levelran - 1].hpran;
            playerRanger.damage = playerRanger.upgradeLevels[levelran - 1].damageran;
            playerRanger.SavePlayerStats();
        }
    }
}
