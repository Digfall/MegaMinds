using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRogue : PlayerBase
{
    private const string RogueHPPrefKey = "RogueHP";
    private const string RogueDamagePrefKey = "RogueDamage";
    private const string RogueSpeedPrefKey = "RogueSpeed";
    protected override void Start()
    {
        HP = PlayerPrefs.GetInt(RogueHPPrefKey, 50);
        damage = PlayerPrefs.GetInt(RogueDamagePrefKey, 50);
        speed = PlayerPrefs.GetFloat(RogueSpeedPrefKey, 2.5f);
        radius = 0.8f;
        attackRate = 0.5f;

        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt(RogueHPPrefKey, HP);
        PlayerPrefs.SetInt(RogueDamagePrefKey, damage);
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
