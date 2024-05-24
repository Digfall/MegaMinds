using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] levelButtons;
    public Sprite unlockedSprite;
    public Sprite lockedSprite;
    public Sprite availableSprite;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 < levelReached)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].GetComponent<Image>().sprite = unlockedSprite;
                int levelIndex = i + 1;
                levelButtons[i].onClick.AddListener(() => SelectLevel(levelIndex));
            }
            else if (i + 1 == levelReached)
            {
                // Уровень доступен и не пройден
                levelButtons[i].interactable = true;
                levelButtons[i].GetComponent<Image>().sprite = availableSprite;
                int levelIndex = i + 1;
                levelButtons[i].onClick.AddListener(() => SelectLevel(levelIndex));
            }
            else
            {
                // Уровень еще не доступен
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().sprite = lockedSprite;
            }
        }
    }

    public void SelectLevel(int levelNumber)
    {
        string sceneName = "Level" + levelNumber;
        FindObjectOfType<SceneSwitcherChooseLevel>().SelectScene(sceneName);
    }
}
