using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rangerlvl3 : EnemyBase
{
    protected override void Start()
    {
        HP = 198;
        damage = 66;
        radius = 2.13f;
        attackRate = 0.5f;
        speed = 2f;

        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        FindTargetToAttack();
        if (isAttacking && attackTarget != null)
        {
            OnAttack();
        }
    }

    protected override void OnAttack()
    {
        if (Time.time >= nextAttackTime && attackTarget != null)
        {
            PlayerBase playerBase = attackTarget.GetComponent<PlayerBase>();
            Castle castle = attackTarget.GetComponent<Castle>();

            if (playerBase != null)
            {
                playerBase.TakeDamage(damage);
            }
            else if (castle != null)
            {
                castle.TakeDamage(damage);
            }

            nextDamageTime = Time.time + damageRate;
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    protected override void FindTargetToAttack()
    {
        if (attackTarget != null && (Vector2.Distance(transform.position, attackTarget.position) > radius || !attackTarget.CompareTag("Enemy") && !attackTarget.CompareTag("EnemyCastle")))
        {
            attackTarget = null;
        }

        if (attackTarget == null)
        {
            Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(attackPos.position, radius);
            float minDistance = Mathf.Infinity;

            foreach (Collider2D target in targetsInRange)
            {
                if (target.CompareTag("Player") || target.CompareTag("Castle"))
                {
                    float distance = Vector2.Distance(transform.position, target.transform.position);
                    if (distance < minDistance)
                    {
                        attackTarget = target.transform;
                        minDistance = distance;
                    }
                }
            }

            if (attackTarget != null)
            {
                isAttacking = true;
                isFighting = true;
                rb.velocity = Vector2.zero;
            }
            else
            {
                isAttacking = false;
                isFighting = false;
            }
        }
    }

    protected override void MoveToTarget()
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

    protected override Transform FindNearestTarget()
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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raycastDistanceToMove);
    }
}
