using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : EnemyBase
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
                HP = 200;
                damage = 50;
                break;
            case 2:
                HP = 400;
                damage = 90;
                break;
            case 3:
                HP = 600;
                damage = 149;
                break;
            default:
                HP = 200;
                damage = 50;
                break;
        }
        speed = 2f;
        attackRate = 0.8f;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnAttack()
    {
        base.OnAttack();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override Transform FindNearestTarget()
    {
        return base.FindNearestTarget();
    }

    protected override void FindTargetToAttack()
    {
        base.FindTargetToAttack();
    }
}
