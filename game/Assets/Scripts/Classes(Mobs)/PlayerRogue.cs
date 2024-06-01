using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeRogues
{
    public int levelrog = 1;
    public int hprog;
    public int damagerog;
    public float speed;
    public int costrog;
    public int damageUpTextrog;
    public int hpUpTextrog;
}

public class PlayerRogue : PlayerBase
{
    private const string RogueHPPrefKey = "RogueHP";
    private const string RogueDamagePrefKey = "RogueDamage";

    public List<UpgradeRogues> upgradeLevels = new List<UpgradeRogues>();

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
        PlayerPrefs.SetInt(RogueHPPrefKey, HP);
        PlayerPrefs.SetInt(RogueDamagePrefKey, damage);
        PlayerPrefs.Save();
    }

    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(RogueHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(RogueDamagePrefKey, damage);
    }

    protected override void OnAttack()
    {
        base.OnAttack();
    }

    public override void TakeDamage(int damagerog)
    {
        base.TakeDamage(damagerog);
    }

    protected override void FindTargetToAttack()
    {
        base.FindTargetToAttack();
    }
}
