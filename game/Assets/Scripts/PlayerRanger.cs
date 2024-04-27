using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanger : PlayerBase
{
    private const string HPPrefKey = "PlayerHP";
    private const string DamagePrefKey = "PlayerDamage";
    private const string SpeedPrefKey = "PlayerSpeed";
    protected override void Start()
    {
        HP = PlayerPrefs.GetInt(HPPrefKey, 60);
        damage = PlayerPrefs.GetInt(DamagePrefKey, 20);
        speed = PlayerPrefs.GetFloat(SpeedPrefKey, 2f);
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
        PlayerPrefs.SetInt(HPPrefKey, HP);
        PlayerPrefs.SetInt(DamagePrefKey, damage);
        PlayerPrefs.SetFloat(SpeedPrefKey, speed);
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
