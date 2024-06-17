using UnityEngine;

public class EnemyCastle : MonoBehaviour
{
    [SerializeField] private int HP;
    [SerializeField] private HealthBarCastle healthBar;
    [SerializeField] private GameObject gameOverCanvas;
    private LevelManager levelManager;
    [SerializeField] private int levelNumber; // Номер этого уровня

    [SerializeField] private int level; // Уровень награды
    [SerializeField] private bool rewardRepeatable = false; // Повторная выдача награды

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
    }

    void Update()
    {
        if (HP <= 0)
        {
            DestroyCastle();
        }
    }

    void DestroyCastle()
    {
        if (!levelManager.IsLevelCompleted(levelNumber) || rewardRepeatable)
        {
            FindObjectOfType<ScienceManager>().UpdateScienceCountCastle(level);
            levelManager.CompleteLevel(levelNumber);
        }
        else
        {
            FindObjectOfType<ScienceManager>().UpdateScienceCountTotal();
        }

        Time.timeScale = 0f;

        gameOverCanvas.SetActive(true);

        GameObject[] HpBars = GameObject.FindGameObjectsWithTag("HpBar");
        foreach (GameObject HpBar in HpBars)
        {
            HpBar.SetActive(false);
        }

        Destroy(gameObject);

    }
}
