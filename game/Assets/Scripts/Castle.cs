using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
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
    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            DestroyCastle();
        }
    }
    public void DestroyCastle()
    {
        FindObjectOfType<ScienceManager>().UpdateScienceCountTotalLose();
        // Остановить все действия на карте
        Time.timeScale = 0f;

        // Включить нужный вам Canvas
        gameOverCanvas.SetActive(true);

        // Уничтожить объект башни
        Destroy(gameObject);
    }
}
