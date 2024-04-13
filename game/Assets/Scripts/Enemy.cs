using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int HP;
    public int damage;
    public HealthBar healthBar;
    public Transform attackPos;
    public LayerMask playerMask;
    public float radius;
    public Transform target;

    //private Animator anim;
    private bool isAttacking = false;
    [SerializeField] private bool speedOff = true;
    private bool isDamaged = false;
    //private float damageStartTime = 0f;
    public float attackRate = 1.0f; // Время между атаками
    public float damageRate = 1.0f; // Задержка между каждым нанесением урона
    private float nextAttackTime = 0f; // Время до следующей атаки
    private float nextDamageTime = 0f; // Время до следующего 
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        //MoveToTarget();
        SearchForEnemy();
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
    void MoveToTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
    void OnAttack()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(attackPos.position, radius, playerMask);
        isAttacking = true;
        speedOff = false;
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].CompareTag("Player") || targets[i].CompareTag("Castle"))
            {
                targets[i].GetComponent<Player>()?.TakeDamage(damage);
                targets[i].GetComponent<Castle>()?.TakeDamage(damage);
                nextDamageTime = Time.time + damageRate;
            }
        }
        // Добавляем задержку перед включением движения и отключением анимации атаки
        StartCoroutine(EndAttackAnimation());
    }
    IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(0.5f); // Можно изменить этот параметр в зависимости от длительности анимации атаки
        isAttacking = false;
        speedOff = true;
        //anim.SetBool("Attack", false);
    }
    void SearchForEnemy()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(attackPos.position, radius);
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].CompareTag("Player") && Time.time >= nextDamageTime)
            {
                OnAttack();
                break;
            }
            if (targets[i].CompareTag("Castle") && Time.time >= nextDamageTime)
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
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            OnAttack();
        }
        Castle castle = collision.gameObject.GetComponent<Castle>();
        if (castle != null)
        {
            OnAttack();
        }
    }
    public void TakeDamage(int damage)
    {
        healthBar.SetHealth(HP);
        HP -= damage;
        //damageStartTime = Time.time;
    }
}