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
