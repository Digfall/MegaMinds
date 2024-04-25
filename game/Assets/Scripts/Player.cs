using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [Header("Статы Персонажа")]
    public int HP = 200;
    public int damage = 50;
    public float speed = 2;

    public float damageRate = 1.0f; // Задержка между каждым нанесением урона
    public float radius;
    public float attackRate = 1.5f; // Время между атаками в секундах
    private float nextAttackTime = 0f; // Время до следующей атаки
    private float nextDamageTime = 0f; // Время до следующего нанесения урона

    [Header("Обращения к объектам и трансформы")]
    public Transform attackPos;
    public HealthBar healthBar;
    public Transform movePos;
    [SerializeField] private Transform moveTarget; // Цель для передвижения
    [SerializeField] private Transform attackTarget; // Цель для атаки

    [Header("Настройки луча")]
    [SerializeField] private float raycastDistance = 0.5f; // Длина луча для поиска цели
    [SerializeField] private float raycastDistanceToMove = 15f;

    private bool isAttacking = false;
    private bool isFighting = false;
    private Rigidbody2D rb;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;

    }
    void Update()
    {
        FindTargetToAttack();
        if (!isFighting && !isAttacking)
        {
            MoveToTarget();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, radius);
            isAttacking = true;
            isFighting = true; // Устанавливаем флаг isFighting в true при использовании OnAttack
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].CompareTag("Enemy") || enemies[i].CompareTag("EnemyCastle"))
                {
                    enemies[i].GetComponent<EnemyBase>()?.TakeDamage(damage);
                    enemies[i].GetComponent<EnemyCastle>()?.TakeDamage(damage);
                    nextDamageTime = Time.time + damageRate;
                }
            }
            StartCoroutine(EndAttackAnimation());

            nextAttackTime = Time.time + 1f / attackRate; // Устанавливаем время следующей атаки
        }
    }
    IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(2.5f);
        isAttacking = false;
        StartCoroutine(ResetIsFightingAfterDelay()); // Запускаем корутину для сброса isFighting обратно в false через 3 секунды после завершения атаки
    }
    IEnumerator ResetIsFightingAfterDelay()
    {
        yield return new WaitForSeconds(2.5f);
        isFighting = false; // Сбрасываем флаг isFighting обратно в false через 3 секунды после завершения атаки
    }
    void MoveToTarget()
    {
        Transform nearestTarget = FindNearestTarget();

        if (nearestTarget != null)
        {
            moveTarget = nearestTarget;
            transform.position = Vector2.MoveTowards(transform.position, moveTarget.position, speed * Time.deltaTime);
        }
        else
        {
            moveTarget = null;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }
    public void TakeDamage(int damage)
    {
        healthBar.SetHealth(HP);
        HP -= damage;
        isFighting = true; // Устанавливаем isFighting в true при получении урона
        StartCoroutine(ResetIsFightingAfterDelay());
    }
    void FindTargetToAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(attackPos.position, Vector2.right, raycastDistance);
        Debug.DrawRay(attackPos.position, Vector2.right * raycastDistance, Color.red);
        if (hit.collider != null)
        {
            attackTarget = hit.collider.transform;
            if (attackTarget.CompareTag("Enemy") || attackTarget.CompareTag("EnemyCastle"))
            {
                OnAttack();
            }
        }
    }
    Transform FindNearestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, raycastDistanceToMove);
        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") || collider.CompareTag("EnemyCastle"))
            {
                float distanceToTarget = Vector2.Distance(transform.position, collider.transform.position);
                if (distanceToTarget < nearestDistance)
                {
                    nearestTarget = collider.transform;
                    nearestDistance = distanceToTarget;
                }
            }
        }

        return nearestTarget;
    }
}