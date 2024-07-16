using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string MusicVolumePrefsKey = "MusicVolume";
    private float musicVolume = 1f;
    public AudioSource musicSource;

    public AudioClip menuMusic; // Музыка для меню
    public AudioClip battleMusic; // Музыка для боя

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Загрузка сохранённой громкости музыки
            musicVolume = PlayerPrefs.GetFloat(MusicVolumePrefsKey, 1f);
            if (musicSource != null)
            {
                musicSource.volume = musicVolume;
            }

            // Подписка на событие смены сцены
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Определение, какую музыку играть в зависимости от сцены
        if (IsMenuScene(scene.name))
        {
            PlayMusic(menuMusic);
        }
        else if (IsBattleScene(scene.name))
        {
            PlayMusic(battleMusic);
        }
    }

    private bool IsMenuScene(string sceneName)
    {
        // Названия сцен меню
        return sceneName == "Entry" || sceneName == "ArmyChoose" || sceneName == "LevelChoose";
    }

    private bool IsBattleScene(string sceneName)
    {
        // Названия уровней
        return sceneName.StartsWith("Level");

    }

    public float MusicVolume
    {
        get => musicVolume;
        set
        {
            musicVolume = value;
            PlayerPrefs.SetFloat(MusicVolumePrefsKey, musicVolume);
            PlayerPrefs.Save();
            if (musicSource != null)
            {
                musicSource.volume = musicVolume;
            }
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip)
        {
            return;
        }

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    private void OnDestroy()
    {
        // Отписка от события смены сцены при уничтожении объекта
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
