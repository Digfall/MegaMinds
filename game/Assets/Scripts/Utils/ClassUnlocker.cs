using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClassUnlocker : MonoBehaviour
{
    [SerializeField] private Button rogueUnlockButton;
    [SerializeField] private Button tankUnlockButton;
    [SerializeField] private Button rogueAvaibleButton;
    [SerializeField] private Button tankAvaibleButton;

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
    [SerializeField] private int currentLevel = 1;

    private const string RogueUnlockedPrefKey = "RogueUnlocked";
    private const string TankUnlockedPrefKey = "TankUnlocked";

    private const string CurrentLevelWarPrefKey = "CurrentLevelWar";
    private const string CurrentLevelRogPrefKey = "CurrentLevelRog";
    private const string CurrentLevelRngPrefKey = "CurrentLevelRng";
    private const string CurrentLevelTankPrefKey = "CurrentLevelTank";

    [SerializeField] private UpgradeSoundManager soundManager;

    private void Start()
    {
        LoadPlayerPrefs();

        // Устанавливаем изображения и интерактивность кнопок на основе сохраненных данных
        UpgradeImagesWar(currentLevelWar);
        UpgradeImagesRanger(currentLevelRanger);
        UpgradeImagesRogue(currentLevelRogue);
        UpgradeImagesTank(currentLevelTank);

        // Проверяем, разблокированы ли рога и танк
        bool isRogueUnlocked = PlayerPrefs.GetInt(RogueUnlockedPrefKey, 0) == 1;
        bool isTankUnlocked = PlayerPrefs.GetInt(TankUnlockedPrefKey, 0) == 1;

        // Активируем или деактивируем кнопки разблокировки в зависимости от их состояния
        rogueAvaibleButton.gameObject.SetActive(!isRogueUnlocked && currentLevel >= 7);
        tankAvaibleButton.gameObject.SetActive(!isTankUnlocked && currentLevel >= 4);
        rogueUnlockButton.gameObject.SetActive(!isRogueUnlocked && currentLevel >= 7);
        tankUnlockButton.gameObject.SetActive(!isTankUnlocked && currentLevel >= 4);

        // Устанавливаем доступность кнопок улучшения
        rogueUpgradeButton.interactable = isRogueUnlocked;
        tankUpgradeButton.interactable = isTankUnlocked;

        // Обновляем текст общего количества ресурсов
        FindObjectOfType<OtherScene>().UpdateTotalScienceText();
    }
    private void Update()
    {
        // Проверяем текущий уровень игры
        if (currentLevel >= 4)
        {
            // Выключаем кнопку, если достигнут уровень 3 и она была активна
            if (tankAvaibleButton.gameObject.activeSelf)
            {
                tankAvaibleButton.gameObject.SetActive(false);
            }
        }
        else
        {
            // Включаем кнопку, если уровень игры меньше 3 и она была выключена
            if (!tankAvaibleButton.gameObject.activeSelf)
            {
                tankAvaibleButton.gameObject.SetActive(true);
            }
        }

        if (currentLevel >= 7)
        {
            // Выключаем кнопку, если достигнут уровень 6 и она была активна
            if (rogueAvaibleButton.gameObject.activeSelf)
            {
                rogueAvaibleButton.gameObject.SetActive(false);
            }
        }
        else
        {
            // Включаем кнопку, если уровень игры меньше 6 и она была выключена
            if (!rogueAvaibleButton.gameObject.activeSelf)
            {
                rogueAvaibleButton.gameObject.SetActive(true);
            }
        }
    }

    private void LoadPlayerPrefs()
    {
        currentLevel = PlayerPrefs.GetInt("LevelReached", 1);
        currentLevelWar = PlayerPrefs.GetInt(CurrentLevelWarPrefKey, 1);
        currentLevelRogue = PlayerPrefs.GetInt(CurrentLevelRogPrefKey, 1);
        currentLevelRanger = PlayerPrefs.GetInt(CurrentLevelRngPrefKey, 1);
        currentLevelTank = PlayerPrefs.GetInt(CurrentLevelTankPrefKey, 1);
    }

    public void UnlockRogue()
    {
        UnlockCharacter(RogueUnlockedPrefKey, rogueCost, rogueUnlockButton, rogueUpgradeButton);
        PlayerPrefs.SetInt("LevelReached", currentLevel);
        PlayerPrefs.Save();
    }

    public void UnlockTank()
    {
        UnlockCharacter(TankUnlockedPrefKey, tankCost, tankUnlockButton, tankUpgradeButton);
        PlayerPrefs.SetInt("LevelReached", currentLevel);
        PlayerPrefs.Save();
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
            levelWarUnitImage.sprite = levelWarUnitSprites[levelwar - 1];
            levelWarBgImage.sprite = levelWarBgSprites[levelwar - 1];
            levelWarWepImage.sprite = levelWarWepSprites[levelwar - 1];
        }
    }

    private void UpgradeImagesTank(int levelTank)
    {
        if (levelTank > 0 && levelTank <= playerTank.upgradeLevels.Count)
        {
            levelTankUnitImage.sprite = levelTankUnitSprites[levelTank - 1];
            levelTankBgImage.sprite = levelTankBgSprites[levelTank - 1];
            levelTankWepImage.sprite = levelTankWepSprites[levelTank - 1];
        }
    }

    private void UpgradeImagesRanger(int levelran)
    {
        if (levelran > 0 && levelran <= playerRanger.upgradeLevels.Count)
        {
            levelRangerUnitImage.sprite = levelRangerUnitSprites[levelran - 1];
            levelRangerBgImage.sprite = levelRangerBgSprites[levelran - 1];
            levelRangerWepImage.sprite = levelRangerWepSprites[levelran - 1];
        }
    }

    private void UpgradeImagesRogue(int levelrog)
    {
        if (levelrog > 0 && levelrog <= playerRogue.upgradeLevels.Count)
        {
            levelRogueUnitImage.sprite = levelRogueUnitSprites[levelrog - 1];
            levelRogueBgImage.sprite = levelRogueBgSprites[levelrog - 1];
            levelRogueWepImage.sprite = levelRogueWepSprites[levelrog - 1];
        }
    }
}
