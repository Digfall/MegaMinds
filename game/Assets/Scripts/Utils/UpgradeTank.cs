using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTank : MonoBehaviour
{
    [SerializeField] private PlayerTank playerTank;
    [SerializeField] private UpgradeSoundManager soundManager;
    [SerializeField] private ClassUnlocker unlocker;
    [SerializeField] private TextMeshProUGUI totalScienceText;
    [SerializeField] private TextMeshProUGUI priceForUpgrade;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI damageUpTextTank;
    [SerializeField] private TextMeshProUGUI hpUpTextTank;
    [SerializeField] private Slider upgradeSlidertank;

    [SerializeField] private List<Sprite> bodySprites;
    [SerializeField] private List<Sprite> wepSprites;

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
            if (currentLevelTank < 10)
            {
                int upgradeCost = playerTank.upgradeLevels[currentLevelTank].costTank;

                if (GameManager.TotalScience >= upgradeCost)
                {
                    GameManager.TotalScience -= upgradeCost;
                    currentLevelTank++;
                    UpgradePlayer(currentLevelTank);
                    soundManager.PlayUpgradeSuccessSound();
                    FindObjectOfType<OtherScene>().UpdateTotalScienceText();
                    UpdateTankStatsText();
                    unlocker.UpgradeImagesTank(currentLevelTank);
                    UpdatePriceForUpgrade(currentLevelTank);
                    upgradeSlidertank.value = (float)(currentLevelTank - 1) / (float)(playerTank.upgradeLevels.Count - 1);



                    PlayerPrefs.SetFloat(UpgradeSliderTankValuePrefKey, upgradeSlidertank.value);
                    PlayerPrefs.SetInt(CurrentLevelTankPrefKey, currentLevelTank);
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
            if (currentLevelTank < 10)
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
            new UpgradeTanks
            {
                levelTank = 1,
                hpTank = 84,
                damageTank = 8,
                costTank = 0,
                damageUpTextTank = 1,
                hpUpTextTank = 6,
                bodySprite = bodySprites[0],
                wepSprite = wepSprites[0]
            },
            new UpgradeTanks
            {
                levelTank = 2,
                hpTank = 90,
                damageTank = 9,
                costTank = 56,
                damageUpTextTank = 1,
                hpUpTextTank = 6,
                bodySprite = bodySprites[0],
                wepSprite = wepSprites[0]
            },
            new UpgradeTanks
            {
                levelTank = 3,
                hpTank = 96,
                damageTank = 10,
                costTank = 62,
                damageUpTextTank = 0,
                hpUpTextTank = 8,
                bodySprite = bodySprites[0],
                wepSprite = wepSprites[0]
            },
            new UpgradeTanks
            {
                levelTank = 4,
                hpTank = 104,
                damageTank = 10,
                costTank = 68,
                damageUpTextTank = 1,
                hpUpTextTank = 8,
                bodySprite = bodySprites[0],
                wepSprite = wepSprites[0]
            },
            new UpgradeTanks
            {
                levelTank = 5,
                hpTank = 112,
                damageTank = 11,
                costTank = 74,
                damageUpTextTank = 1,
                hpUpTextTank = 10,
                bodySprite = bodySprites[1],
                wepSprite = wepSprites[1]
            },
            new UpgradeTanks
            {
                levelTank = 6,
                hpTank = 122,
                damageTank = 12,
                costTank = 80,
                damageUpTextTank = 1,
                hpUpTextTank = 10,
                bodySprite = bodySprites[1],
                wepSprite = wepSprites[1]
            },
            new UpgradeTanks
            {
                levelTank = 7,
                hpTank = 132,
                damageTank = 13,
                costTank = 86,
                damageUpTextTank = 1,
                hpUpTextTank = 10,
                bodySprite = bodySprites[1],
                wepSprite = wepSprites[1]
            },
            new UpgradeTanks
            {
                levelTank = 8,
                hpTank = 142,
                damageTank = 14,
                costTank = 92,
                damageUpTextTank = 1,
                hpUpTextTank = 10,
                bodySprite = bodySprites[2],
                wepSprite = wepSprites[2]
            },
            new UpgradeTanks
            {
                levelTank = 9,
                hpTank = 152,
                damageTank = 15,
                costTank = 98,
                damageUpTextTank = 1,
                hpUpTextTank = 10,
                bodySprite = bodySprites[2],
                wepSprite = wepSprites[2]
            },
            new UpgradeTanks
            {
                levelTank = 10,
                hpTank = 162,
                damageTank = 16,
                costTank = 0,
                damageUpTextTank = 0,
                hpUpTextTank = 0,
                bodySprite = bodySprites[2],
                wepSprite = wepSprites[2]
            }

        };
    }

    private void UpgradePlayer(int levelTank)
    {
        if (levelTank > 0 && levelTank <= playerTank.upgradeLevels.Count)
        {
            playerTank.HP = playerTank.upgradeLevels[levelTank - 1].hpTank;
            playerTank.damage = playerTank.upgradeLevels[levelTank - 1].damageTank;
            playerTank.SavePlayerStats();

        }
    }
}
