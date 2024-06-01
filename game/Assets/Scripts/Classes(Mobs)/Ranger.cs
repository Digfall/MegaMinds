using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : EnemyBase
{
    protected override void Start()
    {
        base.Start();
    }

    public override void ApplyLevelAdjustments()
    {
        switch (level)
        {
            case 1:
                HP = 150;
                damage = 35;
                break;
            case 2:
                HP = 300;
                damage = 70;
                break;
            case 3:
                HP = 450;
                damage = 100;
                break;
            default:
                HP = 150;
                damage = 35;
                break;
        }
        speed = 2f;
        attackRate = 0.8f;
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

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    protected override Transform FindNearestTarget()
    {
        return base.FindNearestTarget();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void FindTargetToAttack()
    {
        base.FindTargetToAttack();
    }
}
