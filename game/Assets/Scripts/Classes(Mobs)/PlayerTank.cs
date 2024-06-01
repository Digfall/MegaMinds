using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeTanks
{
    public int levelTank = 1;
    public int hpTank;
    public int damageTank;
    public float speed;
    public int costTank;
    public int damageUpTextTank;
    public int hpUpTextTank;
}

public class PlayerTank : PlayerBase
{
    private const string TankHPPrefKey = "TankHP";
    private const string TankDamagePrefKey = "TankDamage";

    public List<UpgradeTanks> upgradeLevels = new List<UpgradeTanks>();

    protected override void Start()
    {
        UpdatePlayerStats();
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
        PlayerPrefs.Save();
    }

    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(TankHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(TankDamagePrefKey, damage);
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
