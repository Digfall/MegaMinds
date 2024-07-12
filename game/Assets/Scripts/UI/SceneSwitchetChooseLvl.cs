using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherChooseLevel : MonoBehaviour
{
    private string targetSceneName;

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
    }
}
