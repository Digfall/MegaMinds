using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : EnemyBase
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
                HP = 50;
                damage = 50;
                break;
            case 2:
                HP = 120;
                damage = 100;
                break;
            case 3:
                HP = 200;
                damage = 200;
                break;
            default:
                HP = 50;
                damage = 50;
                break;
        }
        speed = 2.5f;
        attackRate = 0.5f;
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
