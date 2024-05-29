using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ClassUnlocker : MonoBehaviour
{
    public Button rogueButton;
    public Button tankButton;

    public int rogueCost = 200;
    public int tankCost = 250;

    private const string RogueUnlockedPrefKey = "RogueUnlocked";
    private const string TankUnlockedPrefKey = "TankUnlocked";

    private void Start()
    {

        bool isRogueUnlocked = PlayerPrefs.GetInt(RogueUnlockedPrefKey, 0) == 1;
        bool isTankUnlocked = PlayerPrefs.GetInt(TankUnlockedPrefKey, 0) == 1;


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
            button.gameObject.SetActive(false);
            FindObjectOfType<OtherScene>().UpdateTotalScienceText();
        }
        else
        {
            Debug.Log("Недостаточно науки для открытия карточки.");
        }
    }

}
