using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanklvl3 : EnemyBase
{
    protected override void Start()
    {
        HP = 1320;
        damage = 66;
        radius = 1.5f;
        attackRate = 1.5f;
        speed = 1.2f;

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
