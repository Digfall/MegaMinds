using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string MusicVolumePrefsKey = "MusicVolume";
    private float musicVolume = 1f;
    public AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Загрузка сохранённой громкости музыки
            musicVolume = PlayerPrefs.GetFloat(MusicVolumePrefsKey, 1f);
            AudioManager.Instance?.UpdateAllSFXAudioSources();
            if (musicSource != null)
            {
                musicSource.volume = musicVolume;
            }
        }
        else
        {
            Destroy(gameObject);
        }
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
}
