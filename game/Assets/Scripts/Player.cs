using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Статы Персонажа")]

    public int HP;
    public int damage;
    public float speed;
    public float attackRate = 1.0f; // Время между атаками
    public float damageRate = 1.0f; // Задержка между каждым нанесением урона
    public float radius;

    [Header("Обращения к объектам и трансформы")]
    public Transform target;
    public Transform attackPos;
    public HealthBar healthBar;

    private bool isAttacking = false;
    private bool speedOff = true;
    private Rigidbody2D rb;

    private float nextAttackTime = 0f; // Время до следующей атаки
    private float nextDamageTime = 0f; // Время до следующего нанесения урона


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;

    }
    void Update()
    {
        SearchForEnemy();
        //
        target = GameObject.FindGameObjectWithTag("Enemy")?.GetComponent<Transform>();
        // Если вражеский объект не найден, устанавливаем цель на замок
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("EnemyCastle")?.GetComponent<Transform>();
        }
        if (!isAttacking && speedOff)
        {
            MoveToTarget();
        }
        else
        {
            rb.velocity = Vector2.zero; // Останавливаем персонажа во время атаки
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, radius);
        isAttacking = true;
        speedOff = false;
        // anim.SetBool("Attack", true);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].CompareTag("Enemy") || enemies[i].CompareTag("EnemyCastle"))
            {
                enemies[i].GetComponent<Enemy>()?.TakeDamage(damage);
                enemies[i].GetComponent<EnemyCastle>()?.TakeDamage(damage);
                nextDamageTime = Time.time + damageRate;
            }
        }
        // Добавляем задержку перед включением движения и отключением анимации атаки
        StartCoroutine(EndAttackAnimation());
    }
    void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //Vector2 direction = (target.position - transform.position).normalized;
        //rb.velocity = direction * speed;
    }

    IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(0.5f); // Можно изменить этот параметр в зависимости от длительности анимации атаки
        isAttacking = false;
        speedOff = true;
        // anim.SetBool("Attack", false);
    }

    void SearchForEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, radius);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].CompareTag("Enemy") && Time.time >= nextDamageTime)
            {
                OnAttack();
                break;
            }
            if (enemies[i].CompareTag("EnemyCastle") && Time.time >= nextDamageTime)
            {
                OnAttack();
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            OnAttack();
        }
        EnemyCastle enemyCastle = collision.gameObject.GetComponent<EnemyCastle>();
        if (enemyCastle != null)
        {
            OnAttack();
        }
    }
    public void TakeDamage(int damage)
    {
        healthBar.SetHealth(HP);
        HP -= damage;
    }

}