using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Статы Персонажа")]
    public int HP = 200;
    public int damage = 50;
    public float speed = 2;
    public float damageRate = 1.0f;
    public float radius;
    public float attackRate = 1.5f;
    protected float nextAttackTime = 0f;
    protected float nextDamageTime = 0f;

    [Header("Обращения к объектам и трансформы")]
    public Transform attackPos;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Transform movePos;
    [SerializeField] protected Transform moveTarget;
    [SerializeField] protected Transform attackTarget;

    [Header("Настройки луча")]
    [SerializeField] protected float raycastDistanceToMove = 25f;

    public int level = 1;

    protected bool isFighting = false;
    protected bool isAttacking = false;
    protected bool isDead = false;
    protected Rigidbody2D rb;
    protected NavMeshAgent agent;
    [SerializeField] private UnitSounds unitSounds;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();
        ApplyLevelAdjustments();
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;
        nextAttackTime = Time.time + 1.0f;
    }

    protected virtual void Update()
    {
        if (isDead)
        {
            return; // Если мертв, не выполнять обновления
        }
        FindTargetToAttack();
        if (!isFighting && !isAttacking)
        {
            MoveToTarget();
        }
        else
        {
            rb.velocity = Vector2.zero;
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

            if (attackTarget.CompareTag("Player") || attackTarget.CompareTag("Castle"))
            {
                unitSounds.PlayAttackSound();
                attackTarget.GetComponent<PlayerBase>()?.TakeDamage(damage);
                attackTarget.GetComponent<Castle>()?.TakeDamage(damage);
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
                if (target.CompareTag("Player") || target.CompareTag("Castle"))
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
    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            isDead = true;
            healthBar.SetHealth(HP);
            OnDeath();
            unitSounds.PlayDeathSound();
        }
        else
        {
            healthBar.SetHealth(HP);
        }
    }
    protected virtual void OnDeath()
    {
        FindObjectOfType<ScienceManager>().UpdateScienceCountEnemy();
        Destroy(gameObject);
    }
    public abstract void ApplyLevelAdjustments();

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }
}
