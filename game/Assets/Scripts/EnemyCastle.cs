using UnityEngine;

public class EnemyCastle : MonoBehaviour
{
    public int HP;
    public LayerMask TowerMask;
    public HealthBarCastle healthBar;
    public GameObject gameOverCanvas;

    void Start()
    {
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;
    }

    public void TakeDamage(int damage)
    {
        healthBar.SetHealth(HP);
        HP -= damage;
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
        // Остановить все действия на карте
        Time.timeScale = 0f;

        // Включить нужный вам Canvas
        gameOverCanvas.SetActive(true);

        // Уничтожить объект башни
        Destroy(gameObject);
    }
}
