using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBase : MonoBehaviour
{
    [Header("Статы Персонажа")]
    public int HP;
    public int damage;
    public float speed;
    public float damageRate = 1.0f; // Задержка между каждым нанесением урона
    public float radius;
    public float attackRate; // Время между атаками в секундах
    protected float nextAttackTime = 0.8f; // Время до следующей атаки
    protected float nextDamageTime = 0.8f; // Время до следующего удара

    [Header("Обращения к объектам и трансформы")]
    public Transform attackPos;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Transform movePos;
    [SerializeField] protected Transform moveTarget; // Цель для передвижения
    [SerializeField] protected Transform attackTarget; // Цель для атаки

    [Header("Настройки луча")]

    [SerializeField] protected float raycastDistanceToMove = 25f;

    protected bool isFighting = false;
    protected bool isAttacking = false;
    protected Rigidbody2D rb;
    protected NavMeshAgent agent;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;
        nextAttackTime = Time.time + 0.8f;
    }

    protected virtual void Update()
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

    protected virtual void MoveToTarget()
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

    protected virtual void OnAttack()
    {
        if (Time.time >= nextAttackTime && attackTarget != null)
        {
            isAttacking = true;
            isFighting = true;

            if (attackTarget.CompareTag("Enemy") || attackTarget.CompareTag("EnemyCastle"))
            {
                attackTarget.GetComponent<EnemyBase>()?.TakeDamage(damage);
                attackTarget.GetComponent<EnemyCastle>()?.TakeDamage(damage);
                nextDamageTime = Time.time + damageRate;
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    protected virtual void FindTargetToAttack()
    {
        Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(attackPos.position, radius);
        if (targetsInRange.Length > 0)
        {
            Transform closestTarget = null;
            float minDistance = Mathf.Infinity;

            foreach (Collider2D target in targetsInRange)
            {
                if (target.CompareTag("Enemy") || target.CompareTag("EnemyCastle"))
                {
                    float distance = Vector2.Distance(transform.position, target.transform.position);
                    if (distance < minDistance)
                    {
                        closestTarget = target.transform;
                        minDistance = distance;
                    }
                }
            }

            attackTarget = closestTarget;

            if (attackTarget != null)
            {
                isAttacking = true;
                isFighting = true;
                OnAttack();
            }
            else
            {
                isAttacking = false;
                isFighting = false;
            }
        }
        else
        {
            attackTarget = null;
            isAttacking = false;
            isFighting = false;
        }
    }


    protected virtual Transform FindNearestTarget()
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

    public virtual void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            healthBar.SetHealth(HP);
            Destroy(gameObject);
        }
        else
        {
            healthBar.SetHealth(HP);
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
        Gizmos.DrawWireSphere(transform.position, raycastDistanceToMove);
    }
}