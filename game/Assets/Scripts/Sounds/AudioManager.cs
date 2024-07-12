using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private const string SFXVolumePrefsKey = "SFXVolume";
    private float sfxVolume = 1f;

    void Start()
    {
        UpdateAllSFXAudioSources();
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Загрузка сохранённой громкости эффектов
            sfxVolume = PlayerPrefs.GetFloat(SFXVolumePrefsKey, 1f);

            // Обновление громкости всех аудиоисточников после загрузки сцены
            UpdateAllSFXAudioSources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float SFXVolume
    {
        get => sfxVolume;
        set
        {
            sfxVolume = value;
            PlayerPrefs.SetFloat(SFXVolumePrefsKey, sfxVolume);
            PlayerPrefs.Save();
            UpdateAllSFXAudioSources();
        }
    }

    private void OnEnable()
    {
        // Обновление громкости всех аудиоисточников при включении скрипта
        UpdateAllSFXAudioSources();
    }

    public void UpdateAllSFXAudioSources()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (var audioSource in audioSources)
        {
            if (audioSource.CompareTag("SFX"))
            {
                audioSource.volume = sfxVolume;
            }
        }
    }
}
