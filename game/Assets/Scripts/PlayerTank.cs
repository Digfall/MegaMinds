using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : PlayerBase
{
    protected override void Start()
    {
        HP = 400;
        damage = 20;
        radius = 0.8f;
        attackRate = 0.6f;
        speed = 0.8f;

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
