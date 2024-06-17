using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel;

    void Start()
    {
        LoadProgress();
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void CompleteLevel(int levelNumber)
    {
        if (levelNumber > currentLevel)
        {
            currentLevel = levelNumber;
            PlayerPrefs.SetInt("LevelReached", currentLevel);
            PlayerPrefs.Save();
        }
    }

    public void LoadProgress()
    {
        currentLevel = PlayerPrefs.GetInt("LevelReached", 1);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
