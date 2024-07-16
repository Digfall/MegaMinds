using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private AudioSource transitionSound; // Добавьте ваш AudioSource для звука перехода

    public void SwitchScene()
    {
        StartCoroutine(SwitchSceneWithSound());
    }

    private IEnumerator SwitchSceneWithSound()
    {

        if (transitionSound != null)
        {
            transitionSound.Play();
            float clipLength = transitionSound.clip.length;

            yield return new WaitForSecondsRealtime(clipLength);
        }
        SceneManager.LoadScene(targetSceneName);

    }

    // Очистка данных PlayerPrefs
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
