using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherChooseLevel : MonoBehaviour
{
    private string targetSceneName;

    [SerializeField] private AudioSource transitionSound; // Добавьте ваш AudioSource для звука перехода

    public void SelectScene(string sceneName)
    {
        targetSceneName = sceneName;
    }


    public void SwitchScene()
    {
        StartCoroutine(SwitchSceneWithSound());
    }


    private IEnumerator SwitchSceneWithSound()
    {
        if (transitionSound != null && !string.IsNullOrEmpty(targetSceneName))
        {
            transitionSound.Play();
            yield return new WaitForSeconds(transitionSound.clip.length);
        }

        SceneManager.LoadScene(targetSceneName);
    }
}
