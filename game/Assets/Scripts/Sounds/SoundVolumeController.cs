using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeController : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private void Start()
    {
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = MusicManager.Instance.MusicVolume;
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = AudioManager.Instance.SFXVolume;
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }
    }

    private void OnMusicVolumeChanged(float value)
    {
        MusicManager.Instance.MusicVolume = value;
    }

    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SFXVolume = value;
    }
}
