using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    // private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private bool speedOff = true;
    private bool isAttacking = false;

    public Transform target;
    public Transform attackPos;
    public int damage;
    public float radius;

    public int HP;
    public HealthBar healthBar;
    public float attackRate = 2.0f; // Время между атаками
    public float damageRate = 0.5f; // Задержка между нанесением урона

    private float nextAttackTime = 0f; // Время до следующей атаки
    private float nextDamageTime = 0f; // Время до следующего нанесения урона


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;
        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
    }
    void Update()
    {
        SearchForEnemy();
        //

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