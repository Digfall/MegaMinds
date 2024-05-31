using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeRanger : MonoBehaviour
{
    public PlayerRanger playerRanger;
    public TextMeshProUGUI totalScienceText;
    public TextMeshProUGUI priceForUpgrade;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageUpTextran;
    public TextMeshProUGUI hpUpTextran;
    public Slider upgradeRangerSlider;
    public Image levelImage;
    public Image levelImageButton;
    public List<Sprite> levelSprites;
    public List<Sprite> levelSpritesButton;

    private int currentLevelRanger = 1;

    private const string CurrentLevelRngPrefKey = "CurrentLevelRng"; // Ключ для сохранения/загрузки текущего уровня
    private const string UpgradeSliderRangerValuePrefKey = "UpgradeSliderRangerValue"; // Ключ для сохранения значения слайдера

    private void Start()
    {
        // Загружаем текущий уровень из PlayerPrefs
        currentLevelRanger = PlayerPrefs.GetInt(CurrentLevelRngPrefKey, 1);

        // Загружаем значение слайдера из PlayerPrefs
        upgradeRangerSlider.value = PlayerPrefs.GetFloat(UpgradeSliderRangerValuePrefKey, 0f);

        DefineUpgradeLevels(); // Определяем уровни до их использования
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

                // Сохраняем текущее значение слайдера в PlayerPrefs
                PlayerPrefs.SetFloat(UpgradeSliderRangerValuePrefKey, upgradeRangerSlider.value);

                // Сохраняем текущий уровень в PlayerPrefs
                PlayerPrefs.SetInt(CurrentLevelRngPrefKey, currentLevelRanger);
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
            new UpgradeRangers { levelran = 1, hpran = 60, damageran = 20, costran = 0, damageUpTextran = 20, hpUpTextran = 72 },
            new UpgradeRangers { levelran = 2, hpran = 132, damageran = 40, costran = 100, damageUpTextran = 20, hpUpTextran = 68 },
            new UpgradeRangers { levelran = 3, hpran = 198, damageran = 60, costran = 300, damageUpTextran = 20, hpUpTextran = 105 }
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
