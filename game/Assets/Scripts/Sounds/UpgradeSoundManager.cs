using UnityEngine;

public class UpgradeSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip upgradeSuccessSound;
    public AudioClip upgradeFailSound;

    public void PlayUpgradeSuccessSound()
    {
        if (upgradeSuccessSound != null)
        {
            audioSource.PlayOneShot(upgradeSuccessSound);
        }
    }

    public void PlayUpgradeFailSound()
    {
        if (upgradeFailSound != null)
        {
            audioSource.PlayOneShot(upgradeFailSound);
        }
    }
}
