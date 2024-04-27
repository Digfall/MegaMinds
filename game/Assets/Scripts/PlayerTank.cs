using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : PlayerBase
{
    private const string HPPrefKey = "PlayerHP";
    private const string DamagePrefKey = "PlayerDamage";
    private const string SpeedPrefKey = "PlayerSpeed";
    protected override void Start()
    {
        HP = PlayerPrefs.GetInt(HPPrefKey, 400);
        damage = PlayerPrefs.GetInt(DamagePrefKey, 20);
        speed = PlayerPrefs.GetFloat(SpeedPrefKey, 8f);
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
        PlayerPrefs.SetInt(HPPrefKey, HP);
        PlayerPrefs.SetInt(DamagePrefKey, damage);
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
