using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanger : PlayerBase
{
    protected override void Start()
    {
        HP = 60;
        damage = 20;
        radius = 6f;
        attackRate = 0.5f;
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

    protected override void FindTargetToAttack()
    {
        base.FindTargetToAttack();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }
}
