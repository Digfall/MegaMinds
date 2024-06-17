using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    public void SwitchScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
    //Очистка даты playerprefs
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
