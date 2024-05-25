using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : EnemyBase
{
    protected override void Start()
    {
        HP = 50;
        damage = 50;
        radius = 0.6f;
        attackRate = 0.5f;
        speed = 2.5f;

        base.Start();
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

    protected override void FindTargetToAttack()
    {
        base.FindTargetToAttack();
    }
}
