using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gamePaused = false;
    private static int totalScience = 0;

    void Start()
    {
        // Начать сцену
        Time.timeScale = 1f; // Возобновляем время
    }

    public static int TotalScience
    {
        get { return totalScience; }
        set { totalScience = value; }
    }

    // Сделаем функцию TogglePause доступной извне
    public void TogglePauseOn()
    {
        Time.timeScale = 0f; // Приостанавливаем время
        gamePaused = true; // Устанавливаем флаг, что игра приостановлена

    }
    public void TogglePauseOff()
    {
        Time.timeScale = 1f; // Возобновляем время
        gamePaused = false; // Устанавливаем флаг, что игра возобновлена
    }
}
