using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanger : PlayerBase
{
    private const string RangerHPPrefKey = "RangerHP";
    private const string RangerDamagePrefKey = "RangerDamage";
    private const string RangerSpeedPrefKey = "RangerSpeed";
    protected override void Start()
    {
        HP = PlayerPrefs.GetInt(RangerHPPrefKey, 60);
        damage = PlayerPrefs.GetInt(RangerDamagePrefKey, 20);
        speed = PlayerPrefs.GetFloat(RangerSpeedPrefKey, 2f);
        radius = 6f;
        attackRate = 0.5f;
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

    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt(RangerHPPrefKey, HP);
        PlayerPrefs.SetInt(RangerDamagePrefKey, damage);

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
