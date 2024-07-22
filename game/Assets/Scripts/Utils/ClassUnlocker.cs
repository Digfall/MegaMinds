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
    // [SerializeField] private Image levelWarUnitImage;
    [SerializeField] private Image levelWarBgImage;
    // [SerializeField] private Image levelWarWepImage;
    // [SerializeField] private List<Sprite> levelWarUnitSprites;
    [SerializeField] private List<Sprite> levelWarBgSprites;
    // [SerializeField] private List<Sprite> levelWarWepSprites;
    // [SerializeField] private Image levelWarUnitImageUPGRUI;
    [SerializeField] private Image levelWarBgImageUPGRUI;
    // [SerializeField] private Image levelWarWepImageUPGRUI;
    // [SerializeField] private List<Sprite> levelWarUnitSpritesUPGRUI;
    [SerializeField] private List<Sprite> levelWarBgSpritesUPGRUI;
    // [SerializeField] private List<Sprite> levelWarWepSpritesUPGRUI;

    [Header("TANK")]
    [SerializeField] private PlayerTank playerTank;
    // [SerializeField] private Image levelTankUnitImage;
    [SerializeField] private Image levelTankBgImage;
    // [SerializeField] private Image levelTankWepImage;
    // [SerializeField] private List<Sprite> levelTankUnitSprites;
    [SerializeField] private List<Sprite> levelTankBgSprites;
    // [SerializeField] private List<Sprite> levelTankWepSprites;
    // [SerializeField] private Image levelTankUnitImageUPGRUI;
    [SerializeField] private Image levelTankBgImageUPGRUI;
    // [SerializeField] private Image levelTankWepImageUPGRUI;
    // [SerializeField] private List<Sprite> levelTankUnitSpritesUPGRUI;
    [SerializeField] private List<Sprite> levelTankBgSpritesUPGRUI;
    // [SerializeField] private List<Sprite> levelTankWepSpritesUPGRUI;

    [Header("RANGER")]
    [SerializeField] private PlayerRanger playerRanger;
    // [SerializeField] private Image levelRangerUnitImage;
    [SerializeField] private Image levelRangerBgImage;
    // [SerializeField] private Image levelRangerWepImage;
    // [SerializeField] private List<Sprite> levelRangerUnitSprites;
    [SerializeField] private List<Sprite> levelRangerBgSprites;
    // [SerializeField] private List<Sprite> levelRangerWepSprites;
    // [SerializeField] private Image levelRangerUnitImageUPGRUI;
    [SerializeField] private Image levelRangerBgImageUPGRUI;
    // [SerializeField] private Image levelRangerWepImageUPGRUI;
    // [SerializeField] private List<Sprite> levelRangerUnitSpritesUPGRUI;
    [SerializeField] private List<Sprite> levelRangerBgSpritesUPGRUI;
    // [SerializeField] private List<Sprite> levelRangerWepSpritesUPGRUI;

    [Header("ROGUE")]
    [SerializeField] private PlayerRogue playerRogue;
    // [SerializeField] private Image levelRogueUnitImage;
    [SerializeField] private Image levelRogueBgImage;
    // [SerializeField] private Image levelRogueWepImage;
    // [SerializeField] private List<Sprite> levelRogueUnitSprites;
    [SerializeField] private List<Sprite> levelRogueBgSprites;
    // [SerializeField] private List<Sprite> levelRogueWepSprites;
    // [SerializeField] private Image levelRogueUnitImageUPGRUI;
    [SerializeField] private Image levelRogueBgImageUPGRUI;
    // [SerializeField] private Image levelRogueWepImageUPGRUI;
    // [SerializeField] private List<Sprite> levelRogueUnitSpritesUPGRUI;
    [SerializeField] private List<Sprite> levelRogueBgSpritesUPGRUI;
    // [SerializeField] private List<Sprite> levelRogueWepSpritesUPGRUI;

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

    public void UpgradeImagesWar(int levelwar)
    {
        int spriteIndex = GetSpriteIndex(levelwar);
        if (spriteIndex != -1 && spriteIndex < levelWarBgSprites.Count)
        {
            // levelWarUnitImage.sprite = levelWarUnitSprites[spriteIndex];
            levelWarBgImage.sprite = levelWarBgSprites[spriteIndex];
            // levelWarWepImage.sprite = levelWarWepSprites[spriteIndex];
            // levelWarUnitImageUPGRUI.sprite = levelWarUnitSpritesUPGRUI[spriteIndex];
            levelWarBgImageUPGRUI.sprite = levelWarBgSpritesUPGRUI[spriteIndex];
            // levelWarWepImageUPGRUI.sprite = levelWarWepSpritesUPGRUI[spriteIndex];
        }
    }

    public void UpgradeImagesTank(int levelTank)
    {
        int spriteIndex = GetSpriteIndex(levelTank);
        if (spriteIndex != -1 && spriteIndex < levelTankBgSprites.Count)
        {
            // levelTankUnitImage.sprite = levelTankUnitSprites[spriteIndex];
            levelTankBgImage.sprite = levelTankBgSprites[spriteIndex];
            // levelTankWepImage.sprite = levelTankWepSprites[spriteIndex];
            // levelTankUnitImageUPGRUI.sprite = levelTankUnitSpritesUPGRUI[spriteIndex];
            levelTankBgImageUPGRUI.sprite = levelTankBgSpritesUPGRUI[spriteIndex];
            // levelTankWepImageUPGRUI.sprite = levelTankWepSpritesUPGRUI[spriteIndex];
        }
    }

    public void UpgradeImagesRanger(int levelran)
    {
        int spriteIndex = GetSpriteIndex(levelran);
        if (spriteIndex != -1 && spriteIndex < levelRangerBgSprites.Count)
        {
            // levelRangerUnitImage.sprite = levelRangerUnitSprites[spriteIndex];
            levelRangerBgImage.sprite = levelRangerBgSprites[spriteIndex];
            // levelRangerWepImage.sprite = levelRangerWepSprites[spriteIndex];
            // levelRangerUnitImageUPGRUI.sprite = levelRangerUnitSpritesUPGRUI[spriteIndex];
            levelRangerBgImageUPGRUI.sprite = levelRangerBgSpritesUPGRUI[spriteIndex];
            // levelRangerWepImageUPGRUI.sprite = levelRangerWepSpritesUPGRUI[spriteIndex];
        }
    }

    public void UpgradeImagesRogue(int levelrog)
    {
        int spriteIndex = GetSpriteIndex(levelrog);
        if (spriteIndex != -1 && spriteIndex < levelRogueBgSprites.Count)
        {
            // levelRogueUnitImage.sprite = levelRogueUnitSprites[spriteIndex];
            levelRogueBgImage.sprite = levelRogueBgSprites[spriteIndex];
            // levelRogueWepImage.sprite = levelRogueWepSprites[spriteIndex];
            // levelRogueUnitImageUPGRUI.sprite = levelRogueUnitSpritesUPGRUI[spriteIndex];
            levelRogueBgImageUPGRUI.sprite = levelRogueBgSpritesUPGRUI[spriteIndex];
            // levelRogueWepImageUPGRUI.sprite = levelRogueWepSpritesUPGRUI[spriteIndex];
        }
    }

    private int GetSpriteIndex(int level)
    {
        if (level >= 1 && level <= 4)
        {
            return 0;
        }
        else if (level >= 5 && level <= 7)
        {
            return 1;
        }
        else if (level >= 8 && level <= 10)
        {
            return 2;
        }
        return -1; // Неправильный уровень
    }
}
