using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    public static MenuMusicManager instance;

    public AudioSource menuMusic;
    public AudioSource battleMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        StopAllMusic();
        if (menuMusic != null)
        {
            menuMusic.Play();
        }
    }

    public void PlayBattleMusic()
    {
        StopAllMusic();
        if (battleMusic != null)
        {
            battleMusic.Play();
        }
    }

    private void StopAllMusic()
    {
        if (menuMusic != null)
        {
            menuMusic.Stop();
        }
        if (battleMusic != null)
        {
            battleMusic.Stop();
        }
    }
}
