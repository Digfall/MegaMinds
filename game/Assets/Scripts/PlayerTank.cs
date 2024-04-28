using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : PlayerBase
{
    private const string TankHPPrefKey = "TankHP";
    private const string TankDamagePrefKey = "TankDamage";
    private const string TankSpeedPrefKey = "TankSpeed";
    protected override void Start()
    {
        HP = PlayerPrefs.GetInt(TankHPPrefKey, 400);
        damage = PlayerPrefs.GetInt(TankDamagePrefKey, 20);
        speed = PlayerPrefs.GetFloat(TankSpeedPrefKey, 1.2f);
        radius = 0.8f;
        attackRate = 0.6f;


        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt(TankHPPrefKey, HP);
        PlayerPrefs.SetInt(TankDamagePrefKey, damage);
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
