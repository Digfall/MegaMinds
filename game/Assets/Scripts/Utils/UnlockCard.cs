using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnlockCard : MonoBehaviour
{

    [SerializeField] private Button rogueActionButton;
    [SerializeField] private Button tankActionButton;

    private const string RogueUnlockedPrefKey = "RogueUnlocked";
    private const string TankUnlockedPrefKey = "TankUnlocked";

    private void Start()
    {
        CheckAndUpdateButtons();

    }

    private void CheckAndUpdateButtons()
    {
        bool isRogueUnlocked = PlayerPrefs.GetInt(RogueUnlockedPrefKey, 0) == 1;
        bool isTankUnlocked = PlayerPrefs.GetInt(TankUnlockedPrefKey, 0) == 1;

        rogueActionButton.gameObject.SetActive(isRogueUnlocked);
        tankActionButton.gameObject.SetActive(isTankUnlocked);
    }


}
