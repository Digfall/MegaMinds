using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherChooseLevel : MonoBehaviour
{
    private string targetSceneName;

    // Метод для выбора сцены, вызываемый кнопками уровня
    public void SelectScene(string sceneName)
    {
        targetSceneName = sceneName;
    }

    // Метод для переключения сцены, вызываемый кнопкой "Play"
    public void SwitchScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.Log("Сначала выберите сцену!");
        }
    }
}
