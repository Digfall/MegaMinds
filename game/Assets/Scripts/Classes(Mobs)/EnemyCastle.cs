using UnityEngine;

public class EnemyCastle : MonoBehaviour
{
    public int HP;
    public LayerMask TowerMask;
    public HealthBarCastle healthBar;
    public GameObject gameOverCanvas;
    private LevelManager levelManager;
    public int levelNumber; // Номер этого уровня

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
        FindObjectOfType<ScienceManager>().UpdateScienceCountCastle();
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
