using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Animator anim;
    private Rigidbody2D rb;
    public bool speedOff = true;

    public Transform attackPos;
    public LayerMask enemy;
    public int damage;
    public float radius;

    public int HP;

    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, radius, enemy);
        speedOff = true;
        anim.SetBool("Attack", false);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].CompareTag("Enemy"))
            {
                enemies[i].GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    void SearchForEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, radius, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].CompareTag("Enemy"))
            {
                OnAttack();
                break;
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        SearchForEnemy();
        if (speedOff == true)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Вызываем метод обработки атаки при столкновении с врагом
            OnAttack();
        }
    }
}
