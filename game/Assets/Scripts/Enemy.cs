using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Статы Персонажа")]
    public int HP = 200;
    public int damage = 50;
    public float speed = 2;
    public float damageRate = 1.0f; // Задержка между каждым нанесением урона
    public float radius;
    public float attackRate = 1.5f; // Время между атаками в секундах
    private float nextAttackTime = 0f; // Время до следующей атаки
    private float nextDamageTime = 0f; // Время до следующего удара

    [Header("Обращения к объектам и трансформы")]
    public Transform attackPos;
    public HealthBar healthBar;
    public Transform movePos;
    [SerializeField] private Transform moveTarget; // Цель для передвижения
    [SerializeField] private Transform attackTarget; // Цель для атаки

    [Header("Настройки луча")]
    [SerializeField] private float raycastDistance = 0.5f; // Длина луча для поиска цели
    [SerializeField] private float raycastDistanceToMove = 15f;

    private bool isFighting = false;
    private bool isAttacking = false;
    private Rigidbody2D rb;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        rb = GetComponent<Rigidbody2D>();
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
            FindObjectOfType<ScienceManager>().UpdateScienceCountEnemy();
            Destroy(gameObject);
        }
    }
    //Следование за целью
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
    //Метод на атаку
    public void OnAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, radius);
            isAttacking = true;
            isFighting = true; // Устанавливаем флаг isFighting в true при использовании OnAttack
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].CompareTag("Player") || enemies[i].CompareTag("Castle"))
                {
                    enemies[i].GetComponent<Player>()?.TakeDamage(damage);
                    enemies[i].GetComponent<Castle>()?.TakeDamage(damage);
                    nextDamageTime = Time.time + damageRate;
                }
            }
            StartCoroutine(EndAttackAnimation());

            nextAttackTime = Time.time + 1f / attackRate; // Устанавливаем время следующей атаки
        }
    }
    //Время после атаки для анимации
    IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(2.5f);
        isAttacking = false;
        StartCoroutine(ResetIsFightingAfterDelay());
    }
    //Таймер файта
    IEnumerator ResetIsFightingAfterDelay()
    {
        yield return new WaitForSeconds(2.5f);
        isFighting = false;
    }
    //Рендж атаки
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }
    public void TakeDamage(int damage)
    {
        healthBar.SetHealth(HP);
        HP -= damage;
        isFighting = true;
        StartCoroutine(ResetIsFightingAfterDelay());
    }
    void FindTargetToAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(attackPos.position, Vector2.left, raycastDistance);
        if (hit.collider != null)
        {
            attackTarget = hit.collider.transform;
            if (attackTarget.CompareTag("Player") || attackTarget.CompareTag("Castle"))
            {
                OnAttack();
            }
        }

        Debug.DrawRay(attackPos.position, Vector2.left * raycastDistance, Color.red);
    }
    Transform FindNearestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, raycastDistanceToMove);
        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") || collider.CompareTag("Castle"))
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
