using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeRogue : MonoBehaviour
{
    [SerializeField] private PlayerRogue playerRogue;
    [SerializeField] private TextMeshProUGUI totalScienceText;
    [SerializeField] private TextMeshProUGUI priceForUpgrade;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI damageUpTextrog;
    [SerializeField] private TextMeshProUGUI hpUpTextrog;
    [SerializeField] private Slider upgradeRogueSlider;
    [SerializeField] private Image levelImage;
    [SerializeField] private Image levelImageButton;
    [SerializeField] private List<Sprite> levelSprites;
    [SerializeField] private List<Sprite> levelSpritesButton;

    private int currentLevelRogue = 1;

    private const string CurrentLevelRogPrefKey = "CurrentLevelRog"; // Ключ для сохранения/загрузки текущего уровня
    private const string UpgradeSliderRogueValuePrefKey = "UpgradeSliderRogueValue"; // Ключ для сохранения значения слайдера

    private void Start()
    {
        currentLevelRogue = PlayerPrefs.GetInt(CurrentLevelRogPrefKey, 1);

        upgradeRogueSlider.value = PlayerPrefs.GetFloat(UpgradeSliderRogueValuePrefKey, 0f);

        DefineUpgradeLevels();
        FindObjectOfType<OtherScene>().UpdateTotalScienceText();
        UpdateRogueStatsText();
        UpgradePlayer(currentLevelRogue);
        UpdatePriceForUpgrade(currentLevelRogue);
    }

    void Update()
    {
        UpdateRogueStatsText();
        UpdatePriceForUpgrade(currentLevelRogue);
    }

    public void UpgradePlayer()
    {
        if (currentLevelRogue < playerRogue.upgradeLevels.Count)
        {
            int upgradeCost = playerRogue.upgradeLevels[currentLevelRogue].costrog;

            if (GameManager.TotalScience >= upgradeCost)
            {
                GameManager.TotalScience -= upgradeCost;
                currentLevelRogue++;
                UpgradePlayer(currentLevelRogue);
                FindObjectOfType<OtherScene>().UpdateTotalScienceText();
                UpdateRogueStatsText();
                UpdatePriceForUpgrade(currentLevelRogue);
                upgradeRogueSlider.value = (float)(currentLevelRogue - 1) / (float)(playerRogue.upgradeLevels.Count - 1);

                if (currentLevelRogue - 1 < levelSprites.Count)
                {
                    levelImage.sprite = levelSprites[currentLevelRogue - 1];
                    levelImageButton.sprite = levelSpritesButton[currentLevelRogue - 1];
                }

                PlayerPrefs.SetFloat(UpgradeSliderRogueValuePrefKey, upgradeRogueSlider.value);
                PlayerPrefs.SetInt(CurrentLevelRogPrefKey, currentLevelRogue);
            }

        }

    }

    public void LoadPlayerStats()
    {
        playerRogue.HP = PlayerPrefs.GetInt("RogueHP", playerRogue.HP);
        playerRogue.damage = PlayerPrefs.GetInt("RogueDamage", playerRogue.damage);
    }

    private void UpdatePriceForUpgrade(int currentLevelRogue)
    {
        if (currentLevelRogue < playerRogue.upgradeLevels.Count)
        {
            priceForUpgrade.text = playerRogue.upgradeLevels[currentLevelRogue].costrog.ToString();
        }
        else
        {
            priceForUpgrade.text = "Max";
        }
    }

    private void UpdateRogueStatsText()
    {
        if (currentLevelRogue <= playerRogue.upgradeLevels.Count)
        {
            hpText.text = playerRogue.HP.ToString();
            damageText.text = playerRogue.damage.ToString();
            levelText.text = currentLevelRogue.ToString();
            if (currentLevelRogue < 3)
            {
                damageUpTextrog.text = "+" + playerRogue.upgradeLevels[currentLevelRogue - 1].damageUpTextrog.ToString();
                hpUpTextrog.text = "+" + playerRogue.upgradeLevels[currentLevelRogue - 1].hpUpTextrog.ToString();
            }
            else
            {
                damageUpTextrog.text = "";
                hpUpTextrog.text = "";
            }
        }
    }

    private void DefineUpgradeLevels()
    {
        playerRogue.upgradeLevels = new List<UpgradeRogues>
        {
            new UpgradeLevel { levelwar = 1, hpwar = 45, damagewar = 8, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 2, hpwar = 50, damagewar = 8, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 3, hpwar = 55, damagewar = 9, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 4, hpwar = 62, damagewar = 10, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 5, hpwar = 70, damagewar = 12, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 6, hpwar = 78, damagewar = 13, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 7, hpwar = 86, damagewar = 14, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 8, hpwar = 94, damagewar = 16, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 9, hpwar = 102, damagewar = 17, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 },
            new UpgradeLevel { levelwar = 10, hpwar = 110, damagewar = 18, costwar = 0, damageUpTextwar = 000, hpUpTextwar = 000 }
        };
    }

    private void UpgradePlayer(int levelrog)
    {
        if (levelrog > 1 && levelrog <= playerRogue.upgradeLevels.Count)
        {
            playerRogue.HP = playerRogue.upgradeLevels[levelrog - 1].hprog;
            playerRogue.damage = playerRogue.upgradeLevels[levelrog - 1].damagerog;
            playerRogue.SavePlayerStats();
            if (levelrog - 1 < levelSprites.Count)
            {
                levelImage.sprite = levelSprites[levelrog - 1];
                levelImageButton.sprite = levelSpritesButton[levelrog - 1];
            }
        }
    }
}
