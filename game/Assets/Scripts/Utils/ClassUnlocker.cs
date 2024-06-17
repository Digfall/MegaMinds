using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
public class ClassUnlocker : MonoBehaviour
{
    [SerializeField] private Button rogueButton;
    [SerializeField] private Button tankButton;

    [SerializeField] private PlayerWarrior playerWarrior;
    [SerializeField] private Image levelWarImage;
    [SerializeField] private List<Sprite> levelWarSprites;

    [SerializeField] private PlayerTank playerTank;
    [SerializeField] private Image levelTankImage;
    [SerializeField] private List<Sprite> levelTankSprites;

    [SerializeField] private PlayerRanger playerRanger;
    [SerializeField] private Image levelRangerImage;
    [SerializeField] private List<Sprite> levelRangerSprites;

    [SerializeField] private PlayerRogue playerRogue;
    [SerializeField] private Image levelRogueImage;
    [SerializeField] private List<Sprite> levelRogueSprites;

    [SerializeField] private int rogueCost = 200;
    [SerializeField] private int tankCost = 250;
    [SerializeField] private int currentLevelWar = 1;
    [SerializeField] private int currentLevelRogue = 1;
    [SerializeField] private int currentLevelRanger = 1;
    [SerializeField] private int currentLevelTank = 1;

    private const string RogueUnlockedPrefKey = "RogueUnlocked";
    private const string TankUnlockedPrefKey = "TankUnlocked";

    private const string CurrentLevelWarPrefKey = "CurrentLevelWar";
    private const string CurrentLevelRogPrefKey = "CurrentLevelRog";
    private const string CurrentLevelRngPrefKey = "CurrentLevelRng";
    private const string CurrentLevelTankPrefKey = "CurrentLevelTank";

    private void Start()
    {

        currentLevelWar = PlayerPrefs.GetInt(CurrentLevelWarPrefKey, 1);
        currentLevelRogue = PlayerPrefs.GetInt(CurrentLevelRogPrefKey, 1);
        currentLevelRanger = PlayerPrefs.GetInt(CurrentLevelRngPrefKey, 1);
        currentLevelTank = PlayerPrefs.GetInt(CurrentLevelTankPrefKey, 1);
        UpgradeImageWar(currentLevelWar);
        UpgradeImageRang(currentLevelRanger);
        UpgradeImageRog(currentLevelRogue);
        UpgradeImageTank(currentLevelTank);
        bool isRogueUnlocked = PlayerPrefs.GetInt(RogueUnlockedPrefKey, 0) == 1;
        bool isTankUnlocked = PlayerPrefs.GetInt(TankUnlockedPrefKey, 0) == 1;

        rogueButton.gameObject.SetActive(!isRogueUnlocked);
        tankButton.gameObject.SetActive(!isTankUnlocked);

        rogueButton.interactable = !isRogueUnlocked;
        tankButton.interactable = !isTankUnlocked;

        FindObjectOfType<OtherScene>().UpdateTotalScienceText();
    }

    public void UnlockRogue()
    {
        UnlockCharacter(RogueUnlockedPrefKey, rogueCost, rogueButton);
    }

    public void UnlockTank()
    {
        UnlockCharacter(TankUnlockedPrefKey, tankCost, tankButton);
    }

    private void UnlockCharacter(string prefKey, int cost, Button button)
    {
        if (GameManager.TotalScience >= cost)
        {
            GameManager.TotalScience -= cost;
            PlayerPrefs.SetInt(prefKey, 1);
            PlayerPrefs.Save();
            button.gameObject.SetActive(false);
            FindObjectOfType<OtherScene>().UpdateTotalScienceText();
        }
    }

    private void UpgradeImageWar(int levelwar)
    {
        if (levelwar > 0 && levelwar <= playerWarrior.upgradeLevels.Count)
        {
            if (levelwar - 1 < levelWarSprites.Count)
            {
                levelWarImage.sprite = levelWarSprites[levelwar - 1];
            }
        }

    }
    private void UpgradeImageTank(int levelTank)
    {
        if (levelTank > 0 && levelTank <= playerTank.upgradeLevels.Count)
        {
            if (levelTank - 1 < levelTankSprites.Count)
            {
                levelTankImage.sprite = levelTankSprites[levelTank - 1];
            }
        }
    }
    private void UpgradeImageRang(int levelran)
    {
        if (levelran > 0 && levelran <= playerRanger.upgradeLevels.Count)
        {
            if (levelran - 1 < levelRangerSprites.Count)
            {
                levelRangerImage.sprite = levelRangerSprites[levelran - 1];
            }
        }
    }
    private void UpgradeImageRog(int levelrog)
    {
        if (levelrog > 0 && levelrog <= playerRogue.upgradeLevels.Count)
        {
            if (levelrog - 1 < levelRogueSprites.Count)
            {
                levelRogueImage.sprite = levelRogueSprites[levelrog - 1];
            }
        }
    }

}
