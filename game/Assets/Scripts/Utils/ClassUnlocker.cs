using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
public class ClassUnlocker : MonoBehaviour
{
    public Button rogueButton;
    public Button tankButton;

    public PlayerWarrior playerWarrior;
    public Image levelWarImage;
    public List<Sprite> levelWarSprites;

    public PlayerTank playerTank;
    public Image levelTankImage;
    public List<Sprite> levelTankSprites;

    public PlayerRanger playerRanger;
    public Image levelRangerImage;
    public List<Sprite> levelRangerSprites;

    public PlayerRogue playerRogue;
    public Image levelRogueImage;
    public List<Sprite> levelRogueSprites;

    public int rogueCost = 200;
    public int tankCost = 250;
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

    private void Start()
    {

        currentLevelWar = PlayerPrefs.GetInt(CurrentLevelWarPrefKey, 1);
        currentLevelRogue = PlayerPrefs.GetInt(CurrentLevelRogPrefKey, 1);
        currentLevelRanger = PlayerPrefs.GetInt(CurrentLevelRngPrefKey, 1);
        currentLevelTank = PlayerPrefs.GetInt(CurrentLevelTankPrefKey, 1);

        bool isRogueUnlocked = PlayerPrefs.GetInt(RogueUnlockedPrefKey, 0) == 1;
        bool isTankUnlocked = PlayerPrefs.GetInt(TankUnlockedPrefKey, 0) == 1;

        rogueButton.gameObject.SetActive(!isRogueUnlocked);
        tankButton.gameObject.SetActive(!isTankUnlocked);

        rogueButton.interactable = !isRogueUnlocked;
        tankButton.interactable = !isTankUnlocked;
        UpgradeImage(currentLevelWar, currentLevelRogue, currentLevelTank, currentLevelRanger);
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
        else
        {
            Debug.Log("Недостаточно науки для открытия карточки.");
        }
    }

    private void UpgradeImage(int levelwar, int levelran, int levelTank, int levelrog)
    {
        if (levelwar > 0 && levelwar <= playerWarrior.upgradeLevels.Count)
        {
            if (levelwar - 1 < levelWarSprites.Count)
            {
                levelWarImage.sprite = levelWarSprites[levelwar - 1];
            }
        }
        if (levelrog > 0 && levelrog <= playerRogue.upgradeLevels.Count)
        {
            if (levelrog - 1 < levelRogueSprites.Count)
            {
                levelRogueImage.sprite = levelRogueSprites[levelrog - 1];
            }
        }
        if (levelran > 0 && levelran <= playerRanger.upgradeLevels.Count)
        {
            if (levelran - 1 < levelRangerSprites.Count)
            {
                levelRangerImage.sprite = levelRangerSprites[levelran - 1];
            }
        }
        if (levelTank > 0 && levelTank <= playerTank.upgradeLevels.Count)
        {
            if (levelTank - 1 < levelTankSprites.Count)
            {
                levelTankImage.sprite = levelTankSprites[levelTank - 1];
            }
        }
    }
}
