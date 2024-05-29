using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warriorlvl2 : EnemyBase
{
    protected override void Start()
    {
        HP = 500;
        damage = 100;
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
