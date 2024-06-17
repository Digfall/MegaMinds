using UnityEngine;

public class EnemyCastle : MonoBehaviour
{
    [SerializeField] private int HP;
    [SerializeField] private HealthBarCastle healthBar;
    [SerializeField] private GameObject gameOverCanvas;
    private LevelManager levelManager;
    [SerializeField] private int levelNumber; // Номер этого уровня

    [SerializeField] private int level; // Уровень награды

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
        FindObjectOfType<ScienceManager>().UpdateScienceCountCastle(level);
        levelManager.CompleteLevel(levelNumber);
        // Остановить все действия на карте
        Time.timeScale = 0f;

        // Включить нужный вам Canvas
        gameOverCanvas.SetActive(true);

        GameObject[] HpBars = GameObject.FindGameObjectsWithTag("HpBar");
        foreach (GameObject HpBar in HpBars)
        {
            HpBar.SetActive(false);
        }
        // Уничтожить объект башни
        Destroy(gameObject);
    }
}
