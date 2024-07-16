using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private int HP;
    [SerializeField] private HealthBarCastle healthBar;
    [SerializeField] private GameObject gameOverCanvas;

    [SerializeField] private AudioSource destructionSound; // Звук разрушения башни
    [SerializeField] private AudioSource postDestructionSound; // Звук после разрушения

    void Start()
    {
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        healthBar.SetHealth(HP);
    }

    void Update()
    {
        if (HP <= 0)
        {
            if (destructionSound != null)
            {
                DestroyCastle();
                destructionSound.Play();
            }

        }
    }

    public void DestroyCastle()
    {
        FindObjectOfType<ScienceManager>().UpdateScienceCountTotalLose();

        // Начало корутины для обработки последовательности разрушения
        StartCoroutine(HandleDestructionSequence());
    }

    private IEnumerator HandleDestructionSequence()
    {
        Time.timeScale = 0f;

        // Ждать окончания звука разрушения башни
        if (destructionSound != null)
        {
            yield return new WaitForSecondsRealtime(destructionSound.clip.length);
        }

        // Включение канваса
        gameOverCanvas.SetActive(true);

        // Воспроизведение звука после разрушения
        if (postDestructionSound != null)
        {
            postDestructionSound.Play();
        }

        // Деактивация всех полосок здоровья
        GameObject[] HpBars = GameObject.FindGameObjectsWithTag("HpBar");
        foreach (GameObject HpBar in HpBars)
        {
            HpBar.SetActive(false);
        }

        // Удаление объекта башни
        Destroy(gameObject);
    }
}
