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
    public Image levelImage;
    public Image levelImageButton;
    public List<Sprite> levelSprites;
    public List<Sprite> levelSpritesButton;

    private int currentLevel = 1;

    private const string CurrentLevelRogPrefKey = "CurrentLevelRog"; // Ключ для сохранения/загрузки текущего уровня
    private const string UpgradeSliderRogueValuePrefKey = "UpgradeSliderRogueValue"; // Ключ для сохранения значения слайдера

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs
        currentLevel = PlayerPrefs.GetInt(CurrentLevelRogPrefKey, 1);

        // Загружаем значение слайдера из PlayerPrefs
        upgradeRogueSlider.value = PlayerPrefs.GetFloat(UpgradeSliderRogueValuePrefKey, 0.367f);

        DefineUpgradeLevels(); // Определяем уровни до их использования
        UpdateTotalScienceText();
        UpdateRogueStatsText();
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
        if (currentLevel < playerRogue.upgradeLevels.Count)
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
                upgradeRogueSlider.value = (float)(currentLevel - 1) / (float)(playerRogue.upgradeLevels.Count - 1);

                if (currentLevel - 1 < levelSprites.Count)
                {
                    levelImage.sprite = levelSprites[currentLevel - 1];
                    levelImageButton.sprite = levelSpritesButton[currentLevel - 1];
                }

                // Сохраняем текущее значение слайдера в PlayerPrefs
                PlayerPrefs.SetFloat(UpgradeSliderRogueValuePrefKey, upgradeRogueSlider.value);

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

    private void UpdatePriceForUpgrade(int currentLevel)
    {
        if (currentLevel < playerRogue.upgradeLevels.Count)
        {
            priceForUpgrade.text = playerRogue.upgradeLevels[currentLevel].costrog.ToString();
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

    private void UpdateRogueStatsText()
    {
        if (currentLevel <= playerRogue.upgradeLevels.Count)
        {
            hpText.text = playerRogue.HP.ToString();
            damageText.text = playerRogue.damage.ToString();
            levelText.text = currentLevel.ToString();
            if (currentLevel < 3)
            {
                damageUpTextrog.text = "+" + playerRogue.upgradeLevels[currentLevel - 1].damageUpTextrog.ToString();
                hpUpTextrog.text = "+" + playerRogue.upgradeLevels[currentLevel - 1].hpUpTextrog.ToString();
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
            new UpgradeRogues { levelrog = 1, hprog = 150, damagerog = 150, costrog = 0, damageUpTextrog = 50, hpUpTextrog = 50 },
            new UpgradeRogues { levelrog = 2, hprog = 200, damagerog = 200, costrog = 300, damageUpTextrog = 100, hpUpTextrog = 100 },
            new UpgradeRogues { levelrog = 3, hprog = 300, damagerog = 500, costrog = 400, damageUpTextrog = 0, hpUpTextrog = 0 }
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
