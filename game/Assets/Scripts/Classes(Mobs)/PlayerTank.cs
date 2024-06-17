using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI tankLvlText;
    [SerializeField] private int CurrentLevelTank = 1;
    private const string TankHPPrefKey = "TankHP";
    private const string TankDamagePrefKey = "TankDamage";

    public List<UpgradeTanks> upgradeLevels = new List<UpgradeTanks>();

    protected override void Start()
    {
        CurrentLevelTank = PlayerPrefs.GetInt("CurrentLevelTank", CurrentLevelTank);
        UpdateText();
        UpdatePlayerStats();
        base.Start();
    }

    private void UpdateText()
    {
        if (tankLvlText != null)
        {
            tankLvlText.text = CurrentLevelTank.ToString();
        }
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
