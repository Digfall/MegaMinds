using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //private bool gameStarted = false;
    private bool gamePaused = false;
    private static int totalScience = 0;

    // void Start()
    // {
    //     Time.timeScale = 0f; // Приостанавливаем время при старте игры
    // }

    public static int TotalScience
    {
        get { return totalScience; }
        set { totalScience = value; }
    }
    public void ToggleStartPause()
    {
        // if (!gameStarted)
        // {
        //     StartGame();
        // }
        // else
        {
            TogglePause();
        }
    }

    // void StartGame()
    // {
    //     Time.timeScale = 1f; // Возобновляем время
    //     gameStarted = true; // Устанавливаем флаг, что игра уже начата
    // }

    void TogglePause()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0f; // Приостанавливаем время
            gamePaused = true; // Устанавливаем флаг, что игра приостановлена
        }
        else
        {
            Time.timeScale = 1f; // Возобновляем время
            gamePaused = false; // Устанавливаем флаг, что игра возобновлена
        }
    }
}