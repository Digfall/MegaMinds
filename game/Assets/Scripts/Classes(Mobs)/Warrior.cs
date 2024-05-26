using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : EnemyBase
{
    protected override void Start()
    {
        HP = 200;
        damage = 50;
        radius = 1.5f;
        attackRate = 0.8f;
        speed = 2f;

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
