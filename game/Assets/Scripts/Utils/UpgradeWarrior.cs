using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWarrior : MonoBehaviour
{
    [SerializeField] private PlayerWarrior playerWarrior;
    [SerializeField] private UpgradeSoundManager soundManager;
    [SerializeField] private ClassUnlocker unlocker;
    [SerializeField] private TextMeshProUGUI totalScienceText;
    [SerializeField] private TextMeshProUGUI priceForUpgrade;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI damageUpTextwar;
    [SerializeField] private TextMeshProUGUI hpUpTextwar;
    [SerializeField] private Slider upgradeSlider;
    [SerializeField] private Image levelImage;
    [SerializeField] private Image levelImageButton;

    [SerializeField] private List<Sprite> bodySprites; // Добавляем поле для спрайтов тела
    [SerializeField] private List<Sprite> batSprites;  // Добавляем поле для спрайтов дубины

    [SerializeField] private int currentLevelWar = 1;


    private const string CurrentLevelWarPrefKey = "CurrentLevelWar"; // Ключ для сохранения/загрузки текущего уровня
    private const string UpgradeSliderWarValuePrefKey = "UpgradeSliderWarValue"; // Ключ для сохранения значения слайдера

    private void Start()
    {
        currentLevelWar = PlayerPrefs.GetInt(CurrentLevelWarPrefKey, 1);

        upgradeSlider.value = PlayerPrefs.GetFloat(UpgradeSliderWarValuePrefKey, 0f);

        DefineUpgradeLevels();
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
                    soundManager.PlayUpgradeSuccessSound();
                    FindObjectOfType<OtherScene>().UpdateTotalScienceText();
                    UpdateWarriorStatsText();
                    unlocker.UpgradeImagesWar(currentLevelWar);
                    UpdatePriceForUpgrade(currentLevelWar);
                    upgradeSlider.value = (float)(currentLevelWar - 1) / (float)(playerWarrior.upgradeLevels.Count - 1);

                    // if (currentLevelWar - 1 < levelSprites.Count)
                    // {
                    //     levelImage.sprite = levelSprites[currentLevelWar - 1];
                    //     levelImageButton.sprite = levelSpritesButton[currentLevelWar - 1];
                    // }

                    PlayerPrefs.SetFloat(UpgradeSliderWarValuePrefKey, upgradeSlider.value);
                    PlayerPrefs.SetInt(CurrentLevelWarPrefKey, currentLevelWar);
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
        new UpgradeLevel
        {
            levelwar = 1,
            hpwar = 200,
            damagewar = 50,
            costwar = 0,
            damageUpTextwar = 50,
            hpUpTextwar = 200,
            bodySprite = bodySprites[0], // Укажите спрайт тела для уровня 1
            batSprite = batSprites[0]    // Укажите спрайт дубины для уровня 1
        },
        new UpgradeLevel
        {
            levelwar = 2,
            hpwar = 400,
            damagewar = 100,
            costwar = 100,
            damageUpTextwar = 65,
            hpUpTextwar = 200,
            bodySprite = bodySprites[1], // Укажите спрайт тела для уровня 2
            batSprite = batSprites[1]    // Укажите спрайт дубины для уровня 2
        },
        new UpgradeLevel
        {
            levelwar = 3,
            hpwar = 600,
            damagewar = 165,
            costwar = 500,
            damageUpTextwar = 0,
            hpUpTextwar = 0,
            bodySprite = bodySprites[2], // Укажите спрайт тела для уровня 3
            batSprite = batSprites[2]    // Укажите спрайт дубины для уровня 3
        }
    };
    }


    private void UpgradePlayer(int levelwar)
    {
        if (levelwar > 0 && levelwar <= playerWarrior.upgradeLevels.Count)
        {
            playerWarrior.HP = playerWarrior.upgradeLevels[levelwar - 1].hpwar;
            playerWarrior.damage = playerWarrior.upgradeLevels[levelwar - 1].damagewar;
            playerWarrior.SavePlayerStats();

        }
    }
}
