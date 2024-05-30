using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : EnemyBase
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
                HP = 400;
                damage = 20;
                break;
            case 2:
                HP = 880;
                damage = 40;
                break;
            case 3:
                HP = 1320;
                damage = 60;
                break;
            default:
                HP = 400;
                damage = 20;
                break;
        }
        speed = 1.2f;
        attackRate = 1.5f;
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
