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

    [SerializeField] private int currentLevelWar = 1;

    private const string CurrentLevelWarPrefKey = "CurrentLevelWar"; // Ключ для сохранения/загрузки текущего уровня
    private const string UpgradeSliderWarValuePrefKey = "UpgradeSliderWarValue"; // Ключ для сохранения значения слайдера

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs, если значение 0, устанавливаем на 1
        currentLevelWar = PlayerPrefs.GetInt(CurrentLevelWarPrefKey, 1);

        // Загружаем значение слайдера из PlayerPrefs
        upgradeSlider.value = PlayerPrefs.GetFloat(UpgradeSliderWarValuePrefKey, 0f);

        DefineUpgradeLevels(); // Определяем уровни до их использования
        FindObjectOfType<OtherScene>().UpdateTotalScienceText();
        UpdateWarriorStatsText();
        UpgradePlayer(currentLevelWar);
        UpdatePriceForUpgrade(currentLevelWar);
    }

    void Update()
    {
        UpdateWarriorStatsText();
        UpdatePriceForUpgrade(currentLevelWar);
    }

    public void UpgradePlayer()
    {
        if (currentLevelWar < playerWarrior.upgradeLevels.Count)
        {
            if (currentLevelWar < 3)
            {
                int upgradeCost = playerWarrior.upgradeLevels[currentLevelWar].costwar;

                if (GameManager.TotalScience >= upgradeCost)
                {
                    GameManager.TotalScience -= upgradeCost;
                    currentLevelWar++;
                    UpgradePlayer(currentLevelWar);
                    FindObjectOfType<OtherScene>().UpdateTotalScienceText();
                    UpdateWarriorStatsText();
                    UpdatePriceForUpgrade(currentLevelWar);
                    upgradeSlider.value = (float)(currentLevelWar - 1) / (float)(playerWarrior.upgradeLevels.Count - 1);

                    if (currentLevelWar - 1 < levelSprites.Count)
                    {
                        levelImage.sprite = levelSprites[currentLevelWar - 1];
                        levelImageButton.sprite = levelSpritesButton[currentLevelWar - 1];
                    }

                    // Сохраняем текущее значение слайдера в PlayerPrefs
                    PlayerPrefs.SetFloat(UpgradeSliderWarValuePrefKey, upgradeSlider.value);
                    // Сохраняем текущий уровень в PlayerPrefs
                    PlayerPrefs.SetInt(CurrentLevelWarPrefKey, currentLevelWar);
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

    private void UpdatePriceForUpgrade(int currentLevelWar)
    {
        if (currentLevelWar < playerWarrior.upgradeLevels.Count)
        {
            priceForUpgrade.text = playerWarrior.upgradeLevels[currentLevelWar].costwar.ToString();
        }
        else
        {
            priceForUpgrade.text = "Max";
        }
    }

    private void UpdateWarriorStatsText()
    {
        if (currentLevelWar <= playerWarrior.upgradeLevels.Count)
        {
            hpText.text = playerWarrior.HP.ToString();
            damageText.text = playerWarrior.damage.ToString();
            levelText.text = currentLevelWar.ToString();
            if (currentLevelWar < 3)
            {
                damageUpTextwar.text = "+" + playerWarrior.upgradeLevels[currentLevelWar - 1].damageUpTextwar.ToString();
                hpUpTextwar.text = "+" + playerWarrior.upgradeLevels[currentLevelWar - 1].hpUpTextwar.ToString();
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
