using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite availableSprite;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 < levelReached)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].GetComponent<SpriteRenderer>().sprite = unlockedSprite;
                int levelIndex = i + 1;
                levelButtons[i].onClick.AddListener(() => SelectLevel(levelIndex));
            }
            else if (i + 1 == levelReached)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].GetComponent<SpriteRenderer>().sprite = availableSprite;
                int levelIndex = i + 1;
                levelButtons[i].onClick.AddListener(() => SelectLevel(levelIndex));
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<SpriteRenderer>().sprite = lockedSprite;
            }
        }
    }

    public void SelectLevel(int levelNumber)
    {
        string sceneName = "Level" + levelNumber;
        FindObjectOfType<SceneSwitcherChooseLevel>().SelectScene(sceneName);
    }
}
