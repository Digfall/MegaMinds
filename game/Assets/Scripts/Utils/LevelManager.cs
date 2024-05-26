using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;

    void Start()
    {
        LoadProgress();
    }

    // Метод для загрузки уровня по имени
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // Метод для завершения уровня и сохранения прогресса
    public void CompleteLevel(int levelNumber)
    {
        // Если пройденный уровень больше текущего сохраненного, обновляем прогресс
        if (levelNumber > currentLevel)
        {
            currentLevel = levelNumber;
            PlayerPrefs.SetInt("LevelReached", currentLevel);
            PlayerPrefs.Save();
        }
    }

    public void LoadProgress()
    {
        currentLevel = PlayerPrefs.GetInt("LevelReached", 1); // По умолчанию первый уровень
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
