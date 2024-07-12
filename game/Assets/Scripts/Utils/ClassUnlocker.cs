using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClassUnlocker : MonoBehaviour
{
    [SerializeField] private Button rogueUnlockButton;
    [SerializeField] private Button tankUnlockButton;

    [SerializeField] private Button rogueUpgradeButton;
    [SerializeField] private Button tankUpgradeButton;
    [Header("WARRIOR")]
    [SerializeField] private PlayerWarrior playerWarrior;
    [SerializeField] private Image levelWarUnitImage;
    [SerializeField] private Image levelWarBgImage;
    [SerializeField] private Image levelWarWepImage;
    [SerializeField] private List<Sprite> levelWarUnitSprites;
    [SerializeField] private List<Sprite> levelWarBgSprites;
    [SerializeField] private List<Sprite> levelWarWepSprites;
    [Header("TANK")]
    [SerializeField] private PlayerTank playerTank;
    [SerializeField] private Image levelTankUnitImage;
    [SerializeField] private Image levelTankBgImage;
    [SerializeField] private Image levelTankWepImage;
    [SerializeField] private List<Sprite> levelTankUnitSprites;
    [SerializeField] private List<Sprite> levelTankBgSprites;
    [SerializeField] private List<Sprite> levelTankWepSprites;
    [Header("RANGER")]
    [SerializeField] private PlayerRanger playerRanger;
    [SerializeField] private Image levelRangerUnitImage;
    [SerializeField] private Image levelRangerBgImage;
    [SerializeField] private Image levelRangerWepImage;
    [SerializeField] private List<Sprite> levelRangerUnitSprites;
    [SerializeField] private List<Sprite> levelRangerBgSprites;
    [SerializeField] private List<Sprite> levelRangerWepSprites;
    [Header("ROGUE")]
    [SerializeField] private PlayerRogue playerRogue;
    [SerializeField] private Image levelRogueUnitImage;
    [SerializeField] private Image levelRogueBgImage;
    [SerializeField] private Image levelRogueWepImage;
    [SerializeField] private List<Sprite> levelRogueUnitSprites;
    [SerializeField] private List<Sprite> levelRogueBgSprites;
    [SerializeField] private List<Sprite> levelRogueWepSprites;

    [SerializeField] private int rogueCost = 200;
    [SerializeField] private int tankCost = 250;
    private int currentLevelWar = 1;
    private int currentLevelRogue = 1;
    private int currentLevelRanger = 1;
    private int currentLevelTank = 1;

    private const string RogueUnlockedPrefKey = "RogueUnlocked";
    private const string TankUnlockedPrefKey = "TankUnlocked";

    private const string CurrentLevelWarPrefKey = "CurrentLevelWar";
    private const string CurrentLevelRogPrefKey = "CurrentLevelRog";
    private const string CurrentLevelRngPrefKey = "CurrentLevelRng";
    private const string CurrentLevelTankPrefKey = "CurrentLevelTank";
    [SerializeField] private UpgradeSoundManager soundManager;

    private void Start()
    {
        currentLevelWar = PlayerPrefs.GetInt(CurrentLevelWarPrefKey, 1);
        currentLevelRogue = PlayerPrefs.GetInt(CurrentLevelRogPrefKey, 1);
        currentLevelRanger = PlayerPrefs.GetInt(CurrentLevelRngPrefKey, 1);
        currentLevelTank = PlayerPrefs.GetInt(CurrentLevelTankPrefKey, 1);
        UpgradeImagesWar(currentLevelWar);
        UpgradeImagesRanger(currentLevelRanger);
        UpgradeImagesRogue(currentLevelRogue);
        UpgradeImagesTank(currentLevelTank);

        bool isRogueUnlocked = PlayerPrefs.GetInt(RogueUnlockedPrefKey, 0) == 1;
        bool isTankUnlocked = PlayerPrefs.GetInt(TankUnlockedPrefKey, 0) == 1;

        rogueUnlockButton.gameObject.SetActive(!isRogueUnlocked);
        tankUnlockButton.gameObject.SetActive(!isTankUnlocked);

        rogueUpgradeButton.interactable = isRogueUnlocked;
        tankUpgradeButton.interactable = isTankUnlocked;

        FindObjectOfType<OtherScene>().UpdateTotalScienceText();
    }

    public void UnlockRogue()
    {
        UnlockCharacter(RogueUnlockedPrefKey, rogueCost, rogueUnlockButton, rogueUpgradeButton);
    }

    public void UnlockTank()
    {
        UnlockCharacter(TankUnlockedPrefKey, tankCost, tankUnlockButton, tankUpgradeButton);
    }

    private void UnlockCharacter(string prefKey, int cost, Button unlockButton, Button upgradeButton)
    {
        if (GameManager.TotalScience >= cost)
        {
            GameManager.TotalScience -= cost;
            PlayerPrefs.SetInt(prefKey, 1);
            PlayerPrefs.Save();
            unlockButton.gameObject.SetActive(false);
            upgradeButton.interactable = true;
            FindObjectOfType<OtherScene>().UpdateTotalScienceText();
        }
        else
        {
            soundManager.PlayUpgradeFailSound();
        }
    }

    private void UpgradeImagesWar(int levelwar)
    {
        if (levelwar > 0 && levelwar <= playerWarrior.upgradeLevels.Count)
        {
            if (levelwar - 1 < levelWarUnitSprites.Count)
            {
                levelWarUnitImage.sprite = levelWarUnitSprites[levelwar - 1];
            }
            if (levelwar - 1 < levelWarBgSprites.Count)
            {
                levelWarBgImage.sprite = levelWarBgSprites[levelwar - 1];
            }
            if (levelwar - 1 < levelWarWepSprites.Count)
            {
                levelWarWepImage.sprite = levelWarWepSprites[levelwar - 1];
            }
        }
    }

    private void UpgradeImagesTank(int levelTank)
    {
        if (levelTank > 0 && levelTank <= playerTank.upgradeLevels.Count)
        {
            if (levelTank - 1 < levelTankUnitSprites.Count)
            {
                levelTankUnitImage.sprite = levelTankUnitSprites[levelTank - 1];
            }
            if (levelTank - 1 < levelTankBgSprites.Count)
            {
                levelTankBgImage.sprite = levelTankBgSprites[levelTank - 1];
            }
            if (levelTank - 1 < levelTankWepSprites.Count)
            {
                levelTankWepImage.sprite = levelTankWepSprites[levelTank - 1];
            }
        }
    }

    private void UpgradeImagesRanger(int levelran)
    {
        if (levelran > 0 && levelran <= playerRanger.upgradeLevels.Count)
        {
            if (levelran - 1 < levelRangerUnitSprites.Count)
            {
                levelRangerUnitImage.sprite = levelRangerUnitSprites[levelran - 1];
            }
            if (levelran - 1 < levelRangerBgSprites.Count)
            {
                levelRangerBgImage.sprite = levelRangerBgSprites[levelran - 1];
            }
            if (levelran - 1 < levelRangerWepSprites.Count)
            {
                levelRangerWepImage.sprite = levelRangerWepSprites[levelran - 1];
            }
        }
    }

    private void UpgradeImagesRogue(int levelrog)
    {
        if (levelrog > 0 && levelrog <= playerRogue.upgradeLevels.Count)
        {
            if (levelrog - 1 < levelRogueUnitSprites.Count)
            {
                levelRogueUnitImage.sprite = levelRogueUnitSprites[levelrog - 1];
            }
            if (levelrog - 1 < levelRogueBgSprites.Count)
            {
                levelRogueBgImage.sprite = levelRogueBgSprites[levelrog - 1];
            }
            if (levelrog - 1 < levelRogueWepSprites.Count)
            {
                levelRogueWepImage.sprite = levelRogueWepSprites[levelrog - 1];
            }
        }
    }
}
