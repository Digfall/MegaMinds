using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gamePaused = false;
    private static int totalScience = 0;
    private const string CurrentTotalSciencePrefKey = "TotalScienceKey";

    void Start()
    {
        totalScience = PlayerPrefs.GetInt(CurrentTotalSciencePrefKey, 0);
        Time.timeScale = 1f;
    }

    public static int TotalScience
    {
        get { return totalScience; }
        set
        {
            totalScience = value;
            PlayerPrefs.SetInt(CurrentTotalSciencePrefKey, totalScience);
            PlayerPrefs.Save();
        }
    }


    public void TogglePauseOn()
    {
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void TogglePauseOff()
    {
        Time.timeScale = 1f;
        gamePaused = false;
    }
}
