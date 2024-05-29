using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWarrior : MonoBehaviour
{
    public PlayerWarrior playerWarrior;
    public TextMeshProUGUI totalScienceText;
    public TextMeshProUGUI priceForUpgrade;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageUpTextwar;
    public TextMeshProUGUI hpUpTextwar;
    public Slider upgradeSlider;
    public Image levelImage;
    public Image levelImageButton;
    public List<Sprite> levelSprites;
    public List<Sprite> levelSpritesButton;

    [SerializeField] private int currentLevel = 1;

    private const string CurrentLevelWarPrefKey = "CurrentLevelWar"; // Ключ для сохранения/загрузки текущего уровня
    private const string UpgradeSliderWarValuePrefKey = "UpgradeSliderWarValue"; // Ключ для сохранения значения слайдера

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs, если значение 0, устанавливаем на 1
        currentLevel = PlayerPrefs.GetInt(CurrentLevelWarPrefKey, 1);

        // Загружаем значение слайдера из PlayerPrefs
        upgradeSlider.value = PlayerPrefs.GetFloat(UpgradeSliderWarValuePrefKey, 0f);

        DefineUpgradeLevels(); // Определяем уровни до их использования
        FindObjectOfType<OtherScene>().UpdateTotalScienceText();
        UpdateWarriorStatsText();
        UpgradePlayer(currentLevel);
        UpdatePriceForUpgrade(currentLevel);
    }

    void Update()
    {
        UpdateWarriorStatsText();
        UpdatePriceForUpgrade(currentLevel);
    }

    public void UpgradePlayer()
    {
        if (currentLevel < playerWarrior.upgradeLevels.Count)
        {
            if (currentLevel < 3)
            {
                int upgradeCost = playerWarrior.upgradeLevels[currentLevel].costwar;

                if (GameManager.TotalScience >= upgradeCost)
                {
                    GameManager.TotalScience -= upgradeCost;
                    currentLevel++;
                    UpgradePlayer(currentLevel);
                    FindObjectOfType<OtherScene>().UpdateTotalScienceText();
                    UpdateWarriorStatsText();
                    UpdatePriceForUpgrade(currentLevel);
                    upgradeSlider.value = (float)(currentLevel - 1) / (float)(playerWarrior.upgradeLevels.Count - 1);

                    if (currentLevel - 1 < levelSprites.Count)
                    {
                        levelImage.sprite = levelSprites[currentLevel - 1];
                        levelImageButton.sprite = levelSpritesButton[currentLevel - 1];
                    }

                    // Сохраняем текущее значение слайдера в PlayerPrefs
                    PlayerPrefs.SetFloat(UpgradeSliderWarValuePrefKey, upgradeSlider.value);
                    // Сохраняем текущий уровень в PlayerPrefs
                    PlayerPrefs.SetInt(CurrentLevelWarPrefKey, currentLevel);
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
    }

    public void LoadPlayerStats()
    {
        // Загружаем сохраненные значения характеристик из PlayerPrefs
        playerWarrior.HP = PlayerPrefs.GetInt("WarriorHP", playerWarrior.HP);
        playerWarrior.damage = PlayerPrefs.GetInt("WarriorDamage", playerWarrior.damage);
    }

    private void UpdatePriceForUpgrade(int currentLevel)
    {
        if (currentLevel < playerWarrior.upgradeLevels.Count)
        {
            priceForUpgrade.text = playerWarrior.upgradeLevels[currentLevel].costwar.ToString();
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

    private void UpdateWarriorStatsText()
    {
        if (currentLevel <= playerWarrior.upgradeLevels.Count)
        {
            hpText.text = playerWarrior.HP.ToString();
            damageText.text = playerWarrior.damage.ToString();
            levelText.text = currentLevel.ToString();
            if (currentLevel < 3)
            {
                damageUpTextwar.text = "+" + playerWarrior.upgradeLevels[currentLevel - 1].damageUpTextwar.ToString();
                hpUpTextwar.text = "+" + playerWarrior.upgradeLevels[currentLevel - 1].hpUpTextwar.ToString();
            }
            else
            {
                damageUpTextwar.text = "";
                hpUpTextwar.text = "";
            }
        }
    }

    private void DefineUpgradeLevels()
    {
        playerWarrior.upgradeLevels = new List<UpgradeLevel>
        {
            new UpgradeLevel { levelwar = 1, hpwar = 200, damagewar = 50, costwar = 0, damageUpTextwar = 50, hpUpTextwar = 200 },
            new UpgradeLevel { levelwar = 2, hpwar = 400, damagewar = 100, costwar = 100, damageUpTextwar = 65, hpUpTextwar = 200 },
            new UpgradeLevel { levelwar = 3, hpwar = 600, damagewar = 165, costwar = 300, damageUpTextwar = 0, hpUpTextwar = 0 }
        };
    }

    private void UpgradePlayer(int levelwar)
    {
        if (levelwar > 0 && levelwar <= playerWarrior.upgradeLevels.Count)
        {
            playerWarrior.HP = playerWarrior.upgradeLevels[levelwar - 1].hpwar;
            playerWarrior.damage = playerWarrior.upgradeLevels[levelwar - 1].damagewar;
            playerWarrior.SavePlayerStats();
            if (levelwar - 1 < levelSprites.Count)
            {
                levelImage.sprite = levelSprites[levelwar - 1];
                levelImageButton.sprite = levelSpritesButton[levelwar - 1];
            }
        }
    }
}
