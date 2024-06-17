using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class UpgradeLevel
{
    public int levelwar = 1;
    public int hpwar;
    public int damagewar;
    public float speed;
    public int costwar;
    public int damageUpTextwar;
    public int hpUpTextwar;
}

public class PlayerWarrior : PlayerBase
{
    [SerializeField] private TextMeshProUGUI warriorLvlText;
    [SerializeField] private int CurrentLevelWar = 1;
    private const string WarriorHPPrefKey = "WarriorHP";
    private const string WarriorDamagePrefKey = "WarriorDamage";

    public List<UpgradeLevel> upgradeLevels = new List<UpgradeLevel>();

    protected override void Start()
    {
        CurrentLevelWar = PlayerPrefs.GetInt("CurrentLevelWar", CurrentLevelWar);
        UpdateText();
        UpdatePlayerStats();
        base.Start();
    }
    private void UpdateText()
    {
        if (warriorLvlText != null)
        {
            warriorLvlText.text = CurrentLevelWar.ToString();
        }
    }
    protected override void Update()
    {
        base.Update();
    }
    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt(WarriorHPPrefKey, HP);
        PlayerPrefs.SetInt(WarriorDamagePrefKey, damage);
        PlayerPrefs.Save();
    }
    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(WarriorHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(WarriorDamagePrefKey, damage);
    }

    protected override void OnAttack()
    {
        base.OnAttack();
    }

    public override void TakeDamage(int damagewar)
    {
        base.TakeDamage(damagewar);
    }

    protected override void FindTargetToAttack()
    {
        base.FindTargetToAttack();
    }
}
