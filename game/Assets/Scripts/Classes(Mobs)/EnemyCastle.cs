using System.Collections;
using UnityEngine;

public class EnemyCastle : MonoBehaviour
{
    [SerializeField] private int HP;
    [SerializeField] private HealthBarCastle healthBar;
    [SerializeField] private GameObject gameOverCanvas;
    private LevelManager levelManager;
    [SerializeField] private int levelNumber; // Номер этого уровня

    [SerializeField] private int levelREWARD; // Уровень награды
    [SerializeField] private bool rewardRepeatable = false; // Повторная выдача награды
    [SerializeField] private AudioSource destructionSound; // Звук разрушения башни
    [SerializeField] private AudioSource postDestructionSound; // Звук после разрушения

    private bool isDestroyed = false; // Флаг для предотвращения многократного вызова

    void Start()
    {
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        healthBar.SetHealth(HP);
        if (HP <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            DestroyCastle();
        }
    }

    private IEnumerator HandleDestructionSequence()
    {
        Time.timeScale = 0f;

        // Ждать окончания звука разрушения башни
        if (destructionSound != null)
        {
            destructionSound.Play();
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

    void DestroyCastle()
    {
        if (!levelManager.IsLevelCompleted(levelNumber) || rewardRepeatable)
        {
            FindObjectOfType<ScienceManager>().UpdateScienceCountCastle(levelREWARD);
            levelManager.CompleteLevel(levelNumber);
        }
        else
        {
            FindObjectOfType<ScienceManager>().UpdateScienceCountTotal();
        }
        StartCoroutine(HandleDestructionSequence());
    }
}
