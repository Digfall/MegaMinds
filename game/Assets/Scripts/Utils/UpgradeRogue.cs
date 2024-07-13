using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeRogue : MonoBehaviour
{
    [SerializeField] private PlayerRogue playerRogue;
    [SerializeField] private UpgradeSoundManager soundManager;
    [SerializeField] private ClassUnlocker unlocker;
    [SerializeField] private TextMeshProUGUI totalScienceText;
    [SerializeField] private TextMeshProUGUI priceForUpgrade;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI damageUpTextrog;
    [SerializeField] private TextMeshProUGUI hpUpTextrog;
    [SerializeField] private Slider upgradeRogueSlider;

    [SerializeField] private List<Sprite> bodySprites;
    [SerializeField] private List<Sprite> wepSprites;

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
                soundManager.PlayUpgradeSuccessSound();
                FindObjectOfType<OtherScene>().UpdateTotalScienceText();
                UpdateRogueStatsText();
                unlocker.UpgradeImagesRogue(currentLevelRogue);
                UpdatePriceForUpgrade(currentLevelRogue);
                upgradeRogueSlider.value = (float)(currentLevelRogue - 1) / (float)(playerRogue.upgradeLevels.Count - 1);

                PlayerPrefs.SetFloat(UpgradeSliderRogueValuePrefKey, upgradeRogueSlider.value);
                PlayerPrefs.SetInt(CurrentLevelRogPrefKey, currentLevelRogue);
            }
            else
            {
                soundManager.PlayUpgradeFailSound();
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
            new UpgradeRogues
            {
                levelrog = 1,
                hprog = 150,
                damagerog = 150,
                costrog = 0,
                damageUpTextrog = 50,
                hpUpTextrog = 50,
                bodySprite = bodySprites[0],
                wepSprite = wepSprites[0]
            },
            new UpgradeRogues
            {
                levelrog = 2,
                hprog = 200,
                damagerog = 200,
                costrog = 300,
                damageUpTextrog = 100,
                hpUpTextrog = 100,
                bodySprite = bodySprites[1],
                wepSprite = wepSprites[1]
            },
            new UpgradeRogues
            {
                levelrog = 3,
                hprog = 300,
                damagerog = 500,
                costrog = 450,
                damageUpTextrog = 0,
                hpUpTextrog = 0,
                bodySprite = bodySprites[2],
                wepSprite = wepSprites[2]
            }
        };
    }

    private void UpgradePlayer(int levelrog)
    {
        if (levelrog > 1 && levelrog <= playerRogue.upgradeLevels.Count)
        {
            playerRogue.HP = playerRogue.upgradeLevels[levelrog - 1].hprog;
            playerRogue.damage = playerRogue.upgradeLevels[levelrog - 1].damagerog;
            playerRogue.SavePlayerStats();
        }
    }
}
