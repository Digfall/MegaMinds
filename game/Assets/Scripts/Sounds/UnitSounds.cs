using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip AttackSound;
    public AudioClip DeathSound;

    public void PlayAttackSound()
    {
        if (AttackSound != null)
        {
            audioSource.PlayOneShot(AttackSound);
        }
    }

    public void PlayDeathSound()
    {
        if (DeathSound != null)
        {
            audioSource.PlayOneShot(DeathSound);
        }
    }
}
