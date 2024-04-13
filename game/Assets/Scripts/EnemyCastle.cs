using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastle : MonoBehaviour
{
    public int HP;
    public LayerMask TowerMask;
    public HealthBar healthBar;

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
            FindObjectOfType<CoinManager>().Addone();
            Destroy(gameObject);
        }
    }
}
