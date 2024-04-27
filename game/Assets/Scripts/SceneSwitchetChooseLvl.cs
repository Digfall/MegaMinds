using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchetChooseLvl : MonoBehaviour
{
    public string targetSceneName;

    public void SelectScene(string sceneName)
    {
        targetSceneName = sceneName;
    }

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

